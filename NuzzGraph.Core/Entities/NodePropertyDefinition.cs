﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BrightstarDB.EntityFramework;
using NuzzGraph.Core;
using NuzzGraph.Core.Attributes;

namespace NuzzGraph.Core.Entities
{
    [Entity]
    [Inherits("PropertyDefinition")]
    public interface INodePropertyDefinition : IPropertyDefinition
    {
        INodeType DeclaringType { get; set; }
        string InternalUri { get; set; }
        object GetValue(INode node);

    }

    public partial class NodePropertyDefinition : INodePropertyDefinition
    {
        public object GetValue(INode node)
        {
            var _node = (Node)node;
            var data = _node.GetRawObject();
            var pvalue = data.GetPropertyValues(InternalUri).SingleOrDefault();
            return pvalue;
            //var literals = _node.GetRelatedLiteralPropertiesCollection<object>(this.InternalUri);
            //return literals.FirstOrDefault();
        }
    }
}
