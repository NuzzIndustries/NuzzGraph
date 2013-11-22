using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NuzzGraph.Entities.Attributes
{
    public class CustomConstructorAttribute : Attribute
    {
        public CustomConstructorAttribute() : base() { }
    }
}
