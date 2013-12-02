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

        static Viewer()
        {
            
        }

        public Viewer()
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(Viewer_Loaded);
        }

        void Viewer_Loaded(object sender, RoutedEventArgs e)
        {
            //Set node header template
            nodeHeaderTemplate = new DataTemplate();
            var label = new FrameworkElementFactory(typeof(TextBlock));
            label.SetBinding(TextBlock.TextProperty, new Binding());
            label.SetValue(TextBlock.BackgroundProperty, Brushes.Black);
            label.SetValue(TextBlock.ForegroundProperty, Brushes.White);
            label.SetValue(TextBlock.FontWeightProperty, FontWeights.Bold);
            nodeHeaderTemplate.VisualTree = label;

            LoadNodeTree();
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void HelpClick(object sender, RoutedEventArgs e)
        {
            //Process.Start("http://www.nuzzgraph.com");
        }
    }
}
