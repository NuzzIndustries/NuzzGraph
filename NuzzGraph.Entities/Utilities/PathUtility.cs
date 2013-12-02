using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NuzzGraph.Core.Utilities
{
    public static class PathUtility
    {
        public static string GetAppUsingDirectory()
        {
            string path = ".";
            #if DEBUG
                path += "/../../..";
            #endif
            return path;
        }
    }
}
