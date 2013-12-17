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
    public interface IPropertyDefinition : ISystemNode
    {
        IScalarType PropertyType { get; set; }
        bool IsNullable { get; set; }
        object DefaultValue { get; set; }
    }

    public partial class PropertyDefinition : IPropertyDefinition
    {
        void SetDefaultValue(object value)
        {

        }
    }
}
