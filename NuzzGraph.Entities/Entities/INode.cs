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

        // TODO: Add other property references here
    }
}
