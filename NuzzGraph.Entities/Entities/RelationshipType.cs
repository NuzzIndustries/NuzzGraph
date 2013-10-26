using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BrightstarDB.EntityFramework;
using NuzzGraph.Entities.Attributes;

namespace NuzzGraph.Entities
{
    [Entity]
    [Inherits("SystemNode")]
    public interface IRelationshipType
    {
        long MinConnections { get; set; }
        long MaxConnections { get; set; }

        [InverseProperty("AllowedOutgoingRelationships")]
        ICollection<INodeType> OutgoingFrom { get; }

        [InverseProperty("AllowedIncomingRelationships")]
        ICollection<INodeType> IncomingTo { get; }
    }

    public partial class RelationshipType : IRelationshipType
    {
    }
}
