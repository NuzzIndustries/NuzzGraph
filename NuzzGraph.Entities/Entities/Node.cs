using System.Linq;
using BrightstarDB.Client;
using BrightstarDB.EntityFramework;
using NuzzGraph.Core;

namespace NuzzGraph.Entities
{
    [Entity]
    public interface INode
    {
        /// <summary>
        /// Get the persistent identifier for this entity
        /// </summary>
        string Id { get; }
        NodeType TypeHandle { get; set; }

        string Label { get; set; }

        Node Get();
    }

    public partial class Node : BrightstarEntityObject, INode
    {
        public Node Get()
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
            if (context == null)
                context = ContextFactory.New();

            if (EntityUtility.NodeTypesInitialized)
                this.TypeHandle = (NodeType)((GraphContext)context).NodeTypes.Where(x => x.Label == this.GetType().Name).Single();
            if (EntityUtility.IsSeedMode)
                EntityUtility.AddNodeToContext((GraphContext)context, (INode)this);
        }
    }
}
