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
using NuzzGraph.Core.Entities;
using NuzzGraph.Viewer.Utilities;

namespace NuzzGraph.Viewer.UserControls
{
    /// <summary>
    /// Interaction logic for GraphPane.xaml
    /// </summary>
    public partial class GraphPane : UserControl
    {
        public INode CurrentMouseoverNode 
        {
            get { return nodehover.CurrentNode; }
            set { nodehover.CurrentNode = value; }
        }


        public GraphPane()
        {
            InitializeComponent();
            this.Loaded += GraphPane_Loaded;
        }

        void GraphPane_Loaded(object sender, RoutedEventArgs e)
        {
            //var f1 = ContextFactory.New().Functions.Where(x => x.Label == "Get").Single();
            //nodehover.CurrentNode = f1;
        }

        private void GraphPane_Initialized(object sender, EventArgs e)
        {
            viewport.GraphPane = this;
        }

        private void viewport_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
