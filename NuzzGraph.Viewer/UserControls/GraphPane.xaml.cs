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
    /// Interaction logic for GraphPane.xaml
    /// </summary>
    public partial class GraphPane : UserControl
    {
        public GraphPane()
        {
            InitializeComponent();
            this.Loaded += GraphPane_Loaded;
        }

        void GraphPane_Loaded(object sender, RoutedEventArgs e)
        {
            var f1 = ContextFactory.New().Functions.Where(x => x.Label == "Get").Single();
            nodehover.CurrentNode = f1;
        }
    }
}
