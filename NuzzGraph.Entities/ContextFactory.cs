using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using BrightstarDB.Client;

namespace NuzzGraph.Entities
{
    public static class ContextFactory
    {
        static string ConnectionString = ConfigurationManager.ConnectionStrings["Main"].ConnectionString;

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
