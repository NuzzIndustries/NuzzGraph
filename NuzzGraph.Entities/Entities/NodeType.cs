using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BrightstarDB.EntityFramework;
using NuzzGraph.Entities.Attributes;

namespace NuzzGraph.Entities
{
    [Entity]
    [Inherits("Type")]
    public interface INodeType
    {
        bool IsAbstract { get; set; }

        void AddProperty(ScalarType type, string name);
        void RemoveProperty(string name);
        void Inherit(NodeType type);
        void Disinherit(NodeType type);
        void AllowOutgoingRelationship(RelationshipType relationshipType, NodeType otherType);
        void DisallowOutgoingRelationship(RelationshipType relationshipType, NodeType otherType);

        Node CreateInstance(IEnumerable<string> parameters);
    }
}
