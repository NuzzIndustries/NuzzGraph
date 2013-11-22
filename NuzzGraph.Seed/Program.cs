using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NuzzGraph.Entities;
using BrightstarDB.Client;
using System.Reflection;
using BrightstarDB.EntityFramework;

namespace NuzzGraph.Seed
{
    class Program
    {
        //Config
        static string StoreName { get { return "nuzzgraph"; } }
        static string EntityTypeNamespace { get { return "NuzzGraph.Entities"; } } 

        static IBrightstarService Client { get; set; }
        static GraphContext Context { get; set; }

        static Dictionary<System.Type, INodeType> CLRTypeMap { get; set; }
        static List<System.Type> AllCLRTypes { get; set; }
        
        static Dictionary<System.Type, IScalarType> ScalarTypeMap { get; set; }

        static Program()
        {
           
        }

        static void Main(string[] args)
        {
            try
            {
                ResetDB();
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
            Context = ContextFactory.New();

            //Delete store if it exists
            if (Client.DoesStoreExist(StoreName))
                Client.DeleteStore(StoreName);

            //Create store
            Client.CreateStore(StoreName);

            SeedData();
        }

        private static void SeedData()
        {
            var flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic;

            //Create and load node type nodes
            LoadCLRTypeMap();

            LoadScalarTypes();

            //Mark the node a a type node
            //tType. = nodeTypeNode;
            //t.AllowedIncomingRelationships
            //t.AllowedOutgoingRelationships
            //t.Functions
            //t.IsAbstract
            //t.SubTypes
            //t.SuperTypes

            foreach (var clrType in AllCLRTypes)
            {
                var nodeTypeNode = Context.NodeTypes.Where(x => x.Label == clrType.Name.Substring(1)).Single();
                
                //Get properties of type
                foreach (var prop in clrType.GetProperties(flags))
                {
                    //Skip if inverse property
                    if (prop.GetCustomAttributes(typeof(InversePropertyAttribute), false).Count() == 1)
                        continue;

                    //Determine if collection
                    if (prop.GetType() == typeof(ICollection<>))
                    {
                        //Load inner type
                        var innerType = prop.GetType().GetGenericArguments().First();

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
                            relnode.SupportsMany = true;
                            relnode.Label = prop.Name;
                            relnode.OutgoingFrom = CLRTypeMap[clrType];
                            relnode.IncomingTo = CLRTypeMap[innerType];

                        }


                    }


                    //Determine if it is a relationship or a property
                    if (EntityUtility.AllSimpleTypes.Contains(prop.PropertyType))
                    {
                        //Property
                        var propnode = Context.NodePropertyDefinitions.Create();
                        var typeNode = CLRTypeMap[typeof(INodeType)];
                        propnode.DeclaringType = nodeTypeNode;
                        propnode.Label = prop.Name;
                        propnode.PropertyType = ScalarTypeMap[prop.PropertyType];
                    }
                    else
                    {
                        //Relationship
                        var relnode = Context.RelationshipTypes.Create();
                        
                        if (prop.GetType() == typeof(ICollection<>))
                            relnode.SupportsMany = true;


                       // relnode.
                    }
                }
            }

            
            var nodes1 = Context.Nodes.Create();
            var function1 = (Function)Context.Functions.Create();
            var f2 = (INode)function1;
            var f3 = f2.Get();
            var id = function1.Id;



        }

        private static void LoadCLRTypeMap()
        {
            CLRTypeMap = new Dictionary<System.Type, INodeType>();

            //Load Types
            AllCLRTypes = typeof(NuzzGraph.Entities.IType).Assembly
                .GetTypes()
                .Where(x => x.IsInterface)
                .Where(x => x.Namespace == EntityTypeNamespace)
                .Where(x => x.GetCustomAttributes(typeof(BrightstarDB.EntityFramework.EntityAttribute), false).Count() == 1)
                .ToList();

            foreach (var clrType in AllCLRTypes)
            {
                //Create type node
                var t = Context.NodeTypes.Create();
                t.Label = clrType.Name.Substring(1);
                CLRTypeMap[clrType] = t;
                //Context.NodeTypes.Add(t);
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
            IScalarType txt, integer, dec, boolean;

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

            Context.SaveChanges();
        }
    }
}
