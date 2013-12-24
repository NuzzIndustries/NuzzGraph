using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BrightstarDB.Client;
using BrightstarDB.EntityFramework;
using BrightstarDB.EntityFramework.Query;
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
        internal IEnumerable<INode> _GetRelatedNodes()
        {
            var sparql = string.Format(@"
                SELECT DISTINCT ?Property ?Value
                WHERE 
                {{ 
                    <{0}> ?Property ?Value 
                }}", this.Identity);
            // FILTER(STRSTARTS(STR(?Value), ""http://www.brightstardb.com/.well-known/genid/""))

            var nodes = Context.ExecuteNodeQuery(sparql);
            return nodes;
        }

        internal IEnumerable<T> _GetRelatedNodes<T>()
            where T : INode
        {
            if (!EntityUtility.CLRTypeMapInverse.ContainsKey(typeof(T)))
                throw new InvalidOperationException("Cannot use generic variant of _GetRelatedNodes on this type.");

            var typeNode = EntityUtility.CLRTypeMapInverse[typeof(T)];

            return _GetRelatedNodes(typeNode).Select(x => (T)x);
        }

        internal IEnumerable<INode> _GetRelatedNodes(INodeType typeNode)
        {
            var sparql = string.Format(@"
                SELECT DISTINCT ?Property ?Value
                WHERE 
                {{ 
                    <{0}> ?Property ?Value .
                    {{
                        SELECT ?Value
                        WHERE
                        {{
                            ?Value <http://www.nuzzgraph.com/Entities/Node/Properties/TypeHandle> <{1}>
                        }}
                    }}
                }}", this.Identity, typeNode.Id);

            var nodes = Context.ExecuteNodeQuery(sparql);
            return nodes;
        }

        internal IEnumerable<INode> _GetRelatedNodesInverse()
        {
            var sparql = string.Format(@"
                SELECT DISTINCT ?Node ?Property
                WHERE 
                {{ 
                    ?Node ?Property <{0}>
                }}", this.Identity);

            var nodes = Context.ExecuteNodeQuery(sparql);
            return nodes;
        }

        internal IEnumerable<T> _GetRelatedNodesInverse<T>()
            where T : INode
        {
            if (!EntityUtility.CLRTypeMapInverse.ContainsKey(typeof(T)))
                throw new InvalidOperationException("Cannot use generic variant of _GetRelatedNodes on this type.");

            var typeNode = EntityUtility.CLRTypeMapInverse[typeof(T)];

            return _GetRelatedNodesInverse(typeNode).Select(x => (T)x);
        }

        internal IEnumerable<INode> _GetRelatedNodesInverse(INodeType typeNode)
        {
            var sparql = string.Format(@"
                SELECT DISTINCT ?Node ?Property
                WHERE 
                {{ 
                    ?Node ?Property <{0}> .
                    {{
                        SELECT ?Value
                        WHERE
                        {{
                            ?Node <http://www.nuzzgraph.com/Entities/Node/Properties/TypeHandle> <{1}>
                        }}
                    }}
                }}", this.Identity, typeNode.Id);

            var nodes = Context.ExecuteNodeQuery(sparql);
            return nodes;
        }

        public INode Get()
        {
            return this;
        }

        public IDataObject GetRawObject()
        {
            var core = ContextFactory.GetCore();
            var data = core.GetDataObject(this.GetIdentity());
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


        public new string GetIdentity()
        {
            return base.GetIdentity();
        }

        public new NuzzGraphContext Context
        {
            get
            {
                return (NuzzGraphContext)base.Context;
            }
        }

        public override string ToString()
        {
            return TypeHandle.Label + ": " + Label;
        }
    }
}
