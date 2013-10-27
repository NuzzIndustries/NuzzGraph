using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NuzzGraph.Entities;
using BrightstarDB.Client;
using System.Reflection;

namespace NuzzGraph.Seed
{
    class Program
    {
        //Config
        static string StoreName { get { return "nuzzgraph"; } }
        static string ConnectionString { get { return "type=http;endpoint=http://localhost:8090/brightstar;storeName=nuzzgraph"; } }
        static string EntityTypeNamespace { get { return "NuzzGraph.Entities"; } } 

        static IBrightstarService Client { get; set; }
        static GraphContext Context { get; set; }

        static Dictionary<System.Type, INodeType> CLRTypeMap { get; set; }
        static List<System.Type> AllCLRTypes { get; set; }

        static void Main(string[] args)
        {
            ResetDB();
        }

        private static void ResetDB()
        {
            //Load client and context
            Client = BrightstarService.GetClient(ConnectionString);
            Context = new GraphContext(ConnectionString);

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

            //Manually create core type nodes
            var nodeTypeNode = (NodeType)Context.NodeTypes.Create();

            LoadCLRTypeMap();

            
            foreach (var clrType in AllCLRTypes)
            {
                if (clrType == typeof(NodeType))
                    continue;

                //Create type node
                var t = Context.NodeTypes.Create();

                //Mark the node a a type node
                //tType. = nodeTypeNode;
                //t.AllowedIncomingRelationships
                //t.AllowedOutgoingRelationships
                //t.Functions
                //t.IsAbstract
                //t.SubTypes
                //t.SuperTypes

                //Get properties of type
                foreach (var prop in clrType.GetProperties(flags))
                {
                    
                    //code goes here
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
                INodeType y = t;
                
            }
        }
    }
}
