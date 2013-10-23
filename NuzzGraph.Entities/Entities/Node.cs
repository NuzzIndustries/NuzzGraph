using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BrightstarDB.EntityFramework;

namespace NuzzGraph.Entities
{
    [Entity]
    public interface INode
    {
        /// <summary>
        /// Get the persistent identifier for this entity
        /// </summary>
        string Id { get; }

        Node Get();
    }

    public partial class Node : BrightstarEntityObject, INode 
    {
        public int a()
        {
            throw new NotImplementedException();
        }


        public Node Get()
        {
            throw new NotImplementedException();
        }
    }
}
