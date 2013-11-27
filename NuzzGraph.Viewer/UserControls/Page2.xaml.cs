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
using NuzzGraph.Core;
using NuzzGraph.Entities;
using NuzzGraph.Viewer.UIElements;

namespace NuzzGraph.Viewer.UserControls
{
    /// <summary>
    /// Interaction logic for Page2.xaml
    /// </summary>
    public partial class Page2 : UserControl
    {
        DataTemplate nodeHeaderTemplate;
        Thumb thumb;

        public Page2()
        {
            InitializeComponent();

            this.Loaded += Page2_Loaded;
        }

        private void LoadNodeTree()
        {
            INodeType rootNodeType = ContextFactory.New().NodeTypes.Where(x => x.Label == "Node").Single();
            var root = LoadTreeviewItemForType(rootNodeType);
            treeView.Items.Add(root);
        }

        void Page2_Loaded(object sender, RoutedEventArgs e)
        {
            host.Go();

            //Set node header templar
            nodeHeaderTemplate = new DataTemplate();
            var label = new FrameworkElementFactory(typeof(TextBlock));
            label.SetBinding(TextBlock.TextProperty, new Binding());
            label.SetValue(TextBlock.BackgroundProperty, Brushes.Black);
            label.SetValue(TextBlock.ForegroundProperty, Brushes.White);
            label.SetValue(TextBlock.FontWeightProperty, FontWeights.Bold);
            nodeHeaderTemplate.VisualTree = label;

            //Add thumb
            thumb = new Thumb()
            {
                Background = Brushes.Black,
                Width = 50,
                Height = 50
            };
            Canvas.SetLeft(thumb, 150);
            Canvas.SetTop(thumb, 150);
            drawingArea.Children.Add(thumb);
            thumb.DragStarted += new DragStartedEventHandler(thumb_DragStarted);
            thumb.DragCompleted += new DragCompletedEventHandler(thumb_DragCompleted);
            thumb.DragDelta += new DragDeltaEventHandler(thumb_DragDelta);

            LoadNodeTree();
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
            Canvas.SetLeft(thumb, Math.Max(0, Canvas.GetLeft(thumb) + e.HorizontalChange));
            Canvas.SetTop(thumb, Math.Max(0, Canvas.GetTop(thumb) + e.VerticalChange));
            Canvas.SetLeft(thumb, Math.Min(drawingArea.ActualWidth - thumb.Width, Canvas.GetLeft(thumb)));
            Canvas.SetTop(thumb, Math.Min(drawingArea.ActualHeight - thumb.Width, Canvas.GetTop(thumb)));
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
    }

    //For DrawingVisual objects *only*
    public class MyVisualHost : FrameworkElement
    {
        // Create a collection of child visual objects.
        private VisualCollection _children;

        public MyVisualHost()
        {
            _children = new VisualCollection(this);

            // Add the event handler for MouseLeftButtonUp.
            this.MouseLeftButtonUp += new MouseButtonEventHandler(MyVisualHost_MouseLeftButtonUp);
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

        internal void Go()
        {
            //AddVisualChild(new TestUIElement().Path);

            //_children.Add(GetDraggable());


            //_children.Add(CreateDrawingVisualRectangle());
        }    
    }
}
