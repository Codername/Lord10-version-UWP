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
using Lord10;
using System.Diagnostics; // Uso do Writeline DEBUG


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Lord10.Forms
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public partial class ConfigForm : Page
    {
        private RobotLag _LAG;
        private RobotFlag _FLAG;

        public ConfigForm()
        {
            this.InitializeComponent();

            _LAG = ((App)Application.Current).LAG;

            _FLAG = ((App)Application.Current).FLAG;
        }

        private void Accept(object sender, RoutedEventArgs e)
        {
            bool sts;
            sts = cCheckIPLag.IsOn;
            _LAG.Setstatus(sts);
            _LAG.SetIp(cTextLag.Text);
            _LAG.SaveSettings();

            sts = cCheckIPFlag.IsOn;
            _FLAG.Setstatus( sts );
            _FLAG.SetIp(cTextFlag.Text);
            _FLAG.SaveSettings();


            this.Frame.Navigate(typeof(MainForm), e);
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainForm), e);
        }

        private void cCheckIPFlag_Toggled(object sender, RoutedEventArgs e)
        {
            
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
           if (_LAG != null || _FLAG != null)
            {
                cCheckIPLag.IsOn = _LAG.Getstatus();
                cTextLag.Text = _LAG.RetIp();
                cTextLag.IsEnabled = cCheckIPLag.IsOn;

                cCheckIPFlag.IsOn = _FLAG.Getstatus();
                cTextFlag.Text = _FLAG.RetIp();
                cTextFlag.IsEnabled = cCheckIPFlag.IsOn;

            }
        }

        private void cCheckIPLag_Toggled(object sender, RoutedEventArgs e)
        {
            cTextLag.IsEnabled = cCheckIPLag.IsOn;
        }

        private void cCheckIPFlag_Toggled_1(object sender, RoutedEventArgs e)
        {
            cTextFlag.IsEnabled = cCheckIPFlag.IsOn;
        }
    }
}
