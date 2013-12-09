using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BrightstarDB.EntityFramework;
using NuzzGraph.Entities.Attributes;

namespace NuzzGraph.Entities
{
    [Entity]
    [Inherits("Type")]
    public interface IScalarType : IType
    {
        ICollection<PropertyDefinition> PropertiesContainingType { get; }
    }

    public partial class ScalarType : IScalarType
    {
        internal System.Type CLRType
        {
            get
            {
#warning fix this
                return typeof(string);
            }
        }
    }
}
