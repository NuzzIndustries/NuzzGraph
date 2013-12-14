using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using NuzzGraph.Entities;
using NuzzGraph.Viewer.UIElements;

namespace NuzzGraph.Viewer.UserControls
{
    //For DrawingVisual objects *only*
    public class GraphVisualHost : FrameworkElement
    {
        // Create a collection of child visual objects.
        private VisualCollection _children;

        internal List<GraphEdge> GraphEdges { get; set; }
        
        internal GraphViewport Viewport { get; set; }

        public GraphVisualHost()
        {
            _children = new VisualCollection(this);
        }

        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            RedrawEdges(dc);
        }

        protected override int VisualChildrenCount
        {
            get
            {
                return _children.Count;
            }
        }

        protected override Visual GetVisualChild(int index)
        {
            return _children[index];
        }

        private void RedrawEdges(DrawingContext dc)
        {
            if (GraphEdges == null)
                return;

            Pen p = new Pen(Brushes.Black, 5.0);
            
            foreach (var edge in GraphEdges)
                edge.Render(dc);
        }

        
    }
}
