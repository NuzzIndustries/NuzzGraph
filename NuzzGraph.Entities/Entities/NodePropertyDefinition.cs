﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BrightstarDB.EntityFramework;
using NuzzGraph.Entities.Attributes;

namespace NuzzGraph.Entities
{
    [Entity]
    [Inherits("PropertyDefinition")]
    public interface INodePropertyDefinition : IPropertyDefinition
    {
        INodeType DeclaringType { get; set;  }
        string InternalUri { get; set; }

       
    }

    public partial class NodePropertyDefinition : INodePropertyDefinition
    {
    }
}
