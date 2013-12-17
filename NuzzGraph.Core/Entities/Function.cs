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

        INodeType DeclaringType { get; set; }

        ICollection<IFunctionParameter> Parameters { get; set; }

        IFunction Overrides { get; set; }

        [InverseProperty("Overrides")]
        ICollection<IFunction> OverridenBy { get; set; }
    }

    public partial class Function : IFunction
    {
    }
}
