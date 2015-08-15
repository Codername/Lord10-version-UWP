using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;

namespace Lord10
{
    class sysutils
    {


        // Carrega o Container de Dados do Aplicativo

        Windows.Storage.ApplicationDataContainer LocalSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
        Windows.Storage.StorageFolder localfolder = Windows.Storage.ApplicationData.Current.LocalFolder;




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
            Object retorno;
            string str;
            if (pos == 0)
            {
                if (parametro == "" || parametro == null)
                {
                    LocalSettings.Values["ip_lag"] = "192.168.100.100";
                }
                else
                {
                    LocalSettings.Values["ip_lag"] = parametro;
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




    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////    */



  public class Comn
    {
        //  HostName ip = new HostName("192.168.1.2");
        private string _ipdef;
        private bool _active;

        public void SetIp(string par)
        {
            _ipdef = par;
        }


        public string RetIp()
        {
            return _ipdef;
        }


        void connect() { }






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


        public RobotLag() : base()
        {
            Bibli = new sysutils();
            str = Bibli.Get_stored_IP(0);
            Sts = Bibli.Get_Robot_active(0);
            this.SetIp(str);
            this.Setstatus(Sts);
        }

        ~RobotLag()
        {
            Bibli.Store_IP(this.RetIp(), 0);
            Bibli.Store_Robot_active(this.Getstatus(), 0);
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
        }

        ~RobotFlag()
        {
            Bibli.Store_IP(this.RetIp(), 1);
            Bibli.Store_Robot_active(this.Getstatus(), 1);
        }
    }






}