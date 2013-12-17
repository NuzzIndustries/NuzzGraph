using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BrightstarDB.EntityFramework;
using NuzzGraph.Core.Attributes;

namespace NuzzGraph.Core.Entities
{
    [Entity]
    [Inherits("Type")]
    public interface IScalarType : IType
    {
        ICollection<IPropertyDefinition> PropertiesContainingType { get; set; }
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
