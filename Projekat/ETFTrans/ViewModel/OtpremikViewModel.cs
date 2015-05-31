using ETFTrans.DataAcces;
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
    public class OtpremikViewModel : BaseViewModel
    {
        private string userName;
        private List<Autobus> _autobusi;
        private string _nazivKompanije;
        private string _registracijaAutobusa;
        private bool _autobuskompanije;
        private bool _straniAutobus;
        private Autobus _selectedAutobus;
        private bool _otisao;
        private bool _dosao;
        private ICommand _btnOtpremi;
        private string _vrijemeOtpreme;
        public string VrijemeOtpreme
        {
            set
            {
                if(_vrijemeOtpreme != value)
                {
                    _vrijemeOtpreme = value;
                    OnPropertyChanged("VrijemeOtpreme");
                }
            }
            get { return _vrijemeOtpreme; }
        }
        public List<Autobus> Autobusi
        {
            set
            {
                if(_autobusi != value)
                {
                    _autobusi = value;
                    OnPropertyChanged("Autobusi");
                    
                }
            }
            get { return _autobusi; }
        }
        public string NazivKompanije
        {
            set
            {
                if(_nazivKompanije != value)
                {
                    _nazivKompanije = value;
                    OnPropertyChanged("NazivKompanije");
                }
            }
            get { return _nazivKompanije; }
        }
        public string RegistracijaAutobusa
        {
            set
            {
                if(_registracijaAutobusa != value)
                {
                    _registracijaAutobusa = value;
                    OnPropertyChanged("RegistracijaAutobusa");
                }
            }
            get { return _registracijaAutobusa; }
        }
        public bool AutobusKompanije
        {
            set
            {
                if(_autobuskompanije != value)
                {
                    _autobuskompanije = value;
                    if (_autobuskompanije)
                    {
                        NazivKompanije = "";
                        NazivKompanije = null;
                        RegistracijaAutobusa = "";
                        napuniComboBoxAutobusima();
                    }
                    
                    OnPropertyChanged("AutobusKompanije");
                }
            }
            get { return _autobuskompanije; }
        }
        public bool StraniAutobus
        {
            set
            {
                if (_straniAutobus != value)
                {
                    _straniAutobus = value;
                    if(_straniAutobus)
                    Autobusi = null;
                    OnPropertyChanged("StraniAutobus");
                }
            }
            get { return _straniAutobus; }
        }
        public Autobus SelectedAutobus
        {
            set
            {
                if(_selectedAutobus != value)
                {
                    _selectedAutobus = value;
                    OnPropertyChanged("SelectedAutobus");
                }
            }
            get { return _selectedAutobus; }
        }
        public bool Otisao
        {
            set
            {
                if(_otisao != value)
                {
                    _otisao = value;
                    if(_otisao)
                        Dosao = false;
                    OnPropertyChanged("Otisao");
                }
            }
            get { return _otisao; }
        }
        public bool Dosao
        {
            set
            {
                if (_dosao != value)
                {
                    _dosao = value;
                    if (_dosao)
                        Otisao = false;
                    OnPropertyChanged("Dosao");
                }
            }
            get { return _dosao; }
        }
        public ICommand BtnOtpremi
        {
            set { _btnOtpremi = value; }
            get { return _btnOtpremi; }
        }
        private void napuniComboBoxAutobusima()
        {
            Autobusi = BazaFunkcije.GetAutobuse();
        }
        private string otprema;

        public OtpremikViewModel()
        {
            AutobusKompanije = true;
            napuniComboBoxAutobusima();
            BtnOtpremi = new RelayCommand(otpremiAutobus);
        }

        public OtpremikViewModel(string UserName, Window view)
        {
            prijavljeniUser = UserName;
            window = view;
            window.Closing += closeHandler1;
            AutobusKompanije = true;
            napuniComboBoxAutobusima();
            BtnOtpremi = new RelayCommand(otpremiAutobus);
        }
        private bool validirajPodatke()
        {
            if (AutobusKompanije)
            {
                if (SelectedAutobus == null)
                {
                    MessageBox.Show("Nijedan autobus nije odabran");
                    return false;
                }

            }
            if (StraniAutobus)
            {
                if (RegistracijaAutobusa == null || RegistracijaAutobusa == "")
                {
                    MessageBox.Show("Nije upisana registracija autobusa!");
                    return false;
                }
                if (NazivKompanije == null || NazivKompanije == "")
                {
                    MessageBox.Show("Molimo upišite naziv kompanije autobusa!");
                    return false;
                }
            }
            try
            {
                DateTime.Parse(VrijemeOtpreme);
            }
            catch (Exception e)
            {
                MessageBox.Show("Vrijeme mora biti u fomatu hh:mm!");
                return false;
            }
            if (!Otisao && !Dosao)
            {
                MessageBox.Show("Nije čekirano da li je autobus došao ili otišao!");
                return false;
            }
            return true;
        }
        
        private void otpremiAutobus()
        {
            if (!validirajPodatke()) return;
            if(Dosao)
               otprema = "Dolazak";
            else 
                otprema = "Odlazak";
            dolazakOdlazakAutobusa novaOtprema;
            if(StraniAutobus)
             novaOtprema = new dolazakOdlazakAutobusa()
            {
                  datumOtpreme = DateTime.Today.Date.ToShortDateString(),
                   linija = null,
                    nazivKompanije = NazivKompanije,
                     stigaoOtisao = otprema,
                      vrijemeOtpreme = VrijemeOtpreme,
                       autobus = null,
                       registracijaAutobusa = RegistracijaAutobusa
            };
            else  
                novaOtprema = new dolazakOdlazakAutobusa()
                {
                    datumOtpreme = DateTime.Today.Date.ToShortDateString(),
                    nazivKompanije = null,
                    stigaoOtisao = otprema,
                    vrijemeOtpreme = VrijemeOtpreme,
                    registracijaAutobusa = SelectedAutobus.registracija
                };

            BazaFunkcije.spremiOtpremu(novaOtprema, SelectedAutobus, SelectedAutobus.linija, new Otpremnik(), AutobusKompanije);
            MessageBox.Show("Autobus uspjesno otpremljen");
        }


    }
}
