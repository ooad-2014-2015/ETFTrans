using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace ETFTrans
{
    public class testViewModel : INotifyPropertyChanged
    {
        private string polaziste = "Polaziste";
        private string odrediste = "Odrediste";

        /*public testViewModel()
        {
            polaziste = "Polaziste";
            odrediste = "Odrediste";
        }*/

        public string Polaziste
        {
            get { return polaziste; }
            set { 
                polaziste = value;
                NotifyPropertyChanged("Polaziste");
            
            }
        }

        public string Odrediste
        {
            get { return odrediste; }
            set { 
                odrediste = value;
                NotifyPropertyChanged("Odrediste");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
