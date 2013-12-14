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
                edge.Render(dc);
        }

        internal class GraphEdge
        {
            public IRelationshipType @Type { get; set; }
            public INode @From { get; set; }
            public INode @To { get; set; }

            private static Pen PenLine;
            private static Brush Background;
           
            static GraphEdge()
            {
                PenLine = new Pen(Brushes.Black, 5.0);
                Background = GraphViewport.Current.Background;
            }

            private GraphNode FromNode 
            {
                get { return GraphViewport.Current.NodeThumbs[@From]; }
            }

            private GraphNode ToNode
            {
                get { return GraphViewport.Current.NodeThumbs[@To]; }
            }

            public System.Drawing.Rectangle CurrentDrawingBoundary
            {
                get { return FromNode.GetLineBoundaries(ToNode); }
            }

            public void Render(DrawingContext dc)
            {
                //Draw the actual line
                var bounds = CurrentDrawingBoundary;
                int x1 = bounds.X;
                int y1 = bounds.Y;
                int x2 = bounds.X + bounds.Width;
                int y2 = bounds.Y + bounds.Height;
                Point toHead = new Point(x2, y2);
                dc.DrawLine(PenLine, new Point(x1, y1), toHead);

                //Render annotated label
                double centerX = bounds.X + (bounds.Width / 2.0);
                double centerY = bounds.Y + (bounds.Height / 2.0);

                //Draw edge label
                FormattedText tx = new FormattedText(@Type.Label, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, 
                    new Typeface("Courier New"), 32.0, Brushes.White);
                dc.DrawText(tx, new Point(centerX, centerY));

                //Draw Arrow
                double cos = 0.866;
                double sin = 0.500;
                double dx = x1 - x2;
                double dy = y1 - y2;
                double arrowheadLength = 30.0;

                var offset1 = new Vector(.1 * (dx * cos + dy * -sin), .1 * (dx * sin + dy * cos));
                offset1.Normalize();
                offset1 = Vector.Multiply(arrowheadLength, offset1);
                var e1 = Point.Add(toHead, offset1);

                var offset2 = new Vector(.1 * (dx * cos + dy * sin), .1 * (dx * -sin + dy * cos));
                offset2.Normalize();
                offset2 = Vector.Multiply(arrowheadLength, offset2);
                var e2 = Point.Add(toHead, offset2);
                
                dc.DrawLine(PenLine, toHead, e1);
                dc.DrawLine(PenLine, toHead, e2);
            }
        }
    }
}
