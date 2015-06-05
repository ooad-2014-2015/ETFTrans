using ETFTrans.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ETFTrans.ViewModel
{
    public class StanicaViewModel : BaseViewModel
    {
       
        internal Stanica novaStanica;
        private string _nazivStanice;
        private string _cijena;
        private ICommand _btnDodajNovuStanicu;
        Window wind;
       

        public string NazivStanice
        {
            set
            {
                if(_nazivStanice != value)
                {
                    _nazivStanice = value;
                    OnPropertyChanged("NazivStanice");
                }
               
            }
            get { return _nazivStanice; }
        }
        public string Cijena
        {
            set
            {
                if(_cijena != value)
                {
                    _cijena = value;
                    OnPropertyChanged("Cijena");
                }
            }
            get { return _cijena; }
        }
        public ICommand BtnDodajNovuStanicu
        {
            set
            {
                if (_btnDodajNovuStanicu != value)
                    _btnDodajNovuStanicu = value;
            }
            get { return _btnDodajNovuStanicu; }
        }
        public StanicaViewModel(Window x)
        {
            BtnDodajNovuStanicu = new RelayCommand(dodajNovuStanicu);
            wind = x;
        }

        private void dodajNovuStanicu()
        {
            if(validacijaCijene(Cijena))
            {
                novaStanica = new Stanica()
                {
                    nazivGrada = NazivStanice,
                    cijenaVoznje = decimal.Parse(Cijena)
                };
                
                wind.Close();
                
                
            }
        }
        private bool validacijaCijene(string cijena)
        {
            try
            {
                decimal cij = Decimal.Parse(cijena);
                return true;
            }
            catch(Exception e)
            {
                MessageBox.Show("Cijena mora biti broj!!");
                return false;
            }
            
        }

    }
}
