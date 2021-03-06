﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NuzzGraph.Core.Attributes
{
    public class InheritsAttribute : Attribute
    {
        public string InheritedClass { get; set; }

        public InheritsAttribute(string inheritedClass)
        {
            InheritedClass = inheritedClass;
        }
    }
}
