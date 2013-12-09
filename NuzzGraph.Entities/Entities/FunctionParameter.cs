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
    public interface IFunctionParameter : IPropertyDefinition
    {
        [InverseProperty("Parameters")]
        Function DeclaringFunction { get; set; }

        Type ParameterType { get; set; }
    }

    public partial class FunctionParameter : IFunctionParameter
    {
    }
}
