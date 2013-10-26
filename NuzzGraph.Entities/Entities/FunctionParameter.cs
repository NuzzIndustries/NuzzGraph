using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BrightstarDB.EntityFramework;
using NuzzGraph.Entities.Attributes;

namespace NuzzGraph.Entities
{
    [Entity]
    [Inherits("PropertyDefinition")]
    public interface IFunctionParameter
    {
        [InverseProperty("Parameters")]
        IFunction DeclaringFunction { get; }

        INode DefaultValue { get; }

        IType ParameterType { get; }
    }

    public partial class FunctionParameter : IFunctionParameter
    {
    }
}
