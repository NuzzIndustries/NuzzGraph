using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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
using NuzzGraph.Entities;

namespace NuzzGraph.Viewer.UserControls
{
    /// <summary>
    /// Interaction logic for NodeDisplay.xaml
    /// </summary>
    public partial class NodeDisplay : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public IEnumerable GridNodeData  { get; private set; }
        
        private INode _CurrentNode;
        public INode CurrentNode 
        { 
            get { return _CurrentNode; }
            set 
            { 
                _CurrentNode = value;
                GridNodeData = BuildNodeData(value);
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("GridNodeData")); 
            } 
        }

        private IEnumerable BuildNodeData(INode node)
        {
            foreach (var prop in node.TypeHandle.Properties)
            {
                var val = prop.GetValue(node);

            }
            return new List<INode>() { node };
        }


        public NodeDisplay()
        {
            InitializeComponent();
            grid.DataContext = this;
        }

        
    }
}
