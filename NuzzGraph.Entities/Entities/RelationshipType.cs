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
  
        protected override void OnPropertyChanged(string propertyName)
        {
            base.OnPropertyChanged(propertyName);
        }
    }
}
