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
        bool IsAbstract { get; }

        INodeType SuperTypes { get; } //Change to multiple inheritance
        ICollection<IRelationshipType> AllowedOutgoingRelationships { get; }
        ICollection<IRelationshipType> AllowedIncomingRelationships { get; }
        ICollection<INodePropertyDefinition> Properties { get; }

        [InverseProperty("DeclaringType")]
        ICollection<IFunction> Functions { get; }

        [InverseProperty("SuperTypes")]
        ICollection<INodeType> SubTypes { get; }

        [InverseProperty("Type")]
        ICollection<INode> AllNodes { get; }
        

        void AddProperty(ScalarType type, string name);
        void RemoveProperty(string name);
        void Inherit(NodeType type);
        void Disinherit(NodeType type);
        void AllowOutgoingRelationship(RelationshipType relationshipType, NodeType otherType);
        void DisallowOutgoingRelationship(RelationshipType relationshipType, NodeType otherType);

        Node CreateInstance(IEnumerable<string> parameters);
    }

    public partial class NodeType : INodeType
    {
        public void AddProperty(ScalarType type, string name)
        {
            throw new NotImplementedException();
        }

        public void RemoveProperty(string name)
        {
            throw new NotImplementedException();
        }

        public void Inherit(NodeType type)
        {
            throw new NotImplementedException();
        }

        public void Disinherit(NodeType type)
        {
            throw new NotImplementedException();
        }

        public void AllowOutgoingRelationship(RelationshipType relationshipType, NodeType otherType)
        {
            throw new NotImplementedException();
        }

        public void DisallowOutgoingRelationship(RelationshipType relationshipType, NodeType otherType)
        {
            throw new NotImplementedException();
        }

        public Node CreateInstance(IEnumerable<string> parameters)
        {
            throw new NotImplementedException();
        }
    }
}
