using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NuzzGraph.Core
{
    public static class Constants
    {
        public static readonly string GraphUri = BrightstarDB.Constants.DefaultGraphUri;// "http://www.nuzzgraph.com/Graphs/Core";

        internal static readonly string NodeUriFormatBase = "http://www.nuzzgraph.com/Entities/{0}/Properties/{1}";
    }
}
