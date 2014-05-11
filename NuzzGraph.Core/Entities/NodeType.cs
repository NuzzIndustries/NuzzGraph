using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BrightstarDB.EntityFramework;
using NuzzGraph.Core.Utilities;
using NuzzGraph.Core.Attributes;

namespace NuzzGraph.Core.Entities
{
    [Entity]
    [Inherits("Type")]
    public interface INodeType : IType
    {
        bool IsAbstract { get; set; }

        ICollection<INodeType> SuperTypes { get; set; } //Change to multiple inheritance
        ICollection<IRelationshipType> AllowedOutgoingRelationships { get; set; }
        ICollection<IRelationshipType> AllowedIncomingRelationships { get; set; }

        [InverseProperty("DeclaringType")]
        ICollection<INodePropertyDefinition> Properties { get; set; }

        [InverseProperty("DeclaringType")]
        ICollection<IFunction> Functions { get; set; }

        [InverseProperty("SuperTypes")]
        ICollection<INodeType> SubTypes { get; set; }

        [InverseProperty("TypeHandle")]
        ICollection<INode> AllNodes { get; set; }

        void AddProperty(IScalarType type, string name);
        void RemoveProperty(string name);
        void Inherit(INodeType type);
        void Disinherit(INodeType type);
        void AllowOutgoingRelationship(IRelationshipType relationshipType, INodeType otherType);
        void DisallowOutgoingRelationship(IRelationshipType relationshipType, INodeType otherType);

        INode CreateInstance(IEnumerable<string> parameters);
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

        public void AddProperty(IScalarType type, string name)
        {
            throw new NotImplementedException();
        }

        public void RemoveProperty(string name)
        {
            throw new NotImplementedException();
        }

        public void Inherit(INodeType type)
        {
            throw new NotImplementedException();
        }

        public void Disinherit(INodeType type)
        {
            throw new NotImplementedException();
        }

        public void AllowOutgoingRelationship(IRelationshipType relationshipType, INodeType otherType)
        {
            throw new NotImplementedException();
        }

        public void DisallowOutgoingRelationship(IRelationshipType relationshipType, INodeType otherType)
        {
            throw new NotImplementedException();
        }

        public INode CreateInstance(IEnumerable<string> parameters)
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
