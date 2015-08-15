using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Lord10.Forms
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ConfigForm : Page
    {
        public ConfigForm()
        {
            this.InitializeComponent();
        }

        private void Accept(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainForm), e);
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainForm), e);
        }

        private void cCheckIPFlag_Toggled(object sender, RoutedEventArgs e)
        {

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
