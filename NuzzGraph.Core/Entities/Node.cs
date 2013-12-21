using System.Collections.Generic;
using System.Linq;
using BrightstarDB.Client;
using BrightstarDB.EntityFramework;
using NuzzGraph.Core;
using NuzzGraph.Core.Utilities;

namespace NuzzGraph.Core.Entities
{
    [Entity]
    public interface INode
    {
        /// <summary>
        /// Get the persistent identifier for this entity
        /// </summary>
        string Id { get; }

        INodeType TypeHandle { get; set; }

        string Label { get; set; }

        INode Get();
    }

    public partial class Node : BrightstarEntityObject, INode
    {
        internal List<INode> _GetRelatedNodes()
        {
            using (var core = ContextFactory.GetCore())
            {
                var sparql = @"
                    PREFIX id: <http://www.brightstardb.com/.well-known/genid/> 
                    PREFIX prop: <http://www.nuzzgraph.com/Entities/Node/Properties/>

                    SELECT DISTINCT *
                    WHERE { id:fa3d9836-a73c-4600-9888-d10066ef48c6 prop:TypeHandle ?Value }";
                var results = core.BindDataObjectsWithSparql(sparql).ToList();
                
                
            }

            var _nodes = new List<INode>();
            foreach (var @rel in this.TypeHandle.AllowedOutgoingRelationships)
            {
                var r = (RelationshipType)@rel;
                _nodes.AddRange(r._GetRelatedNodes(this));
            }
            return _nodes;
        }

        public INode Get()
        {
            return this;
        }

        public string GetIdentity_Wrapper()
        {
            return GetIdentity();
        }

        public IDataObject GetRawObject()
        {
            var core = ContextFactory.GetCore();
            var data = core.GetDataObject(this.GetIdentity_Wrapper());
            return data;
        }

        protected override void OnCreated(BrightstarEntityContext context)
        {
            bool flag = false;

            if (context == null)
            {
                context = ContextFactory.New();
                flag = true;
            }

            if (EntityUtility.NodeTypesInitialized)
                this.TypeHandle = (NodeType)((GraphContext)context).NodeTypes.Where(x => x.Label == this.GetType().Name).Single();
            if (EntityUtility.IsSeedMode)
                EntityUtility.AddNodeToContext((GraphContext)context, (INode)this);

            if (flag)
                context.Dispose();
        }

        public override string ToString()
        {
            return TypeHandle.Label + ": " + Label;
        }
    }
}
