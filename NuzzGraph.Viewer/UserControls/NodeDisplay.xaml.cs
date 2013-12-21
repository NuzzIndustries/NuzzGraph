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
using NuzzGraph.Core;
using NuzzGraph.Core.Entities;
using NuzzGraph.Core.Utilities;

namespace NuzzGraph.Viewer.UserControls
{
    /// <summary>
    /// Interaction logic for NodeDisplay.xaml
    /// </summary>
    public partial class NodeDisplay : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public IEnumerable GridNodeData { get; private set; }

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

                if (this.grid.Columns.Count > 0)
                {
                    var style = Resources["nodePropertyNameColumn"] as Style;
                    this.grid.Columns[0].CellStyle = style;
                }
            }
        }

        private class NodeDisplayRow
        {
            public string Property { get; set; }
            public string Value { get; set; }
        }

        private IEnumerable BuildNodeData(INode @node)
        {
            //throw new NotImplementedException();
            var data = new List<NodeDisplayRow>();
            var nType = (NodeType)@node.TypeHandle;
            foreach (var prop in nType.AllProperties)
            {
                var d = new NodeDisplayRow();
                d.Property = prop.Label;
                var val = prop.GetValue(@node);
                d.Value = val == null ? "NULL" : val.ToString();
                data.Add(d);
            }

            return data;
        }


        public NodeDisplay()
        {
            InitializeComponent();
            grid.DataContext = this;

            using (var con = ContextFactory.New())
            {
                CurrentNode = (INode)con.NodeTypes.Where(x => x.Label == "Node").Single();
                var rel = ((Node)CurrentNode)._GetRelatedNodes();
            }
            //if (RuntimeUtility.RunningFromVisualStudioDesigner)
            //{
            //CurrentNode = ContextFactory.New().Types.Where(x => x.Label == "Node").Single();
            //}
        }
    }
}
