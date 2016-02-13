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
using Lord10.Controls;
using System.Diagnostics; // Uso do Writeline DEBUG
using Windows.UI.Xaml.Media.Animation;
using Lord10;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Lord10.Forms
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainForm : Page
    {
        private ViewControlMainPage view;
        public MainForm()
        {
            
            this.InitializeComponent();


            // Inicialização do mode View
            view = new ViewControlMainPage
            {
                log = ""
            };
            this.DataContext = view;
            ((App)Application.Current).LAG.event_log += displayLog;
            ((App)Application.Current).FLAG.event_log += displayLog;
        }


        async private void AppBarButton_Sync(object sender, RoutedEventArgs e)
        {

            

            spinButton.Begin();

            // Chame StoryButton FLAG
            Storyboard sb = ( sysutils.FindVisualChild<Button>(MainHub, "cButtonLAG") as Button).Resources["StoryboardOnFlag"] as Storyboard;
            // sb.Begin();


            // Chama Diag

            Conect_Diag conf = new Conect_Diag();
            await conf.ShowAsync();

            /* if (conf.Result == conf.SignInOK)
            {
                // Sign in was successful.
            }
            else if (signInDialog.Result == SignInResult.SignInFail)
            {
                // Sign in failed.
            }
            else if (signInDialog.Result == SignInResult.SignInCancel)
            {
                // Sign in was cancelled by the user.
            } */



        }

        private void cLimpBt_Click(object sender, RoutedEventArgs e)
        {
            // Zera HubLog.
            //
            view.log = "";
            
        }

        private void displayLog(string msg)
        {
            view.log += msg;
        }
    }
  
}
