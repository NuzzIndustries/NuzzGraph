using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Reflection;
using System.ServiceModel;
using System.ServiceProcess;
using System.Text.RegularExpressions;
using BrightstarDB.Client;
using BrightstarDB.EntityFramework;
using NuzzGraph.Core;
using NuzzGraph.Entities;
using NuzzGraph.Entities.Attributes;
using VDS.RDF.Writing;

namespace NuzzGraph.Seed
{
    class Program
    {
        //Config
        static string StoreName { get { return "nuzzgraph"; } }


        static IBrightstarService Client { get; set; }
        static GraphContext Context { get; set; }

        static Dictionary<System.Type, NuzzGraph.Entities.NodeType> CLRTypeMap { get; set; }


        static Dictionary<System.Type, ScalarType> ScalarTypeMap { get; set; }

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

                /*
                var rdftext = File.ReadAllText("test/import/test.n3");
                VDS.RDF.IGraph g = new VDS.RDF.Graph();
                VDS.RDF.Parsing.FileLoader.Load(g, "test/import/test.n3");
                var writer = new RdfXmlWriter();
                writer.Save(g, Path.GetFullPath("./test/import/test.rdf"));
                */
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
            catch (Exception e)
            {
                if (e.Message != string.Format("Error creating store {0}. Store already exists", StoreName))
                    throw;

                //Load service name
                string svcname = ConfigurationManager.AppSettings["SVCName"];

                //Load DB service
                var svc = new ServiceController(svcname, ".");

                var ngfolder = new DirectoryInfo(ContextFactory.PathToData + ContextFactory.StoreName);
                var file = ngfolder.GetFiles().Where(x => x.Name == "data.bs").FirstOrDefault();
                if (file == null)
                    throw new InvalidOperationException("Unable to delete the folder " + ngfolder);

                //Stop DB Service
                if (svc.Status != ServiceControllerStatus.Stopped)
                    svc.Stop();
                svc.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(10));
                System.Threading.Thread.Sleep(200);

                //Delete store folder
                try
                {
                    foreach (var f in ngfolder.GetFiles().ToList())
                    {
                        try
                        {
                            f.Delete();
                        }
                        catch (Exception ex1)
                        {
                            if (ex1.Message.Contains("because it is being used by another process"))
                            {
                                //Check which processes are using the file to be deleted.  If it is the visual studio designer, end it
                                
                                //var pr = Win32Processes.GetProcessesLockingFile(f.FullName).ToLookup(x => x.ProcessName);
                                //foreach(var _p in pr.Where(x => x.Key == "XDesProc"))
                                foreach(var p in Process.GetProcessesByName("XDesProc"))
                                {
                                    p.Kill();
                                }
                                System.Threading.Thread.Sleep(100);
                            }
                        }
                    }

                    ngfolder.Delete(true);
                }
                catch (Exception ex)
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

            //Pause to allow for store to be fully deleted.
            System.Threading.Thread.Sleep(200);

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
            CLRTypeMap = new Dictionary<System.Type, NodeType>();
            var assembly = Assembly.GetAssembly(typeof(INodeType));

            INodeType nodeTypeNode = null;

            //Load Types
            foreach (var clrType in EntityUtility.AllCLRTypes)
            {
                //Create type node
                var t = (NodeType)Context.NodeTypes.Create();
                t.Label = clrType.Name.Substring(1);
                CLRTypeMap[clrType] = t;
                if (clrType.Name == "INodeType")
                    nodeTypeNode = t;
            }
            //Map non-interface types to nodes
            foreach (var clrType in EntityUtility.AllCLRTypes)
            {
                //Create type node
                var t = CLRTypeMap[clrType];

                //Get non-interface type
                var nonInterfaceType = assembly.GetType(clrType.Namespace + "." + t.Label);

                if (nonInterfaceType == null)
                    throw new InvalidOperationException();

                CLRTypeMap[nonInterfaceType] = t;
            }

            Context.SaveChanges();

            //Build inheritance structure
            foreach (var clrType in EntityUtility.AllCLRTypes)
            {
                var tNode = CLRTypeMap[clrType];
                tNode.TypeHandle = (NuzzGraph.Entities.NodeType)nodeTypeNode;
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
            ScalarTypeMap = new Dictionary<System.Type, ScalarType>();

            //Types:
            //Text
            //Integer
            //Decimal
            //Bool
            ScalarType txt, integer, dec, boolean, anyScalar;

            var n = (ScalarType)Context.ScalarTypes.Create();
            n.Label = "Text";
            txt = n;

            n = (ScalarType)Context.ScalarTypes.Create();
            n.Label = "Integer";
            integer = n;

            n = (ScalarType)Context.ScalarTypes.Create();
            n.Label = "Decimal";
            dec = n;

            n = (ScalarType)Context.ScalarTypes.Create();
            n.Label = "Boolean";
            boolean = n;

            n = (ScalarType)Context.ScalarTypes.Create();
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
                var nodeTypeNode = (NodeType)Context.NodeTypes.Where(x => x.Label == clrType.Name.Substring(1)).Single();

                //Get properties of type
                foreach (var prop in clrType.GetProperties(flags).Where(x => clrType != typeof(INode) || x.Name != "Id"))
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
                        if (EntityUtility.IsScalar(prop.PropertyType))
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
            Context.SaveChanges();

            //For each NodePropertyDefinition, attach the RDF URI to NG data
            foreach (var pDef in Context.NodePropertyDefinitions)
            {
                if (pDef.TypeHandle.Label != "NodePropertyDefinition")
                    continue;

                var map = ((NodePropertyDefinition)pDef).Context.Mappings;

                var tInfo = EntityUtility.AllCLRTypes.Where(x => x.Name == "I" + pDef.DeclaringType.Label).Single();
                var pInfo = tInfo.GetProperty(pDef.Label);
                var hint = map.GetPropertyHint(pInfo);

                PropertyMappingType mapType = hint.MappingType;

                if (hint.MappingType == PropertyMappingType.Property)
                    pDef.InternalUri = hint.SchemaTypeUri;
            }

            Context.SaveChanges();

            foreach (var rDef in Context.RelationshipTypes)
            {
                //rDef.
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
