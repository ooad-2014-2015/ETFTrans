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
    public class DirektorViewModel : BaseViewModel
    {
        private List<string> _mjeseci = new List<string>() { "Januar", "Februar", "Mart", "April", "Maj", "Juni", "Juli", "August", "Septembar", "Oktobar", "Novembar", "Decembar" };
        private List<string> _sedmice = new List<string>() { "Prva", "Druga", "Treca", "Cetvrta"};
        private bool _mjesecniIzvjestaj;
        private bool _sedmicniIzvjestaj;
        private bool _izvjestajNaDatum;
        private string _mjesec;
        private string _sedmica;
        private DateTime _naDatum = DateTime.Today;
        private DateTime pocetniDatum;
        private DateTime krajnjiDatum;
        private List<Log> _uposlenici;
        private ICommand _btmGenerisiIzvjestaj;
        private string UserName;
        private View.DirektorView view;
        public ICommand BtnGenerisiIzvjestaj
        {
            set
            {
                _btmGenerisiIzvjestaj = value;
            }
            get { return _btmGenerisiIzvjestaj; }
        }
        public List<Log> Uposlenici
        {
            set
            {
                if(_uposlenici != value)
                {
                    _uposlenici = value;
                    OnPropertyChanged("Uposlenici");
                }
            }
            get { return _uposlenici; }
        }
        public List<string> Mjeseci
        {
            set
            {
                _mjeseci = value;
            }
            get { return _mjeseci; }
        }
        public List<string> Sedmice
        {
            set
            {
                _sedmice = value;
            }
            get { return _sedmice; }
        }
        public bool MjesecniIzvjestaj
        {
            set
            {
                if(_mjesecniIzvjestaj != value)
                {
                    _mjesecniIzvjestaj = value;
                    if(_mjesecniIzvjestaj)
                    {
                        SelectedSedmica = null;
                    }
                    OnPropertyChanged("MjesecniIzvjestaj");
                }
            }
            get { return _mjesecniIzvjestaj; }
        }
        public bool SedmicniIzvjestaj
        {
            set
            {
                if (_sedmicniIzvjestaj != value)
                {
                    _sedmicniIzvjestaj = value;
                    if (_sedmicniIzvjestaj)
                        SelectedMjesec = null;
                    OnPropertyChanged("SedmicniIzvjestaj");
                }
            }
            get { return _sedmicniIzvjestaj; }
        }
        public bool IzvjestajNaDatum
        {
            set
            {
                if (_izvjestajNaDatum != value)
                {
                    _izvjestajNaDatum = value;
                    OnPropertyChanged("IzvjestajNaDatum");
                }
            }
            get { return _izvjestajNaDatum; }
        }
        public string SelectedMjesec
        {
            set
            {
                if(_mjesec != value)
                {
                    _mjesec = value;
                    postaviDatumeZaOdabraniMjesec(ref pocetniDatum, ref krajnjiDatum);
                    OnPropertyChanged("SelectedMjesec");
                }
            }
            get { return _mjesec; }
        }
        public string SelectedSedmica
        {
            set
            {
                if (_sedmica != value)
                {
                    _sedmica = value;
                    postaviDatumeZaOdabranuSedmicu(ref pocetniDatum,ref krajnjiDatum);
                    OnPropertyChanged("SelectedSedmica");
                }
            }
            get { return _sedmica; }
        }
        public DateTime SelectedDatum
        {
            set
            {
                if(_naDatum != value)
                {
                    _naDatum = value;
                    OnPropertyChanged("SelectedDatum");
                }
            }
            get { return _naDatum; }
        }

        public DirektorViewModel()
        {
            MjesecniIzvjestaj = true;
            BtnGenerisiIzvjestaj = new RelayCommand(generisiIzvjestaj);
        }

        public DirektorViewModel(string UserName, View.DirektorView view)
        {
            prijavljeniUser = UserName;
            window = view;
            window.Closing += closeHandler1;
            MjesecniIzvjestaj = true;
            BtnGenerisiIzvjestaj = new RelayCommand(generisiIzvjestaj);
        }

        private void generisiIzvjestaj()
        {
            if(MjesecniIzvjestaj)
            {
                if(SelectedMjesec == null)
                {
                    MessageBox.Show("Nije odabran mjesec!");
                    return;
                }
                Uposlenici = BazaFunkcije.generisiIzvjestajIzmeduDatuma(pocetniDatum, krajnjiDatum);
            }
            else if (SedmicniIzvjestaj)
            {
                if (SelectedSedmica == null)
                {
                    MessageBox.Show("Nije odabran mjesec!");
                    return;
                }
                Uposlenici = BazaFunkcije.generisiIzvjestajIzmeduDatuma(pocetniDatum, krajnjiDatum);
            }
            else if(IzvjestajNaDatum)
            {
                Uposlenici = BazaFunkcije.generisiIzvjestajNaDatum(SelectedDatum);
            }

        }

        private void postaviDatumeZaOdabraniMjesec(ref DateTime pocetak, ref DateTime kraj)
        {
            for(int i=0; i< Mjeseci.Count; i++)
            {
                if(Mjeseci[i] == SelectedMjesec)
                {
                    pocetak = DateTime.Parse( "1."+ (i+1).ToString()+"." + DateTime.Today.Year.ToString());
                    if(i == 1 && DateTime.IsLeapYear(DateTime.Today.Year))
                    {
                        kraj = DateTime.Parse("29.2." + DateTime.Today.Year.ToString());
                    }
                    else if(i==1 && !DateTime.IsLeapYear(DateTime.Today.Year))
                    {
                        kraj = DateTime.Parse("28.2." + DateTime.Today.Year.ToString());
                    }
                    else if( i % 2 == 0 && i < 7)
                    {
                        kraj = DateTime.Parse("31."+ (i+1).ToString()+"." + DateTime.Today.Year.ToString());
                    }
                    else if( i % 2 == 1 && i>=7 )
                    {
                        kraj = DateTime.Parse("31." + (i + 1).ToString() + "." + DateTime.Today.Year.ToString());
                    }
                    else
                    {
                        kraj = DateTime.Parse("30." + (i + 1).ToString() + "." + DateTime.Today.Year.ToString());
                    }

                    break;


                }
            }

        }

        private void postaviDatumeZaOdabranuSedmicu(ref DateTime pocetak, ref DateTime kraj)
        {
            for(int i=0; i<Sedmice.Count; i++)
            {
                if (Sedmice[i] == SelectedSedmica)
                {
                    if (i == 0)
                    {
                        pocetak = DateTime.Parse("1." + DateTime.Today.Month.ToString() + "." + DateTime.Today.Year.ToString());
                        kraj = pocetak.AddDays(6);
                    }
                    else
                    {
                        pocetak = DateTime.Parse((i * 7).ToString() + "." + DateTime.Today.Month.ToString() + "." + DateTime.Today.Year.ToString());
                        if (i == 3)
                        {
                            kraj = pocetak.AddMonths(1);
                            kraj = kraj.AddDays(-pocetak.Day);
                         
                        }
                        else
                            kraj = pocetak.AddDays(6);
                    }
                    break;
                }
            }
            
        }

    }
}
