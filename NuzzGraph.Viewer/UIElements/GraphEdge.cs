using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using NuzzGraph.Core.Entities;
using NuzzGraph.Viewer.UIElements;
using NuzzGraph.Viewer.UserControls;

namespace NuzzGraph.Viewer.UIElements
{
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

        //Gets the base path for the edge line
        public System.Drawing.Rectangle CurrentDrawingBoundary
        {
            get { return GetLineBoundaries(FromNode, ToNode); }
        }

        public void Render(DrawingContext dc)
        {
            //Draw the actual line
            var bounds = CurrentDrawingBoundary;
            int x1 = bounds.X;
            int y1 = bounds.Y;
            Point toHead = GetDestinationEndpoint(bounds);
            dc.DrawLine(PenLine, new Point(x1, y1), toHead);

            //Render annotated label
            double centerX = (bounds.X + toHead.X) / 2.0;
            double centerY = (bounds.Y + toHead.Y) / 2.0;

            //Draw edge label
            FormattedText tx = new FormattedText(@Type.Label, CultureInfo.CurrentCulture, FlowDirection.LeftToRight,
                new Typeface("Courier New"), 32.0, Brushes.White);
            dc.DrawText(tx, new Point(centerX, centerY));

            //Draw Arrow
            double cos = 0.866;
            double sin = 0.500;
            double dx = x1 - toHead.X;
            double dy = y1 - toHead.Y;
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

        private Point GetDestinationEndpoint(System.Drawing.Rectangle baseLineBoundary)
        {
            int x2 = baseLineBoundary.X + baseLineBoundary.Width;
            int y2 = baseLineBoundary.Y + baseLineBoundary.Height;
            
            //Move destination endpoint to the right, or down, depending on which side of the node it is on
            bool moved = true;

            var toNode = ToNode;
            int xdelta = (int)toNode.CenterX - x2;

            if (xdelta > 1)
                y2 += (int)Math.Round(toNode.ActualHeight / 3.0, 0);
            else if (xdelta < -1)
                y2 -= (int)Math.Round(toNode.ActualHeight / 3.0, 0);
            else
                moved = false;
            
            int ydelta = (int)toNode.CenterY + y2;
            if (moved)
                ydelta = 0;

            if (ydelta > 1)
                x2 += (int)Math.Round(toNode.ActualWidth / 3.0, 0);
            else if (ydelta < -1)
                x2 -= (int)Math.Round(toNode.ActualWidth / 3.0, 0);

            return new Point(x2, y2);
        }

        internal System.Drawing.Rectangle GetLineBoundaries(GraphNode node1, GraphNode node2)
        {
            var boundarySrc = node1.Boundary;
            var boundaryDest = node2.Boundary;

            if (boundarySrc.IntersectsWith(boundaryDest))
            {
                return System.Drawing.Rectangle.Empty;
            }

            bool xIntersects = 
                (
                    boundarySrc.X <= boundaryDest.X && 
                    boundarySrc.X + boundarySrc.Width >= boundaryDest.X
                )
                || 
                (
                    boundaryDest.X <= boundarySrc.X && 
                    boundaryDest.X + boundaryDest.Width >= boundarySrc.X
                );
            bool yIntersects = 
                (
                    boundarySrc.Y <= boundaryDest.Y && 
                    boundarySrc.Y + boundarySrc.Height >= boundaryDest.Y
                )
                || 
                (
                    boundaryDest.Y <= boundarySrc.Y && 
                    boundaryDest.Y + boundaryDest.Height >= boundarySrc.Y
                );

            int x1, x2, y1, y2;

            if (xIntersects)
            {
                x1 = boundarySrc.X + boundarySrc.Width / 2;
                x2 = boundaryDest.X + boundaryDest.Width / 2;
                if (boundarySrc.Y + boundarySrc.Height <= boundaryDest.Y)
                {
                    y1 = boundarySrc.Y + boundarySrc.Height;
                    y2 = boundaryDest.Y;
                }
                else
                {
                    y1 = boundarySrc.Y;
                    y2 = boundaryDest.Y + boundaryDest.Height;
                }
            }
            else if (yIntersects)
            {
                y1 = boundarySrc.Y + boundarySrc.Height / 2;
                y2 = boundaryDest.Y + boundaryDest.Height / 2;
                if (boundarySrc.X + boundarySrc.Width <= boundaryDest.X)
                {
                    x1 = boundarySrc.X + boundarySrc.Width;
                    x2 = boundaryDest.X;
                }
                else
                {
                    x1 = boundarySrc.X;
                    x2 = boundaryDest.X + boundaryDest.Width;
                }
            }
            else
            {
                if (boundarySrc.Y + boundarySrc.Height <= boundaryDest.Y)
                {
                    y1 = boundarySrc.Y + boundarySrc.Height / 2;
                    y2 = boundaryDest.Y;
                    if (boundarySrc.X + boundarySrc.Width <= boundaryDest.X)
                    {
                        x1 = boundarySrc.X + boundarySrc.Width;
                    }
                    else
                    {
                        x1 = boundarySrc.X;
                    }
                    x2 = boundaryDest.X + boundaryDest.Width / 2;
                }
                else
                {
                    y1 = boundarySrc.Y + boundarySrc.Height / 2;
                    y2 = boundaryDest.Y + boundaryDest.Height;
                    if (boundarySrc.X + boundarySrc.Width <= boundaryDest.X)
                        x1 = boundarySrc.X + boundarySrc.Width;
                    else
                        x1 = boundarySrc.X;
                    x2 = boundaryDest.X + boundaryDest.Width / 2;
                }
            }

            return new System.Drawing.Rectangle(x1, y1, x2 - x1, y2 - y1);
        }
    }
}
