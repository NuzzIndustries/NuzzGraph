using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using NuzzGraph.Entities;
using BrightstarDB.Client;
using NuzzGraph.Core.Utilities;
using System.IO;

namespace NuzzGraph.Core
{
    public static class ContextFactory
    {
        static string ConnectionString { get; set; }

        static string StoreName { get; set; }

        static ContextFactory()
        {
            try
            {
                StoreName = ConfigurationManager.AppSettings["StoreName"];
            }
            catch (Exception)
            {
                StoreName = "nuzzgraph_default";
            }

            ConnectionString = string.Format("type=Embedded;StoreName={0}", StoreName);
            try
            {
                ConnectionString = ConnectionString + GetEmbeddedPath();
            }
            catch (Exception)
            {
                ConnectionString = ConnectionString + ";StoresDirectory=./data";
            }

        }

        //Gets the path to add to the connection string for StoresDirectory
        private static string GetEmbeddedPath()
        {
            string path = "";
            if (RuntimeUtility.RunningFromVisualStudioDesigner)
                path = "C:\\dev\\nuzzgraph\\nuzzgraph\\data";
            else
                path = Path.GetFullPath(PathUtility.GetAppUsingDirectory()) + "/data";

            return ";StoresDirectory=" + path;
        }

        public static GraphContext New()
        {
            return new GraphContext(ConnectionString);
        }

        public static IDataObjectStore GetCore()
        {
            //var db = new EmbeddedDataObjectContext(new BrightstarDB.ConnectionString(ConnectionString));
            
            var raw = BrightstarService.GetDataObjectContext(ConnectionString).OpenStore(StoreName);
            return raw;
        }

        public static IBrightstarService GetClient()
        {
            return BrightstarService.GetClient(ConnectionString);
        }
    }
}
