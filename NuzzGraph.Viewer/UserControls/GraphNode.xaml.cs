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
                return PositionLeft + this.Width;
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
                return Canvas.GetBottom(this);
            }
        }

        public double CenterX
        {
            get
            {
                return (PositionLeft + PositionRight / 2.0);
            }
        }

        public double CenterY
        {
            get
            {
                return (PositionTop + PositionBottom / 2.0);
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
            var rSource = this.Boundary;
            var rDest = otherNode.Boundary;
            
            if (rSource.IntersectsWith(rDest))
            {
                return System.Drawing.Rectangle.Empty;
            }

            bool xIntersect = (rSource.X <= rDest.X && rSource.X + rSource.Width >= rDest.X)
                || (rDest.X <= rSource.X && rDest.X + rDest.Width >= rSource.X);
            bool yIntersect = rSource.Y <= rDest.Y && rSource.Y + rSource.Height >= rDest.Y
                || (rDest.Y <= rSource.Y && rDest.Y + rDest.Height >= rSource.Y);

            int x1, x2, y1, y2;

            if (xIntersect)
            {
                x1 = rSource.X + rSource.Width / 2;
                x2 = rDest.X + rDest.Width / 2;
                if (rSource.Y + rSource.Height <= rDest.Y)
                {
                    y1 = rSource.Y + rSource.Height;
                    y2 = rDest.Y;
                }
                else
                {
                    y1 = rSource.Y;
                    y2 = rDest.Y + rDest.Height;
                }
            }
            else if (yIntersect)
            {
                y1 = rSource.Y + rSource.Height / 2;
                y2 = rDest.Y + rDest.Height / 2;
                if (rSource.X + rSource.Width <= rDest.X)
                {
                    x1 = rSource.X + rSource.Width;
                    x2 = rDest.X;
                }
                else
                {
                    x1 = rSource.X;
                    x2 = rDest.X + rDest.Width;
                }
            }
            else
            {
                if (rSource.Y + rSource.Height <= rDest.Y)
                {
                    y1 = rSource.Y + rSource.Height / 2;
                    y2 = rDest.Y;
                    if (rSource.X + rSource.Width <= rDest.X)
                    {
                        x1 = rSource.X + rSource.Width;
                    }
                    else
                    {
                        x1 = rSource.X;
                    }
                    x2 = rDest.X + rDest.Width / 2;
                }
                else
                {
                    y1 = rSource.Y + rSource.Height / 2;
                    y2 = rDest.Y + rDest.Height;
                    if (rSource.X + rSource.Width <= rDest.X)
                        x1 = rSource.X + rSource.Width;
                    else
                        x1 = rSource.X;
                    x2 = rDest.X + rDest.Width / 2;
                }
            }

            return new System.Drawing.Rectangle(x1, y1, x2 - x1, y2 - y1);
        }

    }
}
