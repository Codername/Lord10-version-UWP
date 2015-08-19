using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Lord10.Forms
{
    class ViewControlMainPage : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged; // Evento de handle de Inotify - Main Form Chama este evento quando há algum evento selecionado nos HUBS

        public string _log;
        public string log
        {
            get { return this._log; }
            set
            {
                this._log = value;
                this.OnPropertyChanged("log");
            }
        }


        protected virtual void OnPropertyChanged(string propertyName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
