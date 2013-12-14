using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using NuzzGraph.Entities;

namespace NuzzGraph.Viewer.UserControls
{
    /// <summary>
    /// Interaction logic for GraphNode.xaml
    /// </summary>
    public partial class GraphNode : Thumb
    {
        public INode RepresentedNode { get; set; }

        public GraphNode()
        {
            InitializeComponent();
        }

        public double PositionLeft
        {
            get 
            {
                return Canvas.GetLeft(this);
            }
        }

        public double PositionRight
        {
            get
            {
                return PositionLeft + this.ActualWidth;
            }
        }

        public double PositionTop
        {
            get
            {
                return Canvas.GetTop(this);
            }
        }

        public double PositionBottom
        {
            get
            {
                return PositionTop + this.ActualHeight;
            }
        }

        public double CenterX
        {
            get
            {
                return (PositionLeft + PositionRight) / 2.0;
            }
        }

        public double CenterY
        {
            get
            {
                return (PositionTop + PositionBottom) / 2.0;
            }
        }

        public System.Drawing.Rectangle Boundary
        {
            get
            {
                return new System.Drawing.Rectangle((int)this.PositionLeft, (int)this.PositionTop, (int)this.ActualWidth, (int)this.ActualHeight);
            }
        }

        internal System.Drawing.Rectangle GetLineBoundaries(GraphNode otherNode)
        {
            var boundarySrc = this.Boundary;
            var boundaryDest = otherNode.Boundary;
            
            if (boundarySrc.IntersectsWith(boundaryDest))
            {
                return System.Drawing.Rectangle.Empty;
            }

            bool xIntersects = (boundarySrc.X <= boundaryDest.X && boundarySrc.X + boundarySrc.Width >= boundaryDest.X)
                || (boundaryDest.X <= boundarySrc.X && boundaryDest.X + boundaryDest.Width >= boundarySrc.X);
            bool yIntersects = boundarySrc.Y <= boundaryDest.Y && boundarySrc.Y + boundarySrc.Height >= boundaryDest.Y
                || (boundaryDest.Y <= boundarySrc.Y && boundaryDest.Y + boundaryDest.Height >= boundarySrc.Y);

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
