using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using BrightstarDB.Client;
using NuzzGraph.Core.Utilities;
using System.IO;
using System.ServiceProcess;
using System.Management;

namespace NuzzGraph.Core
{
    public static class ContextFactory
    {
        public static Configuration DefaultConfig { get; set; }

        static ContextFactory()
        {
            DefaultConfig = new Configuration();
        }

        public static NuzzGraphContext New(Configuration config = null)
        {
            if (config == null)
                config = DefaultConfig;
            //return new GraphContext(config.ConnectionString, null, Constants.GraphUri, new List<string>() { Constants.GraphUri }, null);
            return new NuzzGraphContext(config.ConnectionString);
            //return new GraphContext(config.ConnectionString, null, Constants.GraphUri, new List<string>(), null);
        }

        public static IDataObjectStore GetCore(Configuration config = null)
        {
            if (config == null)
                config = DefaultConfig;

            //var db = new EmbeddedDataObjectContext(new BrightstarDB.ConnectionString(ConnectionString));

            var raw = BrightstarService.GetDataObjectContext(config.ConnectionString).OpenStore(config.StoreName);
            //var raw = BrightstarService.GetDataObjectContext(config.ConnectionString).OpenStore(config.StoreName, null, null, Constants.GraphUri, new List<string>() { Constants.GraphUri });
            return raw;
        }

        public static IBrightstarService GetClient(Configuration config = null)
        {
            if (config == null)
                config = DefaultConfig;

            return BrightstarService.GetClient(config.ConnectionString);
        }
    }
}
