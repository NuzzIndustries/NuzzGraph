using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NuzzGraph.Entities;
using BrightstarDB.Client;
using System.Reflection;
using BrightstarDB.EntityFramework;
using System.Configuration;
using System.ServiceProcess;
using System.Management;
using System.IO;
using System.ServiceModel;
using VDS.RDF;
using VDS.RDF.Writing;
using NuzzGraph.Core;
using NuzzGraph.Entities.Attributes;

namespace NuzzGraph.Seed
{
    class Program
    {
        //Config
        static string StoreName { get { return "nuzzgraph"; } }
        

        static IBrightstarService Client { get; set; }
        static GraphContext Context { get; set; }

        static Dictionary<System.Type, INodeType> CLRTypeMap { get; set; }
        
        
        static Dictionary<System.Type, IScalarType> ScalarTypeMap { get; set; }

        static Program()
        {
           
        }

        static void Main(string[] args)
        {
            try
            {
                ResetDB();
                var job = Client.StartExport(StoreName, "test.n3", null);
                System.Threading.Thread.Sleep(1000);
               
                var rdftext = File.ReadAllText("test/import/test.n3");
                IGraph g = new Graph();
                VDS.RDF.Parsing.FileLoader.Load(g, "test/import/test.n3");
                var writer = new RdfXmlWriter();
                writer.Save(g, Path.GetFullPath("./test/import/test.rdf"));
               
            }
            finally
            {
                Context.Dispose();
            }
        }

        private static void ResetDB()
        {
            //Load client and context
            Client = ContextFactory.GetClient();
            try
            {
                Context = ContextFactory.New();
            }
            catch (FaultException e)
            {
                if (e.Message != string.Format("Error creating store {0}. Store already exists", StoreName))
                    throw;

                 //Load service name
                string svcname = ConfigurationManager.AppSettings["SVCName"];

                //Load DB service
                var svc = new ServiceController(svcname, ".");
                
                //Get path to service exe in order to delete / cleanup folder for store
                string svcPath = null;
                ManagementClass mc = new ManagementClass("Win32_Service");
                foreach (ManagementObject mo in mc.GetInstances())
                {
                    if (mo.GetPropertyValue("Name").ToString() == svcname)
                        svcPath = mo.GetPropertyValue("PathName").ToString().Trim('"');
                }
                
                //Load path to folder
                var root = Directory.GetParent(svcPath).Parent;
                var ngfolder = root.GetDirectories()
                    .Where(x => x.Name.ToLower() == "data")
                    .Single()
                    .GetDirectories()
                    .Where(x => x.Name.ToLower() == "nuzzgraph")
                    .FirstOrDefault();
                var file = ngfolder.GetFiles().Where(x => x.Name == "data.bs").FirstOrDefault();
                if (file == null)
                    throw new InvalidOperationException("Unable to delete the folder " + ngfolder.FullName);
                   

                //Stop DB Service
                svc.Stop();
                svc.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(20));
                System.Threading.Thread.Sleep(200);

                //Delete store folder
                try
                {
                    ngfolder.Delete();
                }
                catch (Exception)
                {
                    throw new InvalidOperationException("Unable to remove store " + StoreName + ".");
                }

                //Restart service
                svc.Start();
                svc.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(20));
                System.Threading.Thread.Sleep(200);
                
                //Reload context
                Client = ContextFactory.GetClient();
                Context = ContextFactory.New();
            }

            //Delete store if it exists
            if (Client.DoesStoreExist(StoreName))
                Client.DeleteStore(StoreName);

            //Create store
            Client.CreateStore(StoreName);

            SeedData();
        }

        private static void SeedData()
        {
            LoadCLRTypeMap();

            //Load other data
            LoadScalarTypes();
            ProcessProperties();
            LoadMethods();

            Context.SaveChanges();
        }



        private static void LoadCLRTypeMap()
        {
            CLRTypeMap = new Dictionary<System.Type, INodeType>();

            //Load Types
            

            //Load Types
            foreach (var clrType in EntityUtility.AllCLRTypes)
            {
                //Create type node
                var t = Context.NodeTypes.Create();
                t.Label = clrType.Name.Substring(1);
                CLRTypeMap[clrType] = t;
                //Context.NodeTypes.Add(t);
            }

            Context.SaveChanges();

            //Build inheritence structure
            foreach (var clrType in EntityUtility.AllCLRTypes)
            {
                var tNode = CLRTypeMap[clrType];
                var inheritsAttribute = clrType.GetCustomAttributes(typeof(InheritsAttribute), false).FirstOrDefault() as InheritsAttribute;
                if (inheritsAttribute == null)
                    continue;
                var superType = EntityUtility.AllCLRTypes.First(x => x.Name == "I" + inheritsAttribute.InheritedClass);
                var superTypeNode = CLRTypeMap[superType];
                tNode.SuperTypes.Add(superTypeNode);
            }

            Context.SaveChanges();

            EntityUtility.NodeTypesInitialized = true;
        }

        private static void LoadScalarTypes()
        {
            ScalarTypeMap = new Dictionary<System.Type, IScalarType>();

            //Types:
            //Text
            //Integer
            //Decimal
            //Bool
            IScalarType txt, integer, dec, boolean, anyScalar;

            var n = Context.ScalarTypes.Create();
            n.Label = "Text";
            txt = n;

            n = Context.ScalarTypes.Create();
            n.Label = "Integer";
            integer = n;

            n = Context.ScalarTypes.Create();
            n.Label = "Decimal";
            dec = n;

            n = Context.ScalarTypes.Create();
            n.Label = "Boolean";
            boolean = n;

            n = Context.ScalarTypes.Create();
            n.Label = "AnyScalar";
            anyScalar = n;

            ScalarTypeMap[typeof(string)] = txt;
            ScalarTypeMap[typeof(char)] = txt;
            ScalarTypeMap[typeof(float)] = dec;
            ScalarTypeMap[typeof(decimal)] = dec;
            ScalarTypeMap[typeof(double)] = dec;
            ScalarTypeMap[typeof(int)] = integer;
            ScalarTypeMap[typeof(long)] = integer;
            ScalarTypeMap[typeof(short)] = integer;
            ScalarTypeMap[typeof(byte)] = integer;
            ScalarTypeMap[typeof(bool)] = boolean;
            ScalarTypeMap[typeof(char?)] = txt;
            ScalarTypeMap[typeof(float?)] = dec;
            ScalarTypeMap[typeof(decimal?)] = dec;
            ScalarTypeMap[typeof(double?)] = dec;
            ScalarTypeMap[typeof(int?)] = integer;
            ScalarTypeMap[typeof(long?)] = integer;
            ScalarTypeMap[typeof(short?)] = integer;
            ScalarTypeMap[typeof(byte?)] = integer;
            ScalarTypeMap[typeof(bool?)] = boolean;
            ScalarTypeMap[typeof(object)] = anyScalar;

            Context.SaveChanges();
        }

        private static void ProcessProperties()
        {
            var flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic;
            foreach (var clrType in EntityUtility.AllCLRTypes)
            {
                var nodeTypeNode = Context.NodeTypes.Where(x => x.Label == clrType.Name.Substring(1)).Single();

                //Get properties of type
                foreach (var prop in clrType.GetProperties(flags))
                {
                    //Skip if inverse property
                    if (prop.GetCustomAttributes(typeof(InversePropertyAttribute), false).Count() == 1)
                        continue;

                    //Determine if collection
                    if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(ICollection<>))
                    {
                        //Load inner type
                        var innerType = prop.PropertyType.GetGenericArguments().First();

                        //Determine if it is a scalar
                        if (EntityUtility.AllSimpleTypes.Contains(innerType))
                        {
                            throw new InvalidOperationException("Scalar collections not supported yet.");
                        }
                        else
                        {
                            //Collection of nodes
                            if (!CLRTypeMap.ContainsKey(innerType))
                                throw new InvalidOperationException("Node Type not found: " + innerType.Name);

                            //Create relationship definition
                            var relnode = Context.RelationshipTypes.Create();
                            relnode.Label = prop.Name;
                            relnode.SupportsMany = true;
                            relnode.OutgoingFrom = CLRTypeMap[clrType];
                            relnode.IncomingTo = CLRTypeMap[innerType];
                        }
                    }
                    else //Not a collection
                    {
                        //Determine if it is a relationship or a property
                        if (EntityUtility.AllSimpleTypes.Contains(prop.PropertyType) || prop.PropertyType == typeof(object))
                        {
                            //Property
                            var propnode = Context.NodePropertyDefinitions.Create();
                            propnode.Label = prop.Name;
                            propnode.DeclaringType = nodeTypeNode;
                            propnode.PropertyType = ScalarTypeMap[prop.PropertyType];
                        }
                        else
                        {
                            //Relationship
                            if (!CLRTypeMap.ContainsKey(prop.PropertyType))
                                throw new InvalidOperationException("Node Type not found: " + prop.PropertyType.Name);

                            var relnode = Context.RelationshipTypes.Create();
                            relnode.Label = prop.Name;
                            relnode.SupportsMany = false;
                            relnode.OutgoingFrom = CLRTypeMap[clrType];
                            relnode.IncomingTo = CLRTypeMap[prop.PropertyType];
                        }
                    }
                }
            }
        }

        private static void LoadMethods()
        {
            var flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic;
            foreach (var clrType in EntityUtility.AllCLRTypes)
            {
                var methods = clrType.GetMethods(flags).ToList();
                foreach (var method in methods)
                {
                    if (method.IsSpecialName)
                        continue;
                    var funcNode = Context.Functions.Create();
                    funcNode.DeclaringType = CLRTypeMap[clrType];
                    funcNode.Label = method.Name;
                    funcNode.FunctionBody = ""; 
                }
            }
        }
    }
}
