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
        static string ConnectionString { get; set; }
        internal static string StoreName { get; private set; }
        internal static string SvcName { get; private set; }

        internal static string PathToData { get; private set; }

        static ContextFactory()
        {
            SvcName = LoadServiceName();
            StoreName = GetStoreName();
            PathToData = GetEmbeddedPath();
            ConnectionString = string.Format("type=Embedded;StoreName={0};StoresDirectory={1}", StoreName, PathToData);
        }

        private static string LoadServiceName()
        {
            var n = ConfigurationManager.AppSettings["SVCName"];
            if (string.IsNullOrEmpty(n))
                n = "BrightstarDB";
            var svc = new ServiceController(n, ".");

            ManagementClass mc = new ManagementClass("Win32_Service");
            bool flag = true;
            foreach (ManagementObject mo in mc.GetInstances())
            {
                if (mo.GetPropertyValue("Name").ToString() == n)
                    flag = false;
            }
            if (flag)
                throw new InvalidOperationException("Unable to find service with name " + n);

            return n;
        }

        private static string GetStoreName()
        {
            string sn = "";
            try
            {
                if (RuntimeUtility.RunningFromVisualStudioDesigner)
                    sn = "nuzzgraph";
                else
                    sn = ConfigurationManager.AppSettings["StoreName"];

                if (string.IsNullOrEmpty(StoreName))
                    sn = "nuzzgraph";
            }
            catch (Exception)
            {
                sn = "nuzzgraph";
            }
            return sn;
        }

        //Gets the path to add to the connection string for StoresDirectory
        private static string GetEmbeddedPath()
        {
            string path = "";
            try
            {
                if (RuntimeUtility.RunningFromVisualStudioDesigner)
                    path = "C:\\dev\\nuzzgraph\\nuzzgraph\\ngdata\\";
                else
                    path = Path.GetFullPath(PathUtility.GetAppUsingDirectory()) + "\\ngdata\\";
            }
            catch (Exception)
            {
                //Load service name
                string svcname = SvcName;

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

                path = root.FullName + "\\data\\";
            }

            return path;
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
