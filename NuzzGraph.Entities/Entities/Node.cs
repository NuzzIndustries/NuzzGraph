using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BrightstarDB.EntityFramework;
using BrightstarDB.Client;
using NuzzGraph.Entities.Attributes;
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
        INodeType TypeHandle { get; set; }

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
                TypeHandle = ((GraphContext)context).NodeTypes.Where(x => x.Label == this.GetType().Name).Single();
            if (EntityUtility.IsSeedMode)
                EntityUtility.AddNodeToContext((GraphContext)context, (INode)this);
        }
    }
}
