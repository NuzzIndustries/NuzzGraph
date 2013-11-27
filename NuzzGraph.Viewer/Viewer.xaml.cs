﻿using System;
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

namespace NuzzGraph.Viewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Viewer : Elysium.Controls.Window
    {
       
        public Viewer()
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(Viewer_Loaded);
        }

        

        void Viewer_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private static readonly string Windows = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
        private static readonly string SegoeUI = Windows + @"\Fonts\SegoeUI.ttf";
        private static readonly string Verdana = Windows + @"\Fonts\Verdana.ttf";

        private void ThemeGlyphInitialized(object sender, EventArgs e)
        {
            ThemeGlyph.FontUri = new Uri(File.Exists(SegoeUI) ? SegoeUI : Verdana);
        }

        private void AccentGlyphInitialized(object sender, EventArgs e)
        {
            AccentGlyph.FontUri = new Uri(File.Exists(SegoeUI) ? SegoeUI : Verdana);
        }

        private void ContrastGlyphInitialized(object sender, EventArgs e)
        {
            ContrastGlyph.FontUri = new Uri(File.Exists(SegoeUI) ? SegoeUI : Verdana);
        }

        private void LightClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Apply(Theme.Light);
        }

        private void DarkClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Apply(Theme.Dark);
        }

        private void AccentClick(object sender, RoutedEventArgs e)
        {
            var item = e.Source as MenuItem;
            if (item != null)
            {
                var accentBrush = (SolidColorBrush)((Rectangle)item.Icon).Fill;
                Application.Current.Apply(accentBrush, null);
            }
        }

        private void WhiteClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Apply(null, Brushes.White);
        }

        private void BlackClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Apply(null, Brushes.Black);
        }

        private void NotificationClick(object sender, RoutedEventArgs e)
        {
            NotificationManager.BeginTryPush("Message", "The quick brown fox jumps over the lazy dog");
        }

        private void DonateClick(object sender, RoutedEventArgs e)
        {
            Process.Start("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=KNYYZ7RM6LBCG");
        }

        private void LicenseClick(object sender, RoutedEventArgs e)
        {
            Process.Start("http://elysium.asvishnyakov.com/License.cshtml");
        }

        private void AuthorsClick(object sender, RoutedEventArgs e)
        {
            Process.Start("http://elysium.codeplex.com/team/view");
        }

        private void HelpClick(object sender, RoutedEventArgs e)
        {
            Process.Start("http://elysium.asvishnyakov.com/Documentation.cshtml");
        }
        
    }

   
}
