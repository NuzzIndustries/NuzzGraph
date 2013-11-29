using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;

namespace NuzzGraph.Viewer.Utilities
{
    internal static class ResourceUtility
    {
        internal static string[] GetResourcesUnder(string strFolder)
        {
            strFolder = strFolder.ToLower() + "/";

            var oAssembly = Assembly.GetCallingAssembly();
            string strResources = oAssembly.GetName().Name + ".g.resources";
            var oStream = oAssembly.GetManifestResourceStream(strResources);
            var oResourceReader = new ResourceReader(oStream);

            var vResources =
                from p in oResourceReader.OfType<DictionaryEntry>()
                let strTheme = (string)p.Key
                where strTheme.StartsWith(strFolder)
                select strTheme.Substring(strFolder.Length);

            return vResources.ToArray();
        }
    }
}
