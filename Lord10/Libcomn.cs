using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Controls;
using System.Net.Sockets;
using System.Net.Http;
using Windows.Networking.Sockets;
using Windows.UI;
using Windows.UI.Xaml.Documents;

namespace Lord10
{
    class sysutils
    {


        // Carrega o Container de Dados do Aplicativo

        Windows.Storage.ApplicationDataContainer LocalSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
        Windows.Storage.StorageFolder localfolder = Windows.Storage.ApplicationData.Current.LocalFolder;

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
        ///  Override de Metodo para localizar Rich Text
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
            object ret;

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
        //  HostName ip = new HostName("192.168.1.2");
        private string _ipdef;
        private bool _active;

        private Socket webSock;          // <summary>
        private MessageWebSocket msgSock;

        private HttpClient client;


        
        public bool IsConnected{
                                     get;
                               }
        protected string textmsg;
        public delegate void logMsg(Paragraph msg);
        public event logMsg event_log;

        public delegate void Color_log(Color Cor);
        public event Color_log event_log_color;

        public Comn() {
                        IsConnected = false;
                        }
        

        public void SetIp(string par)
        {
            _ipdef = par;
        }


        public virtual void Connect()
        {
            try
            {
                if (event_log != null)
                {
                    this.event_log_color(Colors.DarkGreen);

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

                    ///// Histórico de Evento  
//                    DateTime Hoje = DateTime.Now;
//                    string strl = "_init():: Conect() dispatched as " + Hoje.ToString("T") + " " + Hoje.ToString("d") + "\n";

//                    this.event_log_color(Colors.White);
//                    this.event_log(strl);

                }
                webSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                //                sck.AcceptAsync()

                //                sck.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

           
            }
            finally {
                if (event_log != null)
                {

                    Paragraph Login = new Paragraph();
                    Run run = new Run();
                    run.Foreground = new SolidColorBrush(Windows.UI.Colors.Red);
                    run.FontSize = 13.333;
                    run.Text = textmsg;

                    Run Complemento = new Run();
                    Complemento.Text = "_init():: Conect()";
                    Complemento.FontSize = 13.333;
                    Complemento.Foreground = new SolidColorBrush(Windows.UI.Colors.White);

                    Run Compl1 = new Run();
                    Compl1.Foreground = new SolidColorBrush(Windows.UI.Colors.Red);
                    Compl1.FontSize = 13.333;
                    Compl1.Text = " FATAL ERROR CONECT ";

                    Run Compl2 = new Run();
                    DateTime Hoje = DateTime.Now;
                    Compl2.Foreground = new SolidColorBrush(Windows.UI.Colors.White);
                    Compl2.FontSize = 13.333;
                    Compl2.Text = " as " + Hoje.ToString("T") + " " + Hoje.ToString("d") + "\n";

                    Login.Inlines.Add(run);
                    Login.Inlines.Add(Complemento);
                    Login.Inlines.Add(Compl1);
                    Login.Inlines.Add(Compl2);

                    this.event_log(Login);
                    ///// Histórico de Evento  
                    /*                   DateTime Hoje = DateTime.Now;

                                       string strl = textmsg + "_init():: Conect() FATAL ERROR CONNECT as " + Hoje.ToString("T") + " " + Hoje.ToString("d") + "\n";
                                       this.event_log_color(Colors.Red);
                                       this.event_log(strl);   */

                }
            };
            
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

    }


    /*////////////////////////////////////////////////////////////////////////////////////////////////////////////////////    


        Clase Lag




    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////    */


    public class RobotLag : Comn
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

        override public void Connect()
        {
            base.Connect();
        }


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