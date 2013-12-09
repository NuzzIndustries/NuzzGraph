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
    public interface IFunction : ISystemNode
    {
        string FunctionBody { get; set; }

        NodeType DeclaringType { get; set; }

        ICollection<FunctionParameter> Parameters { get; set; }

        Function Overrides { get; set; }

        [InverseProperty("Overrides")]
        ICollection<Function> OverridenBy { get; }
    }

    public partial class Function : IFunction
    {
    }
}
