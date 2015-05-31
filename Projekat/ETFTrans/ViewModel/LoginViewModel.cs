using ETFTrans.DataAcces;
using ETFTrans.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ETFTrans;
using ETFTrans.Model;

namespace ETFTrans.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        private string _txtUserName;
        private string _txtPassword;
        private List<Uposlenik> tipoviZaposlenika = new List<Uposlenik>() { new ClanUprave() { ime = "Clan uprave" }, new Direktor() { ime = "Direktor" }, new Otpremnik() { ime = "Otpremnik" }, new RadnikNaSalteruProdaja() { ime = "Radnik u prodaji" }, new RadnikNaSalteruPretraga() { ime = "Radnik na pretrazi" } };
        private int selectedIndexTipZaposlenika;
        private ICommand klikNa_btnLogIn;
        private static Window mainWindow;
        public int SelectedIndexTipZaposlenika
        {
            get { return selectedIndexTipZaposlenika; }
            set
            {
                selectedIndexTipZaposlenika = value;
                OnPropertyChanged("SelectedIndexTipZaposlenika");
            }
        }

        public List<Uposlenik> TipoviZaposlenika
        {
            get { return tipoviZaposlenika; }
            set
            {
                if (tipoviZaposlenika != value)
                    tipoviZaposlenika = value;
            }
        }

        public ICommand KlikNa_LogIn
        {
            get { return klikNa_btnLogIn; }
            set
            {
                if (klikNa_btnLogIn != value)
                    klikNa_btnLogIn = value;
            }
        }
        
        public LoginViewModel()
        {
            SelectedIndexTipZaposlenika = 0;
            klikNa_btnLogIn = new RelayCommand(new Action(ValidirajLogInZaUposlenika));
      
            mainWindow = Application.Current.MainWindow;
        }

        private void ValidirajLogInZaUposlenika()
        {
            Uposlenik u = BazaFunkcije.dajUposlenika(UserName, Password);

            if(u == null)
            {
                MessageBox.Show("Korisnik ne postoji!!", "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);
                UserName = "";
                Password = "";
                return;
            }

            if (u is ClanUprave)
                SelectedIndexTipZaposlenika = 0;
            if (u is Direktor)
                SelectedIndexTipZaposlenika = 1;
            if (u is Otpremnik)
                SelectedIndexTipZaposlenika = 2;
            if (u is RadnikNaSalteruProdaja)
                SelectedIndexTipZaposlenika = 3;
            if (u is RadnikNaSalteruPretraga)
                SelectedIndexTipZaposlenika = 4;
            BazaFunkcije.registrujLogInUposlenika(UserName);
            otvoriOdgovarajuciView(tipoviZaposlenika[SelectedIndexTipZaposlenika].ime, UserName);
               
            
        }
        private void otvoriOdgovarajuciView(string tipZaposlenika, string userName)
        {
            if(tipZaposlenika == "Clan uprave")
            {
                ClanUpraveView view = new ClanUpraveView();
                ClanUpraveViewModel dijete = new ClanUpraveViewModel(UserName, view);
                view.DataContext = dijete;
                mainWindow.Close();
                view.Show();
            }
            else if(tipZaposlenika == "Direktor")
            {
                
            }
            else if (tipZaposlenika == "Radnik u prodaji")
            {
                ProdajaKarataView view = new ProdajaKarataView();
                ProdajaKarataViewModel dijete = new ProdajaKarataViewModel(UserName, view);
                view.DataContext = dijete;
                mainWindow.Close();
                view.Show();

            }
            else if(tipZaposlenika == "Otpremnik")
            {
                OtpremnikView view = new OtpremnikView();
                OtpremikViewModel dijete = new OtpremikViewModel(UserName, view);
                view.DataContext = dijete;
                mainWindow.Close();
                view.Show();
            }
            else if(tipZaposlenika == "Radnik na pretrazi")
            {

            }

        }
   
        public string UserName
        {
            get { return _txtUserName; }
            set
            {
                if( _txtUserName != value)
                {
                    _txtUserName = value;
                    OnPropertyChanged("UserName");
                }
            }
        }
        public string Password
        {
            get { return _txtPassword; }
            set
            {
                if(_txtPassword != value)
                {
                    
                    _txtPassword = value;
                    OnPropertyChanged("Password");
                }
            }
        }
    }
}
