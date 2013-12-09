using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
using NuzzGraph.Core;
using NuzzGraph.Entities;
using NuzzGraph.Viewer.Utilities;

namespace NuzzGraph.Viewer.UserControls
{
    /// <summary>
    /// Interaction logic for Viewport.xaml
    /// </summary>
    public partial class GraphViewport : UserControl
    {
        public INode CurrentMouseoverNode 
        {
            get { return GraphPane.CurrentMouseoverNode; }
            set { GraphPane.CurrentMouseoverNode = value; } 
        }

        static List<Brush> SimpleBrushes;

        static GraphViewport()
        {
            SimpleBrushes = new List<Brush>();
            SimpleBrushes.Add(Brushes.Aqua);
            SimpleBrushes.Add(Brushes.Black);
            SimpleBrushes.Add(Brushes.Blue);
            SimpleBrushes.Add(Brushes.Violet);
            SimpleBrushes.Add(Brushes.Red);
            SimpleBrushes.Add(Brushes.Green);
            SimpleBrushes.Add(Brushes.Pink);
            SimpleBrushes.Add(Brushes.Orange);
            SimpleBrushes.Add(Brushes.Brown);
            SimpleBrushes.Add(Brushes.Yellow);
            SimpleBrushes.Add(Brushes.ForestGreen);
            SimpleBrushes.Add(Brushes.Purple);
            SimpleBrushes.Add(Brushes.Maroon);
        }

        public GraphViewport()
        {
            InitializeComponent();

            zoom.MouseWheel += zoom_MouseWheel;
            this.Loaded += GraphViewport_Loaded;
        }

        public GraphPane GraphPane { get; set; }

        void GraphViewport_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshVisualGraph();

            zoom.Zoom = 0.2;
            this.InvalidateVisual();
        }

        private void RefreshVisualGraph()
        {
            var nodesToRemove = canvas.Children.Cast<object>().Where(x => x.GetType() == typeof(GraphNode)).ToList();
            foreach (var n in nodesToRemove)
                canvas.Children.Remove((GraphNode)n);

            Random r = new Random();

            var brushes = typeof(Brushes).GetProperties(BindingFlags.Public | BindingFlags.Static)
                .Select(x => (Brush)x.GetValue(null, null))
                .ToList();

            Dictionary<INode, Thumb> NodeThumbs = new Dictionary<INode, Thumb>();

            List<INode> nodes;
            using (var con = ContextFactory.New())
            {
                nodes = con.Nodes.ToList();
            }
            foreach (var node in nodes)
            {
                
                var interfaceName = "I" + node.TypeHandle.Label;
                var interfaceType = EntityUtility.AllCLRTypes.Where(x => x.Name == interfaceName).Single();
                int interfaceIndex = EntityUtility.AllCLRTypes.IndexOf(interfaceType);

                var brush = SimpleBrushes[interfaceIndex];

                int rx = r.Next(2000);
                rx = rx % 2 == 0 ? rx : rx * -1;
                int ry = r.Next(2000);
                ry = ry % 2 == 0 ? ry : ry * -1;
                NodeThumbs[node] = CreateAndAddVisualNode(rx, ry, brush, node);
            }

            foreach (var node in nodes)
            {
                var thumb = NodeThumbs[node];
            }
        }

        private Thumb CreateAndAddVisualNode(int x, int y, Brush color, INode node)
        {
            //Add thumb
            var thumb = new GraphNode()
            {
                Background = color,
                Width = 200,
                Height = 200,
                RepresentedNode = node
            };
            thumb.Tag = node.Label;

            Canvas.SetLeft(thumb, x);
            Canvas.SetTop(thumb, y);
            canvas.Children.Add(thumb);
            thumb.DragStarted += new DragStartedEventHandler(thumb_DragStarted);
            thumb.DragCompleted += new DragCompletedEventHandler(thumb_DragCompleted);
            thumb.DragDelta += new DragDeltaEventHandler(thumb_DragDelta);
            thumb.MouseEnter += thumb_MouseEnter;

            return thumb;
        }


        void zoom_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            decimal delta = (decimal)e.Delta / 120m / 20m;
            zoom.Zoom += ((double)delta * zoom.Zoom);
            if (zoom.Zoom > 2.0)
                zoom.Zoom = 2.0;
            if (zoom.Zoom < 0.1)
                zoom.Zoom = 0.1;
        }


        void thumb_MouseEnter(object sender, MouseEventArgs e)
        {
            this.CurrentMouseoverNode = ((GraphNode)sender).RepresentedNode;
        }

        void thumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            e.Handled = true;
            var th = (Thumb)sender;
            Canvas.SetLeft(th, Canvas.GetLeft(th) + e.HorizontalChange);
            Canvas.SetTop(th, Canvas.GetTop(th) + e.VerticalChange);
            /*Canvas.SetLeft(th, Math.Max(0, Canvas.GetLeft(th) + e.HorizontalChange));
            Canvas.SetTop(th, Math.Max(0, Canvas.GetTop(th) + e.VerticalChange));
            Canvas.SetLeft(th, Math.Min(canvas.ActualWidth - th.Width, Canvas.GetLeft(th)));
            Canvas.SetTop(th, Math.Min(canvas.ActualHeight - th.Width, Canvas.GetTop(th)));*/
        }

        void thumb_DragCompleted(object sender, DragCompletedEventArgs e)
        {
        }

        void thumb_DragStarted(object sender, DragStartedEventArgs e)
        {
        }
    }
}
