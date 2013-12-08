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

            var set1 = new DirectEntitySet<NodeType>(context);
            var q1 = set1.Where(x => x.Label == this.GetType().Name).ToList();

            if (EntityUtility.NodeTypesInitialized)
            {
                var typenodes = ((GraphContext)context)._NodeTypes.Where(x => x.Label == this.GetType().Name).ToList();
                var set2 = ((GraphContext)context).NodeTypes.Cast<NodeType>();
                var typeNodes3 = set2.Where(x => x.Label == this.GetType().Name).ToList();
                var typenodesFromInterface = ((GraphContext)context).NodeTypes.Where(x => x.Label == this.GetType().Name).ToList(); ;
            }
            if (EntityUtility.IsSeedMode)
                EntityUtility.AddNodeToContext((GraphContext)context, (INode)this);
        }
    }
}
