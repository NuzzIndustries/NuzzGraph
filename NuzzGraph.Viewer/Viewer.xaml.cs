using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using NuzzGraph.Viewer.UIElements;
using System.Windows.Controls.Primitives;
using NuzzGraph.Entities;
using NuzzGraph.Core;
using System.IO;
using Elysium;
using System.Diagnostics;
using Elysium.Notifications;
using System.Reflection;
using NuzzGraph.Viewer.Utilities;
using NuzzGraph.Viewer.UserControls;

namespace NuzzGraph.Viewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Viewer : Elysium.Controls.Window
    {
        DataTemplate nodeHeaderTemplate;
       
        public Viewer()
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(Viewer_Loaded);
            zoom.MouseWheel += zoom_MouseWheel;
            //zoom.ZoomBox = Rect.Empty;
            //zoom.ZoomBoxBackground = Brushes.Red;
            //zoom.ModifierMode = WPFExtensions.Controls.ZoomViewModifierMode.None;
            
        }

        void Viewer_Loaded(object sender, RoutedEventArgs e)
        {
            host.Go();

            //Set node header template
            nodeHeaderTemplate = new DataTemplate();
            var label = new FrameworkElementFactory(typeof(TextBlock));
            label.SetBinding(TextBlock.TextProperty, new Binding());
            label.SetValue(TextBlock.BackgroundProperty, Brushes.Black);
            label.SetValue(TextBlock.ForegroundProperty, Brushes.White);
            label.SetValue(TextBlock.FontWeightProperty, FontWeights.Bold);
            nodeHeaderTemplate.VisualTree = label;

            LoadNodeTree();
            RefreshVisualGraph();
        }

        private void RefreshVisualGraph()
        {
            Random r = new Random();

            var brushes = typeof(Brushes).GetProperties(BindingFlags.Public | BindingFlags.Static)
                .Select(x => (Brush)x.GetValue(null, null))
                .ToList();

            Dictionary<INode, Thumb> NodeThumbs = new Dictionary<INode, Thumb>();

            var nodes = ContextFactory.New().Nodes.ToList();
            foreach (var node in nodes)
            {
                var interfaceName = "I" + node.TypeHandle.Label;
                var interfaceType = EntityUtility.AllCLRTypes.Where(x => x.Name == interfaceName).Single();
                int interfaceIndex = EntityUtility.AllCLRTypes.IndexOf(interfaceType);
                var brush = brushes[interfaceIndex];

                int rx = r.Next(1000);
                rx = rx % 2 == 0 ? rx : rx * -1;
                int ry = r.Next(1000);
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
                Width = 50,
                Height = 50
            };
            
            Canvas.SetLeft(thumb, x);
            Canvas.SetTop(thumb, y);
            canvas.Children.Add(thumb);
            thumb.DragStarted += new DragStartedEventHandler(thumb_DragStarted);
            thumb.DragCompleted += new DragCompletedEventHandler(thumb_DragCompleted);
            thumb.DragDelta += new DragDeltaEventHandler(thumb_DragDelta);
            //thumb.Style = Resources["GraphNode_Thumb"] as Style;
            var res = ResourceUtility.GetResourcesUnder("Themes");

            //Add text
            Dictionary<string, string> props = new Dictionary<string, string>();
            props["TestKey1"] = "TestValue1";
            props["TestKey2"] = "TestValue2";
            props["TestKey3"] = "TestValue3";
            props["TestKey4"] = "TestValue4";

            foreach (var pair in props)
            {
            }

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

        private void LoadNodeTree()
        {
            INodeType rootNodeType = ContextFactory.New().NodeTypes.Where(x => x.Label == "Node").Single();
            var root = LoadTreeviewItemForType(rootNodeType);
            treeView.Items.Add(root);
        }

        private TreeViewItem LoadTreeviewItemForType(INodeType type)
        {
            var typeItem = new TreeViewItem() { Header = type.Label };
            typeItem.HeaderTemplate = nodeHeaderTemplate;
            typeItem.IsExpanded = true;
            //Load subnodes for each:
            //Subnodes
            //Properties
            //Relationships
            //Functions

            var cat_subnode = new TreeViewItem() { Header = "Subtypes" };
            var cat_properties = new TreeViewItem() { Header = "Properties" };
            var cat_relationships = new TreeViewItem() { Header = "Relationships" };
            var cat_functions = new TreeViewItem() { Header = "Functions" };

            typeItem.Items.Add(cat_subnode);
            typeItem.Items.Add(cat_properties);
            typeItem.Items.Add(cat_relationships);
            typeItem.Items.Add(cat_functions);

            cat_subnode.IsExpanded = true;
            cat_properties.IsExpanded = true;
            cat_relationships.IsExpanded = true;
            cat_functions.IsExpanded = true;

            foreach (var subtype in type.SubTypes)
            {
                var subnode = LoadTreeviewItemForType(subtype);
                cat_subnode.Items.Add(subnode);
            }

            foreach (var prop in type.Properties)
            {
                var propItem = new TreeViewItem() { Header = prop.Label };
                cat_properties.Items.Add(propItem);
            }

            foreach (var rel in type.AllowedOutgoingRelationships)
            {
                var relItem = new TreeViewItem() { Header = rel.Label };
                cat_relationships.Items.Add(relItem);
            }

            foreach (var func in type.Functions)
            {
                var funcItem = new TreeViewItem() { Header = func.Label };
                cat_functions.Items.Add(funcItem);
            }

            return typeItem;
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void HelpClick(object sender, RoutedEventArgs e)
        {
            //Process.Start("http://www.nuzzgraph.com");
        }
    }
}
