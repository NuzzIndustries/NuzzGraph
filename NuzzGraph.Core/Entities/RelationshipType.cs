using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BrightstarDB.Client;
using BrightstarDB.EntityFramework;
using NuzzGraph.Core;
using NuzzGraph.Entities.Attributes;

namespace NuzzGraph.Entities
{
    [Entity]
    [Inherits("SystemNode")]
    public interface IRelationshipType : ISystemNode
    {
        bool SupportsMany { get; set; }

        [InverseProperty("AllowedOutgoingRelationships")]
        INodeType OutgoingFrom { get; set; }

        [InverseProperty("AllowedIncomingRelationships")]
        INodeType IncomingTo { get; set; }
    }

    public partial class RelationshipType : IRelationshipType
    {
        private string _uri = null;
        private string _URI
        {
            get
            {
                if (_uri == null)
                    _uri = DataUtility.GetUri(this);
                return _uri;
            }
        }

        internal IEnumerable<INode> _GetRelatedNodesInverse(INode n)
        {
            if (n.TypeHandle.Id != IncomingTo.Id)
            {
                throw new TypeAccessException("Type " + n.TypeHandle.Label + " does not contain inverse relationship " + this.Label);
            }

            List<string> _objectIds;

            using (var core = NuzzGraph.Core.ContextFactory.GetCore())
            {
                
                //Get underlying data object for this node
                var nodeDataObject = core.GetDataObject(n.Id);

                //Get related object IDs
                _objectIds = nodeDataObject.GetPropertyValues(_URI).Select(x => ((IDataObject)x).Identity).ToList();
            }

            if (_objectIds.Count == 0)
                return new List<INode>();

            using (var con = ContextFactory.New())
            {
                var nodes = _objectIds.Select(id => con.Nodes.Where(@node => @node.Id == id).SingleOrDefault()).Where(x => x != null);
                return nodes;
            }
        }

        internal IEnumerable<INode> _GetRelatedNodes(INode n)
        {
            if (n.TypeHandle.Id != OutgoingFrom.Id)
            {
                throw new TypeAccessException("Type " + n.TypeHandle.Label + " does not contain relationship " + this.Label);
            }

            List<string> _objectIds;

            using (var core = NuzzGraph.Core.ContextFactory.GetCore())
            {
                //Get underlying data object for this node
                var nodeDataObject = core.GetDataObject(n.Id);
                _objectIds = nodeDataObject.GetPropertyValues(_URI).Select(x => ((IDataObject)x).Identity).ToList();
            }

            if (_objectIds.Count == 0)
                return new List<INode>();

            using (var con = ContextFactory.New())
            {
                var nodes = _objectIds.Select(id => con.Nodes.Where(@node => @node.Id == id).SingleOrDefault()).Where(x => x != null);
                return nodes;
            }
        }

        protected override void OnPropertyChanged(string propertyName)
        {
            base.OnPropertyChanged(propertyName);
        }
    }
}
