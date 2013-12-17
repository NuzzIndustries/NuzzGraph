using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BrightstarDB.EntityFramework;
using NuzzGraph.Core.Attributes;

namespace NuzzGraph.Core.Entities
{
    [Entity]
    [Inherits("Node")]
    public interface ISystemNode : INode
    {
    }

    public partial class SystemNode : Node, ISystemNode
    {
    }
}
