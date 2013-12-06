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

            var set = new BrightstarEntitySet<INodeType>(context);
            var set2 = set.Select(x => (NodeType)x);

            if (EntityUtility.NodeTypesInitialized && this.GetType() == typeof(NodeType))
                TypeHandle = set2.Where(x => x.Label == this.GetType().Name).Single();
            //TypeHandle = ((GraphContext)context).NodeTypes.Where(x => x.Label == this.GetType().Name).Single();
            if (EntityUtility.IsSeedMode)
                EntityUtility.AddNodeToContext((GraphContext)context, (INode)this);
        }
    }
}
