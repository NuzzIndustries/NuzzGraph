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

namespace NuzzGraph.Viewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Viewer : Window
    {
        public Viewer()
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(Viewer_Loaded);
        }

        Ellipse s;
        Thumb thumb;

        void Viewer_Loaded(object sender, RoutedEventArgs e)
        {
            host.Go();

            s = new Ellipse()
            {
                Opacity = 1,
                Width = 100,
                Height = 100,
                Fill = Brushes.Red
            };

            s.MouseMove += new MouseEventHandler(s_MouseMove);
            s.MouseDown += new MouseButtonEventHandler(s_MouseDown);

            drawingArea.Children.Add(s);

            //Add thumb
            thumb = new Thumb()
            {
                Background = Brushes.Black,
                Width = 50,
                Height = 50
            };
            Canvas.SetLeft(thumb, 250);
            Canvas.SetTop(thumb, 400);
            drawingArea.Children.Add(thumb);
            thumb.DragStarted += new DragStartedEventHandler(thumb_DragStarted);
            thumb.DragCompleted += new DragCompletedEventHandler(thumb_DragCompleted);
            thumb.DragDelta += new DragDeltaEventHandler(thumb_DragDelta);
        }

        void thumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            e.Handled = true;
            Canvas.SetLeft(thumb, Math.Max(0, Canvas.GetLeft(thumb) + e.HorizontalChange));
            Canvas.SetTop(thumb, Math.Max(0, Canvas.GetTop(thumb) + e.VerticalChange));
            Canvas.SetRight(thumb, Math.Max(drawingArea.Width, Canvas.GetRight(thumb)));
            Canvas.SetBottom(thumb, Math.Max(drawingArea.Height, Canvas.GetBottom(thumb)));
            
        }

        void thumb_DragCompleted(object sender, DragCompletedEventArgs e)
        {
        }

        void thumb_DragStarted(object sender, DragStartedEventArgs e)
        {
        }

        private Point MouseDownLocation;

        void s_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                
                var pos = e.GetPosition(drawingArea);
                double left = pos.X + s.Margin.Left - MouseDownLocation.X;
                double top = pos.Y + s.Margin.Top - MouseDownLocation.Y;
                s.Width = left + 100;
                s.Height = top + 100;
                //s.Margin = new Thickness(left, top, s.Margin.Right, s.Margin.Bottom);
            }
            
        }

        void s_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed && e.ChangedButton == MouseButton.Left)
                MouseDownLocation = e.GetPosition(drawingArea);
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
            cx.DrawEllipse(Brushes.LightBlue, null,new System.Windows.Point(160, 100), 320, 80);

            cx.Close();

            return drawingVisual;
        }

        internal void Go()
        {
            //AddVisualChild(new TestUIElement().Path);

            _children.Add(GetDraggable());
            

            //_children.Add(CreateDrawingVisualRectangle());
        }
    }
}
