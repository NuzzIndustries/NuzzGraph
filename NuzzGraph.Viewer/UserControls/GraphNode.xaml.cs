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
using NuzzGraph.Core.Entities;

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
    }
}
