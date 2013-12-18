using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Management;
using System.ServiceProcess;
using System.Text;
using NuzzGraph.Core.Utilities;

namespace NuzzGraph.Core
{
    public class Configuration
    {
        public enum ConnectionType
        {
            Embedded,
            REST
        }

        public string ConnectionString 
        {
            get 
            {
                switch (ConnectType)
                {
                    case ConnectionType.Embedded:
                        return string.Format("type=embedded;StoreName={1};StoresDirectory={2}", ConnectType, StoreName, PathToData);
                    case ConnectionType.REST:
                        return string.Format("type=rest;endpoint={0};storename={1}", RestEndpointUrl, StoreName);
                    default:
                        throw new NotImplementedException();
                }
            }
        }

        public string StoreName { get; set; }

        private string _SvcName;
        public string SvcName 
        {
            get { return _SvcName; }
            set { _SvcName = value; PathToData = GetEmbeddedPath(); }
        }

        public string PathToData { get; private set; }
        public ConnectionType ConnectType { get; set; }
        public string RestEndpointUrl { get; set; }

        private static string defaultStoreName { get; set; }
        private static string defaultSvcName { get; set; }
        private static ConnectionType defaultConnectType { get; set; }
        private static string defaultRestEndpointUrl { get; set; }

        static Configuration()
        {
            defaultSvcName = LoadServiceName();
            defaultStoreName = GetStoreName();
            defaultConnectType = ConnectionType.Embedded;
            defaultRestEndpointUrl = "http://localhost:8090/brightstar";
        }

        public Configuration()
        {
            StoreName = defaultStoreName;
            SvcName = defaultSvcName;
            ConnectType = defaultConnectType;
            RestEndpointUrl = defaultRestEndpointUrl;
        }

        private static string LoadServiceName()
        {
            var n = ConfigurationManager.AppSettings["DefaultSVCName"];
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
            string sn = "nuzzgraph";

            try
            {
                if (!RuntimeUtility.RunningFromVisualStudioDesigner)
                    sn = ConfigurationManager.AppSettings["DefaultStoreName"];
            }
            catch (Exception) { }
            return sn;
        }

        //Gets the path to add to the connection string for StoresDirectory
        private string GetEmbeddedPath()
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
    }
}
