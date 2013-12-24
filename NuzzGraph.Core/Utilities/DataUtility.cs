using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BrightstarDB.Client;
using BrightstarDB.EntityFramework.Query;
using NuzzGraph.Core.Entities;

namespace NuzzGraph.Core.Utilities
{
    public static class DataUtility
    {
        internal static string GetUri(string type, string property)
        {
            return string.Format(Constants.NodeUriFormatBase, type, property);
        }

        internal static string GetUri(NodeType type, string property)
        {
            return GetUri(type.Label, property);
        }

        internal static string GetUri(RelationshipType relationshipType)
        {
            return GetUri(relationshipType.OutgoingFrom.Label, relationshipType.Label);
        }

        internal static string GetUri(NodePropertyDefinition property)
        {
            return GetUri(property.DeclaringType.Label, property.Label);
        }

        internal static IEnumerable<INode> ExecuteNodeQuery(this NuzzGraphContext context, string sparql)
        {
            //Add prefix statements
            string prefixes = @"
                PREFIX id: <http://www.brightstardb.com/.well-known/genid/> 
                PREFIX prop: <http://www.nuzzgraph.com/Entities/Node/Properties/>
                ";
            sparql = prefixes + sparql;

            //Build and run query
            var q = new SparqlQueryContext(sparql, null, null, null, null, null);
            var results = context.ExecuteQuery<INode>(q)
                .Where(x => x.TypeHandle != null)
                .Select(@n => EntityUtility.RebindEntity((Node)@n));
            
            return results;
        }
    }
}
