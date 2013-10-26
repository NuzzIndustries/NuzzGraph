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
    public interface IFunction
    {
        string FunctionBody { get; set; }

        INodeType DeclaringType { get; }

        ICollection<IFunctionParameter> Parameters { get; }
    }

    public partial class Function : IFunction
    {
    }
}
