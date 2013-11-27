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
using FirstFloor.ModernUI.Windows.Controls;

namespace NuzzGraph.Viewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Viewer : ModernWindow
    {
       
        public Viewer()
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(Viewer_Loaded);
        }

        

        void Viewer_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        
    }

   
}
