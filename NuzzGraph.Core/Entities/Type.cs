using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BrightstarDB.EntityFramework;
using NuzzGraph.Core.Attributes;

namespace NuzzGraph.Core.Entities
{
    [Entity]
    [Inherits("SystemNode")]
    public interface IType : ISystemNode
    {
    }

    public partial class Type : IType
    {
    }
}
