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
using Lord10.DataTypes;
using System.Diagnostics;

// The Content Dialog item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Lord10.Forms
{
    public sealed partial class Conect_Diag : ContentDialog
    {
        

        private RobotLag _LAG;
        private RobotFlag _FLAG;

          public Conect_Diag()
        {
            this.InitializeComponent();
            this.IsPrimaryButtonEnabled = false;
            _LAG  = ((App)Application.Current).LAG;
            _FLAG = ((App)Application.Current).FLAG;
            if (!(_LAG.IsConnected == generics.connect.conected)   && !(_FLAG.IsConnected == generics.connect.conected) ) // NOT Robos conectados ???
            {
                if (!_LAG.Getstatus() && !_FLAG.Getstatus()) // NOT Selecionados ????
                {
                    // Mensagem solicitando conectar pelo menos um Mindstorms

                    ProgreesTemp.Visibility = Visibility.Collapsed;
                    MesgGlob.Text = "Você precisa habilitar pelo menos um controlador Lego Mindstorms em configurações.";
                    MesgGlob.FontSize = 13;
                    MesgGlob.Visibility = Visibility.Visible;
                }
                else
                {
                    this.Label_Status.Text = "Conectando Lag... Isso pode demorar...";
                    _process();
                                       
                }
            }
            else
            {
                //  TODO : FLAG ou LAG ja conectado
                
            }
        }

        private async void _process()
        {
            await _LAG.ConnectAsync();
            if (_LAG.IsConnected == generics.connect.fail || _LAG.IsConnected == generics.connect.idle)
            {
                ProgreesTemp.Visibility = Visibility.Collapsed;
                this.Label_Status.Text = "";
                MesgGlob.Text = " Não Houve Resposta de Lag... ";
                MesgGlob.FontSize = 14;
                MesgGlob.Height = 40;
                MesgGlob2.Text = "Deseja tentar uma nova conexão ?";

                MesgGlob.Visibility = Visibility.Visible;
                MesgGlob2.Visibility = Visibility.Visible;

                PrimaryButtonText = "Sim";
                SecondaryButtonText = "Não";
                IsPrimaryButtonEnabled = true;
            }
        }


        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            if(_LAG.IsConnected == generics.connect.fail || _LAG.IsConnected == generics.connect.idle)
            {
                IsPrimaryButtonEnabled = false;
                MesgGlob.Visibility = Visibility.Collapsed;
                MesgGlob2.Visibility = Visibility.Collapsed;
                this.Label_Status.Text = "Conectando Lag... Isso pode demorar...";
                ProgreesTemp.Visibility = Visibility.Visible;
                _process();

                args.Cancel = true; // Evita fechamento do Dialogo
            }
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            
        }
    }
}
