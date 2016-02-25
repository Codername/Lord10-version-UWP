using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
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
using Windows.UI.Xaml.Documents;

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
                log = "",
                Cor = new SolidColorBrush(Colors.White)
            };
            this.DataContext = view;
            ((App)Application.Current).LAG.event_log += displayLog;
            ((App)Application.Current).LAG.event_log_color += displaycolorLog;
      //      ((App)Application.Current).FLAG.event_log += displayLog;
        }


        async private void AppBarButton_Sync(object sender, RoutedEventArgs e)
        {

            

            spinButton.Begin();

            Button sb = (sysutils.FindVisualChild<Button>(MainHub, "cButtonLAG") as Button); // Referencia Button OK
            Display_Animation_Button(sb);

            // sb.Background =(Brush)Resources["BrushPadraoVerdeAtivo"]; // Carrega Resource definido em Styles não esta funbcionando Typecasting

            
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

        private void displayLog(Paragraph msg)
        {
            //           view.log += msg;
            RichTextBlock log = (sysutils.FindVisualChildlog<RichTextBlock>(MainHub, "cLogView") as RichTextBlock);
            log.Blocks.Add(msg);
        }

        private void displaycolorLog(Color cor)
        {
            view.Cor = new SolidColorBrush(cor);
        }

        private void Display_Animation_Button( Button rbutt )
        {
            /// Criação de Gradiente manual
            LinearGradientBrush BrushPadraoVerdeAtivo = new LinearGradientBrush();
            BrushPadraoVerdeAtivo.StartPoint = new Point(0.5, 0);
            BrushPadraoVerdeAtivo.EndPoint = new Point(0.5, 1);

            CompositeTransform trans = new CompositeTransform();
            trans.CenterY = 0.5;
            trans.CenterX = 0.5;
            trans.Rotation = 90;

            BrushPadraoVerdeAtivo.RelativeTransform = trans;
                    // Create and add Gradient stops
                       GradientStop stop1 = new GradientStop();
                       stop1.Color = Color.FromArgb(0xDD,0x00,0xF3,0x37);
                       stop1.Offset = 0.5;
                       BrushPadraoVerdeAtivo.GradientStops.Add(stop1);
                       GradientStop stop2 = new GradientStop();
                       stop2.Color = Color.FromArgb(0x52,0x00,0xFF,0xB9);
                       stop2.Offset = 1;
                       BrushPadraoVerdeAtivo.GradientStops.Add(stop2);
                       GradientStop stop3 = new GradientStop();
                       stop3.Color = Color.FromArgb(0x52,0x00,0xFF,0xB9);
                       BrushPadraoVerdeAtivo.GradientStops.Add(stop3);
                       GradientStop stop4 = new GradientStop();
                       stop4.Color = Color.FromArgb(0x78,0x00,0xF8,0x71);
                       stop4.Offset = 0.226;
                       BrushPadraoVerdeAtivo.GradientStops.Add(stop4);
                       GradientStop stop5 = new GradientStop();
                       stop5.Color = Color.FromArgb(0x7A,0x00,0xF8,0x6E);
                       stop5.Offset = 0.787;
                       BrushPadraoVerdeAtivo.GradientStops.Add(stop5);

            rbutt.Background = BrushPadraoVerdeAtivo;
             
                           
                                              /// Inicializa Storyboard

                                              Storyboard sbL = (sysutils.FindVisualChild<Button>(MainHub, "cButtonLAG") as Button).Resources["StoryboardOnLag"] as Storyboard;
            sbL.Begin();
            Storyboard sbF = (sysutils.FindVisualChild<Button>(MainHub, "cButtonFLAG") as Button).Resources["StoryboardOnFlag"] as Storyboard;
            sbF.Begin();
        }
    }
     
}
