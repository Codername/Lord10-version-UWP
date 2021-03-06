﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using System.Diagnostics;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Controls;
using System.Net.Sockets;
using System.Net.Http;
using Windows.Networking;
using Windows.Networking.Sockets;
using Windows.UI;
using Windows.UI.Xaml.Documents;
using System.IO;
using Lord10.DataTypes;
using Windows.Foundation;
using Windows.ApplicationModel.Background;

namespace Lord10
{


    public static class HubExtensions
    {
        public static async Task ScrollToSectionAnimated(this Hub hub, HubSection section)
        {
            // Find the internal scrollviewer and its current horizontal offset.

            // var viewer = hub.GetsDescendantOfType<ScrollViewer>();
           ScrollViewer viewer = FindChild<ScrollViewer>(hub, "ScrollViewer");

 
            var current = viewer.HorizontalOffset;

            // Find the distance to scroll.
            var visual = section.TransformToVisual(hub);
            var point = visual.TransformPoint(new Point(0, 0));
            var offset = point.X;

            // Scroll in a more or less animated way.
            var increment = offset / 24;
            for (int i = 1; i < 25; i++)
            {
                viewer.ChangeView((i * increment) + current, null, null, true);
                await Task.Delay(TimeSpan.FromMilliseconds(i * 5));
            }
        }


   
        /// <summary>
        ///  Pesquisa Visual Tree por Nome
        ///  Sample FindChild<TextBox>(Application.Current.MainWindow, "myTextBoxName");
        ///  
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parent"></param>
        /// <param name="childName"></param>
        /// <returns></returns>

        public static T FindChild<T>(DependencyObject parent, string childName)
   where T : DependencyObject
        {
            // Confirm parent and childName are valid. 
            if (parent == null) return null;

            T foundChild = null;

            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                // If the child is not of the request child type child
                T childType = child as T;
                if (childType == null)
                {
                    // recursively drill down the tree
                    foundChild = FindChild<T>(child, childName);

                    // If the child is found, break so we do not overwrite the found child. 
                    if (foundChild != null) break;
                }
                else if (!string.IsNullOrEmpty(childName))
                {
                    var frameworkElement = child as FrameworkElement;
                    // If the child's name is set for search
                    if (frameworkElement != null && frameworkElement.Name == childName)
                    {
                        // if the child's name is of the request name
                        foundChild = (T)child;
                        break;
                    }
                }
                else
                {
                    // child element found.
                    foundChild = (T)child;
                    break;
                }
            }

            return foundChild;
        }

    }

    class sysutils
    {


        // Carrega o Container de Dados do Aplicativo

        Windows.Storage.ApplicationDataContainer LocalSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
        Windows.Storage.StorageFolder localfolder = Windows.Storage.ApplicationData.Current.LocalFolder;


        static public Run printRunRTF(string str, Color Cor, int tipo)
        {
            // Paragraph Login = new Paragraph();
            Run run = new Run();
            run.Foreground = new SolidColorBrush(Cor);
            run.Text = str;
            run.FontSize = 13.333;
            if (tipo != 0 )
            {
                run.FontWeight = FontWeights.SemiBold;
            }
            return run;
        }

       // static public void message

        ///
        ///
        /// Metodo de localização de StoryBoard ON HUB
        /// ///
        /// ////
        /// ////
        /// //////

        static public childItem FindVisualChild<childItem>(DependencyObject obj, String name)
                      where childItem : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is childItem && ((Button)child).Name == name)
                    return (childItem)child;
                else
                {
                    childItem childOfChild = FindVisualChild<childItem>(child, name);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }


        /// <summary>
        ///  Override de Metodo para localizar Rich Text Control em tempo de execução
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        /// 

        static public childItem FindVisualChildlog<childItem>(DependencyObject obj, String name)
                 where childItem : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is childItem && ((RichTextBlock)child).Name == name)
                    return (childItem)child;
                else
                {
                    childItem childOfChild = FindVisualChildlog<childItem>(child, name);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }

        

        public string Get_stored_IP(int parametro)
        {
            Object retorno;
            string str="";
            if (parametro == 0)
            {
                // Lag
                retorno = LocalSettings.Values​​["ip_lag"];
                if (retorno != null)
                {
                    str = retorno.ToString();
                }
                if (retorno == null || str == "")
                {
                    return "192.168.100.100";
                }
                else
                {
                    return str;
                }

            }
            //Flag
            else
            {
                retorno = LocalSettings.Values​​["ip_flag"];
                if (retorno != null )
                {
                    str = retorno.ToString();
                }
                if (retorno == null || str == "")
                {
                    return "192.168.200.200";
                }
                else
                {
                    return str;
                }
            }
        }

        /// Salva Recursos; ("string IP", parametro)
        ///  (parametro 0) Lag
        ///  (parametro 1) Flag

        public void Store_IP(String parametro, int pos)
        {
           


            if (pos == 0)
            {
                if (parametro == "" || parametro == null)
                {
                    LocalSettings.Values["ip_lag"] = "192.168.100.100";
                }
                else
                {
                    LocalSettings.Values["ip_lag"] = parametro;
                    //Mensagem para Debuger
                }

            }
            //Flag
            else
            {
                if (parametro == "" || parametro == null)
                {
                    LocalSettings.Values["ip_flag"] = "192.168.100.200";
                }
                else
                {
                    LocalSettings.Values["ip_flag"] = parametro;
                }
            }
        }

        public Boolean Get_Robot_active(int par)
        {
            object ret;

            if (par == 0)
            {
                ret = LocalSettings.Values​​["active_lag"];
                if (ret == null || Convert.ToBoolean(ret))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                ret = LocalSettings.Values​​["active_flag"];
                if (ret == null || Convert.ToBoolean(ret))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////




        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public void Store_Robot_active(bool act, int par)
        {
            if (par == 0)
            {
                LocalSettings.Values​​["active_lag"] = act;
            }
            else
            {
                LocalSettings.Values​​["active_flag"] = act;
            }
        }



    }


    /*////////////////////////////////////////////////////////////////////////////////////////////////////////////////////    


        Classe Comn - Comunications Kernel

        // This example displays the following output to the console:
//       d: 6/15/2008
//       D: Sunday, June 15, 2008
//       f: Sunday, June 15, 2008 9:15 PM
//       F: Sunday, June 15, 2008 9:15:07 PM
//       g: 6/15/2008 9:15 PM
//       G: 6/15/2008 9:15:07 PM
//       m: June 15
//       o: 2008-06-15T21:15:07.0000000
//       R: Sun, 15 Jun 2008 21:15:07 GMT
//       s: 2008-06-15T21:15:07
//       t: 9:15 PM
//       T: 9:15:07 PM
//       u: 2008-06-15 21:15:07Z
//       U: Monday, June 16, 2008 4:15:07 AM
//       y: June, 2008
//       
//       'h:mm:ss.ff t': 9:15:07.00 P
//       'd MMM yyyy': 15 Jun 2008
//       'HH:mm:ss.f': 21:15:07.0
//       'dd MMM HH:mm:ss': 15 Jun 21:15:07
//       '\Mon\t\h\: M': Month: 6
//       'HH:mm:ss.ffffzzz': 21:15:07.0000-07:00



    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////    */



    public class Comn
    {


        //************************************************
        /// <summary>
        ///  Bloco de Definições Web Socket
        /// </summary>

        private const string PortComm = "1020";   // Porta para comunicação
        private const string _backgroundTaskName = "Socket Background Task"; // Nome do Socket Activity Triger

        private StreamSocket LocalSok; // Socket de Comunicação
        private HostName IpScocket; // Ip de comunicação
        private Stream streamOut;
        private Stream streamIn;

        //        private MessageWebSocket msgSock;


        ///////////////////////////////////////////////////////

        private string _ipdef;
        private bool _active;

        private generics.connect _isconected;

        public generics.connect IsConnected {
            get { return this._isconected; }
            
        }
        protected string textmsg;
        public delegate void logMsg(Paragraph msg);
        public event logMsg event_log;

        public delegate void Color_log(Color Cor);
        public event Color_log event_log_color;

        public Comn() {
            _isconected = generics.connect.idle;
        }


        public void SetIp(string par)
        {
            _ipdef = par;
        }

        public async Task<int> ConnectAsync()
        {
            if (event_log != null)
            {
                Paragraph Login = new Paragraph();
                Run run = new Run();
                run.Foreground = new SolidColorBrush(Windows.UI.Colors.Yellow);
                run.FontSize = 13.333;
                run.Text = textmsg;

                Run Complemento = new Run();
                DateTime Hoje = DateTime.Now;
                string strl = "_init():: Conect() dispatched as " + Hoje.ToString("T") + " " + Hoje.ToString("d") + "\n";
                Complemento.Text = strl;
                Complemento.FontSize = 13.333;
                Complemento.Foreground = new SolidColorBrush(Windows.UI.Colors.White);

                Login.Inlines.Add(run);
                Login.Inlines.Add(Complemento);

                this.event_log(Login);
            }


            ///
            /// Inicialização de Rede
            /// 
            IpScocket = new HostName(_ipdef); // Aponta Para endereço IP dos Mindstorms
            LocalSok = new StreamSocket();

            ///  Conexão
            ///  

            try
            {

                //                Debug.WriteLine("Tentativa de Conexão" + (DateTime.Now).ToString("T") );

                await LocalSok.ConnectAsync(IpScocket, PortComm);
                streamOut = LocalSok.OutputStream.AsStreamForWrite();
                StreamWriter writer = new StreamWriter(streamOut);
                streamIn = LocalSok.InputStream.AsStreamForRead();
                StreamReader reader = new StreamReader(streamIn);
                string outmsg = "handshake\n";
                await writer.WriteLineAsync(outmsg);
                await writer.FlushAsync();

                //Espera dados do mindstorms ...
              
                string response = await reader.ReadLineAsync();
                
                if (response == "MINDSTORMS")
                {
                    // Aponta o Delegate para listener(escuta) do socket
                    
                    _isconected = generics.connect.conected;

                    Paragraph Login = new Paragraph();
                    Run run = new Run();
                    run = sysutils.printRunRTF("Received " + textmsg, Colors.Lime, 1);
                    Run hand = new Run();
                    hand = sysutils.printRunRTF(" handshake", Colors.White, 1);
                    Run Complemento = new Run();
                    DateTime Hoje = DateTime.Now;
                    Complemento = sysutils.printRunRTF( " as " + Hoje.ToString("T")+"\n", Colors.White, 0);

                    Login.Inlines.Add(run);
                    Login.Inlines.Add(hand);
                    Login.Inlines.Add(Complemento);

                    this.event_log(Login);

                    /// Scket Activity Trigger
                    /// 

            /*        var socketTaskBuilder = new BackgroundTaskBuilder();
                    socketTaskBuilder.Name = _backgroundTaskName;
                    socketTaskBuilder.TaskEntryPoint = _backgroundTaskEntryPoint;
                    var trigger = new SocketActivityTrigger();
                    socketTaskBuilder.SetTrigger(trigger);
                    _task = socketTaskBuilder.Register(); */



                    //                    StreamSocketListener socketListener = new StreamSocketListener();
                    //                    socketListener.ConnectionReceived += SocketListener_ConnectionReceived;
                    //                    await socketListener.BindServiceNameAsync(PortComm);
                }

            }
            catch (Exception ex)
            {
                //Add code here to handle any exceptions
                if (event_log != null)
                {

                    Paragraph Login = new Paragraph();
                    Run run = new Run();
                    run = sysutils.printRunRTF(textmsg, Colors.Red, 0 );

                    Run Complemento = new Run();
                    Complemento = sysutils.printRunRTF("_init():: Conect() dispatched as", Colors.White, 0 );

                    Run Compl1 = new Run();
                    Compl1 = sysutils.printRunRTF(" FATAL ERROR CONECT ", Colors.Red, 1 );

                    Run Compl2 = new Run();
                    ///// Histórico de Evento  
                    DateTime Hoje = DateTime.Now;
                    Compl2 = sysutils.printRunRTF( ("as " + Hoje.ToString("T") + " " + Hoje.ToString("d") + "\n"), Colors.White, 1);

                    Login.Inlines.Add(run);
                    Login.Inlines.Add(Complemento);
                    Login.Inlines.Add(Compl1);
                    Login.Inlines.Add(Compl2);
                    this.event_log(Login);
                    _isconected = generics.connect.fail;
                }
            }
            return 0; 
        }
   
        public async Task<int> OnReceive()
        {
            StreamReader reader = new StreamReader(LocalSok.InputStream.AsStreamForRead());
            while (_isconected == generics.connect.conected )
            {

                string response = await reader.ReadLineAsync();
                if (response != null)
                {
                    Paragraph Login = new Paragraph();
                    Run run = new Run();
                    run = sysutils.printRunRTF("Received " + textmsg, Colors.Lime, 1);
                    Run Complemento = new Run();
                    DateTime Hoje = DateTime.Now;
                    Complemento = sysutils.printRunRTF((response + " as " + Hoje.ToString("T") + "\n"), Colors.White, 0);

                    Login.Inlines.Add(run);
                    Login.Inlines.Add(Complemento);

                    event_log(Login);
                }
            }
            return 0;
        }

        public void disConect()
        {
            if (_isconected == generics.connect.conected)
            {
                _send("CLOSE\n");
                LocalSok.Dispose();

                Run run = new Run();
                run = sysutils.printRunRTF(textmsg, Colors.Red, 1);

                Run Complemento = new Run();
                Complemento = sysutils.printRunRTF(" CLOSED ", Colors.White, 1);

                Run Compl2 = new Run();
                ///// Histórico de Evento  
                DateTime Hoje = DateTime.Now;
                Compl2 = sysutils.printRunRTF(("as " + Hoje.ToString("T") + " " + Hoje.ToString("d") + "\n"), Colors.White, 0);

                Paragraph Closemsg = new Paragraph();

                Closemsg.Inlines.Add(run);
                Closemsg.Inlines.Add(Complemento);
                Closemsg.Inlines.Add(Compl2);
                this.event_log(Closemsg);
                _isconected = generics.connect.idle;

            }
        }

        public void genericMove(generics.singleMovement mov )
        {
               switch (mov){

                case generics.singleMovement.stop:
                                                     _send("AB_STOP\n");
                                                     break;
                case generics.singleMovement.forward:
                                                     _send("AB_ROTATE\n");
                                                     break;
                case generics.singleMovement.rear:
                                                     _send("AB_INVERSE ROTATE\n");
                                                     break;
                case generics.singleMovement.left:
                                                     _send("B_ALTERNATE\n");
                                                     break;
                case generics.singleMovement.right:
                                                     _send("A_ALTERNATE\n");
                                                     break;
            }
        }

        private async void _send( string strOut)
        {
            StreamWriter writer = new StreamWriter(streamOut);
            await writer.WriteLineAsync(strOut);
            await writer.FlushAsync();

        }

        public string RetIp()
        {
            return _ipdef;
        }


     

        public bool Getstatus()
        {
            return _active;
        }

        public void Setstatus(bool act)
        {
            _active = act;
        }

        /// <summary>
        /// CallBack do Socket de Comunicação
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        private static void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.
                Socket client = (Socket)ar.AsyncState;

                // Complete the connection.
  //              client.EndConnect(ar);

                Debug.WriteLine("Socket connected to {0}",
                    client.RemoteEndPoint.ToString());

                // Signal that the connection has been made.
 //               connectDone.Set();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
        }

        /// <summary>
        ///  Send message on IP net
        /// </summary>
        /// <param name="str" String Message></param>
        /// 
        async public void SendCommand(string str)
        {

            Stream outStream = LocalSok.OutputStream.AsStreamForWrite();
            StreamWriter writer = new StreamWriter(outStream);
            await writer.WriteLineAsync(str);
            await writer.FlushAsync();
        }

        /// <summary>
        ///  "Listener" Escuta a porta do Socket 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>

        private async void SocketListener_ConnectionReceived(StreamSocketListener sender,
            StreamSocketListenerConnectionReceivedEventArgs args)
        {
            //Lê uma linha do cliente remoto
            try
            {
                Stream inStream = args.Socket.InputStream.AsStreamForRead();
                StreamReader reader = new StreamReader(inStream);
                string request = await reader.ReadLineAsync();

                Paragraph Login = new Paragraph();
                Run run = new Run();
                run = sysutils.printRunRTF("Received " + textmsg, Colors.LightGreen, 1);

                Run Complemento = new Run();
                DateTime Hoje = DateTime.Now;
                Complemento = sysutils.printRunRTF( ( request+"as " + Hoje.ToString("T") ), Colors.White, 0);
                
                Login.Inlines.Add(run);
                Login.Inlines.Add(Complemento);
                
                this.event_log(Login);
            }
            catch
            {
               
            }
        }

         public bool isEventAtrib()
        {
            if (this.event_log == null)
            { 
                return false;
            }
            else
            {
                return true;
            }

        }

        
        

    

}

    


    /*////////////////////////////////////////////////////////////////////////////////////////////////////////////////////    


        Clase Lag




    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////    */


    public  class RobotLag : Comn
    {
        sysutils Bibli;
        string str;
        private bool Sts;
        event logMsg log;


        public RobotLag() : base()
        {
            Bibli = new sysutils();
            str = Bibli.Get_stored_IP(0);
            Sts = Bibli.Get_Robot_active(0);
            this.SetIp(str);
            this.Setstatus(Sts);
            this.textmsg = "LAG: ";

        }
        
        ~RobotLag()
        {
           
            Debug.WriteLine("Metodo Destrutor Chamado");
        }

 /*       override public async void Connect()
        {
            base.Connect();
        } */


        public void SaveSettings()
        {
            Bibli.Store_IP(this.RetIp(), 0);
            Bibli.Store_Robot_active(this.Getstatus(), 0);
            //Mensagem para Debuger

        }


        }


        /*////////////////////////////////////////////////////////////////////////////////////////////////////////////////////    


            Classe Flag




        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////    */



        public class RobotFlag : Comn
    {
        sysutils Bibli;
        string str;
        private bool Sts;


    public RobotFlag() : base()
    {
            Bibli = new sysutils();
            str = Bibli.Get_stored_IP(1);
            Sts = Bibli.Get_Robot_active(1);
            this.SetIp(str);
            this.Setstatus(Sts);
            this.textmsg = "FLAG: ";
        }
        public void SaveSettings()
        {
            Bibli.Store_IP(this.RetIp(), 1);
            Bibli.Store_Robot_active(this.Getstatus(), 1);
        }

    }

 }  
