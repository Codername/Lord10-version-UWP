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
using Windows.Storage.Pickers;
using Windows.Storage;
using Windows.Storage.Provider;
using Lord10.DataTypes;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Lord10.Forms
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainForm : Page
    {
        private ViewControlMainPage view;
        private RobotLag _LAG;
        public MainForm()
        {
            
            this.InitializeComponent();


            // Inicialização do mode View
            view = new ViewControlMainPage
            {
                log = "",
                Cor = new SolidColorBrush(Colors.White)
            };
            //// Set de variáveis globais e privates

            this.DataContext = view;
            ((App)Application.Current).LAG.event_log += displayLog;
            ((App)Application.Current).LAG.event_log_color += displaycolorLog;
            _LAG = ((App)Application.Current).LAG;

        //// *********************************************************************************************************************
      //      ((App)Application.Current).FLAG.event_log += displayLog;
        }


        async private void AppBarButton_Sync(object sender, RoutedEventArgs e)
        {

            Conect_Diag conf = new Conect_Diag();
            await conf.ShowAsync();
            spinButton.Begin();

            Button sb = (sysutils.FindVisualChild<Button>(MainHub, "cButtonLAG") as Button); // Referencia Button OK
            Display_Animation_Button(); // Chamar Rotina para atualizar fundo de Botoes
            AppBarDisconect.Visibility = Visibility.Visible;




                      
            
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
            //
            RichTextBlock log = (sysutils.FindVisualChildlog<RichTextBlock>(MainHub, "cLogView") as RichTextBlock);
            log.Blocks.Clear();

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

        private void Display_Animation_Button()
        {
            /// Criação de Gradiente manual
            /// Bloco de HistoryBoard Desabilitado 
            ///

            /*  LinearGradientBrush BrushPadraoVerdeAtivo = new LinearGradientBrush();
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
                sbL.Begin(); */

            // Button sbF = (sysutils.FindVisualChild<Button>(MainHub, "cButtonLAG") as Button);
            //  sbF.Background = new SolidColorBrush(Color.FromArgb(0xBC,0x16,0x9E,0x41));
            Storyboard sbL = (sysutils.FindVisualChild<Button>(MainHub, "cButtonLAG") as Button).Resources["StoryboardOnLag"] as Storyboard;
            sbL.Begin();
        }

        private void AppBarDisconect_Click(object sender, RoutedEventArgs e)
        {
            Button sbF = (sysutils.FindVisualChild<Button>(MainHub, "cButtonLAG") as Button);
            sbF.Background = new SolidColorBrush(Color.FromArgb(0x33,0x06,0x06,0x06));
            spinButton.Stop();
            AppBarDisconect.Visibility = Visibility.Collapsed;
            Storyboard sbL = (sysutils.FindVisualChild<Button>(MainHub, "cButtonLAG") as Button).Resources["StoryboardOnLag"] as Storyboard;
            sbL.Stop();
        }

        private async void Salvar_Click(object sender, RoutedEventArgs e)
        {
            RichTextBlock _log = (sysutils.FindVisualChildlog<RichTextBlock>(MainHub, "cLogView") as RichTextBlock);
            _log.SelectAll();
            string TXT = _log.SelectedText;
            if (TXT != "")
            {
                // Clear previous returned file name, if it exists, between iterations of this scenario

                FileSavePicker savePicker = new FileSavePicker();
                savePicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
                // Dropdown of file types the user can save the file as
                savePicker.FileTypeChoices.Add("rtf (Rich Text Format)", new List<string>() { ".rtf" });
                // Default file name if the user does not type one in or select a file to replace
                DateTime Hoje = DateTime.Now;
                string str = "Log " + Hoje.ToString("T") + " " + Hoje.ToString("d");


                /// Bloco de criação de Content Diag  Hard Coded
                // *****************************************************************************


                var dialog = new ContentDialog()
                {
                    Title = "",
                    RequestedTheme = ElementTheme.Dark,
                    //FullSizeDesired = true,
                    MaxWidth = this.ActualWidth // Required for Mobile!
                };

                // Setup Content
                var panel = new StackPanel();

                dialog.Content = panel;
                // Add Buttons

                dialog.PrimaryButtonText = "OK";
                dialog.IsPrimaryButtonEnabled = true;

                //************************************************************************************

                savePicker.SuggestedFileName = str;
                StorageFile file = await savePicker.PickSaveFileAsync();

                if (file != null)
                {
                    // Prevent updates to the remote version of the file until we 
                    // finish making changes and call CompleteUpdatesAsync.
                    // write to file

                    Windows.Storage.CachedFileManager.DeferUpdates(file);

                    // Let Windows know that we're finished changing the file so the 
                    // other app can update the remote version of the file.


                    await FileIO.WriteTextAsync(file, TXT);

                    /// 
                    // Let Windows know that we're finished changing the file so the other app can update the remote version of the file.
                    // Completing updates may require Windows to ask for user input.


                    Windows.Storage.Provider.FileUpdateStatus status = await Windows.Storage.CachedFileManager.CompleteUpdatesAsync(file);


                    if (status == FileUpdateStatus.Complete)
                    {
                        panel.Children.Add(new TextBlock
                        {
                            Text = "Arquivo: " + file.Name + " foi salvo com sucesso.",
                            TextWrapping = TextWrapping.Wrap,
                        });
                        // Show Dialog

                        var result = await dialog.ShowAsync();
                    }
                    else
                    {
                        panel.Children.Add(new TextBlock
                        {
                            Text = "Arquivo :" + file.Name + " não pode ser salvo.",
                            TextWrapping = TextWrapping.Wrap,
                        });
                        // Show Dialog

                        var result = await dialog.ShowAsync();
                    }
                }
                else
                {
                    /*       panel.Children.Add(new TextBlock
                           {
                               Text = ("Operação Cancelada."),
                               TextWrapping = TextWrapping.Wrap
                           });
                          // Show Dialog
                           var result = await dialog.ShowAsync();  */
                }
            }
        }

        private async void moveRobot(generics.singleMovement mov, string str )
        {
            Paragraph msg = new Paragraph();

            Run run = new Run();

            run = sysutils.printRunRTF("Send LAG :", Colors.Yellow, 1);
            Run Compl2 = new Run();
            DateTime Hoje = DateTime.Now;
            Compl2 = sysutils.printRunRTF((str+" as " + Hoje.ToString("T") + "\n"), Colors.White, 0);

            msg.Inlines.Add(run);
            msg.Inlines.Add(Compl2);

            displayLog(msg);

            ///// Comando de Movimentação
            _LAG.genericMove(mov);

        }

        private void ButtonStop_Click(object sender, RoutedEventArgs e)
        {
            if(_LAG.IsConnected == generics.connect.conected)
            {
                moveRobot(generics.singleMovement.stop, "Stop A & B Motors");
            }
        }


    }
   }
     

