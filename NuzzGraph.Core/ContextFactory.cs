using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using NuzzGraph.Entities;
using BrightstarDB.Client;
using NuzzGraph.Core.Utilities;

namespace NuzzGraph.Core
{
    public static class ContextFactory
    {
        static string ConnectionString = ConfigurationManager.ConnectionStrings["Main"].ConnectionString + GetEmbeddedPath();

        //Gets the suffix to add to the connection string, to determine path
        private static string GetEmbeddedPath()
        {
            return ";StoresDirectory=" + PathUtility.GetAppUsingDirectory() + "/data";
        }

        public static GraphContext New()
        {
            return new GraphContext(ConnectionString);
        }

        public static IBrightstarService GetClient()
        {
            return BrightstarService.GetClient(ConnectionString);
        }
    }
}
