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
    public interface INodeType : IType
    {
        bool IsAbstract { get; set; }

        ICollection<NodeType> SuperTypes { get; set; } //Change to multiple inheritance
        ICollection<RelationshipType> AllowedOutgoingRelationships { get; set; }
        ICollection<RelationshipType> AllowedIncomingRelationships { get; set; }

        [InverseProperty("DeclaringType")]
        ICollection<NodePropertyDefinition> Properties { get; set; }

        [InverseProperty("DeclaringType")]
        ICollection<Function> Functions { get; set; }

        [InverseProperty("SuperTypes")]
        ICollection<NodeType> SubTypes { get; set; }

        [InverseProperty("TypeHandle")]
        ICollection<Node> AllNodes { get; set; }

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
        //Pretty slow (inaccurate?) in current implementation
        //Switch to use lower level RDF functions
        public List<INodeType> InheritanceChain
        {
            get
            {
                List<INodeType> list = new List<INodeType>();
                foreach (var type in SuperTypes)
                {
                    var _type = (NodeType)type;
                    list.AddRange(_type.InheritanceChain);
                    list.Add(type);
                }
                list = list.Distinct().ToList();
                return list;
            }
        }

        public List<INodePropertyDefinition> AllProperties
        {
            get
            {
                var list = new List<INodePropertyDefinition>();
                foreach (var type in InheritanceChain)
                {
                    var _type = (NodeType)type;
                    list.AddRange(_type.Properties.ToList());
                }
                list.AddRange(this.Properties.ToList());
                return list;
            }
        }

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

        internal System.Type _UnderlyingType
        {
            get
            {
                return EntityUtility.CLRTypeMap[this];
            }

        }
    }
}
