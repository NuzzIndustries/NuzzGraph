using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BrightstarDB.Client;
using NuzzGraph.Core.Entities;

namespace NuzzGraph.Core.Utilities
{
    internal static class DataUtility
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


    }
}
