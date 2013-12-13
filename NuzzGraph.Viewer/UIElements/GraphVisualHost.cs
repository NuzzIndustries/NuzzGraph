using System;
using System.Collections.Generic;
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

            // Add the event handler for MouseLeftButtonUp.
            this.MouseLeftButtonUp += new MouseButtonEventHandler(MyVisualHost_MouseLeftButtonUp);
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

        void MyVisualHost_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private DrawingVisual CreateDrawingVisualRectangle()
        {
            DrawingVisual drawingVisual = new DrawingVisual();

            // Retrieve the DrawingContext in order to create new drawing content.
            DrawingContext drawingContext = drawingVisual.RenderOpen();

            // Create a rectangle and draw it in the DrawingContext.
            Rect rect = new Rect(new System.Windows.Point(160, 100), new System.Windows.Size(320, 80));
            drawingContext.DrawRectangle(System.Windows.Media.Brushes.LightBlue, (System.Windows.Media.Pen)null, rect);

            // Persist the drawing content.
            drawingContext.Close();

            return drawingVisual;
        }

        private Visual GetPath()
        {
            TestUIElement el = new TestUIElement();

            int h = 200;
            int w = 200;

            Random random = new Random();
            double r;

            StreamGeometry g = new StreamGeometry();
            StreamGeometryContext cr = g.Open();
            cr.BeginFigure(new Point(0, 0), false, false);
            for (int i = 0; i < w; i++)
            {
                // ugly calculations below to get the signal centered in container
                r = (random.NextDouble() - 0.5) * h - h / 2;
                cr.LineTo(new Point(i, r), true, false);
            }
            cr.Close();
            g.Freeze();

            DrawingContext crx = el.Path.RenderOpen();
            Pen p = new Pen(Brushes.Black, 2);
            crx.DrawGeometry(null, p, g);

            // Ellipse included for "visual debugging"
            crx.DrawEllipse(Brushes.Red, p, new Point(50, 50), 45, 20);

            crx.Close();

            this.AddVisualChild(el.Path);
            return el;
        }

        private DrawingVisual GetDraggable()
        {
            DrawingVisual drawingVisual = new DrawingVisual();
            var cx = drawingVisual.RenderOpen();

            //cx.DrawGeometry(Brushes.Red, null, e.RenderedGeometry);
            cx.DrawEllipse(Brushes.LightBlue, null, new System.Windows.Point(160, 100), 320, 80);

            cx.Close();

            return drawingVisual;
        }

        private void RedrawEdges(DrawingContext dc)
        {
            if (GraphEdges == null)
                return;

            Pen p = new Pen(Brushes.Black, 5.0);
            
            foreach (var edge in GraphEdges)
            {
                var fromThumb = Viewport.NodeThumbs[edge.From];
                var toThumb = Viewport.NodeThumbs[edge.To];

                var bounds = fromThumb.GetLineBoundaries(toThumb);

                dc.DrawLine(p, new Point(bounds.X, bounds.Y), new Point(bounds.X + bounds.Width, bounds.Y + bounds.Height));
            }
        }

        internal class GraphEdge
        {
            public IRelationshipType @Type { get; set; }
            public INode @From { get; set; }
            public INode @To { get; set; }
        }


    }
}
