using ETFTrans.DataAcces;
using ETFTrans.Model;
using ETFTrans.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ETFTrans.ViewModel
{
    public class ClanUpraveViewModel : BaseViewModel
    {
        private string _nazivStanice;
        private string _cijenaVoznje;
        private ICommand _btnDodajStanicu;
        public string NazivStanice
        {
            set
            {
                if (_nazivStanice != value)
                {
                    _nazivStanice = value;
                    OnPropertyChanged("NazivStanice");
                }
            }
            get
            {
                return _nazivStanice;
            }
        }
        public string CijenaVoznje
        {
            set
            {
                if(_cijenaVoznje != value)
                {
                    _cijenaVoznje = value;
                    OnPropertyChanged("CijenaVoznje");
                }
            }
            get
            {
                return _cijenaVoznje;
            }
        }
        public ICommand BtnDodajStanicu
        {
            set
            {
                if (_btnDodajStanicu != value)
                    _btnDodajStanicu = value;
            }
            get
            {
                return _btnDodajStanicu;
            }
        }

        public ClanUpraveViewModel()
            : base()
        {

            BtnDodajAutobus = new RelayCommand(registrujAutobus);
            BtnIzmjeniAutobus = new RelayCommand(izmjeniAutobus);
            BtnObrisiAutobus = new RelayCommand(obrisiAutobus);
            BtnOdjava = new RelayCommand(odjavaUposlenika);
            BtnDodajAutobusZaLiniju = new RelayCommand(dodajAutobusZaLiniju);
            BtnDodajStanicuZaLiniju = new RelayCommand(dodajStanicuZaLiniju);
            BtnDodajLiniju = new RelayCommand(dodajLiniju);
            BtnDodajNovuStanicu = new RelayCommand(dodajNovuStanicu);
            BtnIzbrisiLiniju = new RelayCommand(izbrisiLiniju);
            BtnPrikaziStaniceLinije = new RelayCommand(prikaziStaniceLinije);
            BtnDodajStanicu = new RelayCommand(dodajNovuStanicuIzStaniceTaba);
            UpdateDataGridAutobusima();
        }

        private void dodajNovuStanicuIzStaniceTaba()
        {
            List<Stanica> staniceIzBaze = BazaFunkcije.dajStanice();
            if(!validirajCijenu(CijenaVoznje)) 
            {
                MessageBox.Show("Cijena mora biti broj");
                return;
            }
            Stanica novaStanica = new Stanica() { nazivGrada = NazivStanice, cijenaVoznje = Decimal.Parse(CijenaVoznje) };
            BazaFunkcije.UpisiStanicuUBazu(novaStanica);
            MessageBox.Show("Stanica uspjesno dodana!");
        }

        private bool validirajCijenu(string CijenaVoznje)
        {
            try
            {
                Decimal.Parse(CijenaVoznje);
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        #region DataGrid-IzmjenaLinijeIStanica

        private List<Linija> dataGridLinije;
        private ICommand _btnIzbrisiLiniju;
        private ICommand _btnPrikaziStaniceLinije;
        private Linija _selectedLinija;
        private PrikazStanicaViewModel dijetePrikazStanicaViewModel;
        public List<Linija> DataGridLinije
        {
            set
            {
                if (dataGridLinije != value)
                {
                    dataGridLinije = value;
                    OnPropertyChanged("DataGridLinije");
                }
            }
            get { return dataGridLinije; }
        }
        public ICommand BtnIzbrisiLiniju
        {
            set { _btnIzbrisiLiniju = value; }
            get { return _btnIzbrisiLiniju; }
        }
        public ICommand BtnPrikaziStaniceLinije
        {
            set
            {
                if (_btnPrikaziStaniceLinije != value)
                    _btnPrikaziStaniceLinije = value;
            }
            get { return _btnPrikaziStaniceLinije; }
        }

        public Linija SelectedLinija
        {
            set
            {
                if(_selectedLinija != value)
                {
                    _selectedLinija = value;
                    OnPropertyChanged("SelectedLinija");
                }
            }
            get { return _selectedLinija; }
        }

        private void UpdateDataGridLinijama()
        {
            DataGridLinije = BazaFunkcije.dajLinije();
        }
     
        

        private void prikaziStaniceLinije()
        {
            if(SelectedLinija == null)
            {
                MessageBox.Show("Nijedna linija nije selektovana!");
                return;
            }
            dijetePrikazStanicaViewModel = new PrikazStanicaViewModel(SelectedLinija.staniceLinije);
            PrikazStanicaView staniceWindow = new PrikazStanicaView();
            staniceWindow.DataContext = dijetePrikazStanicaViewModel;
            staniceWindow.Show();
            staniceWindow.Closed += handlerPrikazStanica;
        }

        private void handlerPrikazStanica(object sender, EventArgs e)
        {
            SelectedLinija.staniceLinije = dijetePrikazStanicaViewModel.ListaStanica;
            sortirajStanice(SelectedLinija.staniceLinije);
            SelectedLinija.odredisteLinije = SelectedLinija.staniceLinije[SelectedLinija.staniceLinije.Count - 1].nazivGrada;
            SelectedLinija.cijenaZaGlavnoOdrediste = SelectedLinija.staniceLinije[SelectedLinija.staniceLinije.Count - 1].cijenaVoznje;
            BazaFunkcije.spremiIzmjenuLinijeZaStanice(SelectedLinija, dijetePrikazStanicaViewModel.listaIzbrisanihStanica, dijetePrikazStanicaViewModel.listaDodanihStanica);
            UpdateDataGridLinijama();
        }
        private void sortirajStanice(List<Stanica> stanice)
        {
            Stanica temp;
            for(int i = 0; i<stanice.Count-1; i++)
                for(int j=i+1; j<stanice.Count; j++)
                {
                    if(stanice[i].cijenaVoznje > stanice[j].cijenaVoznje)
                    {
                        temp = stanice[i];
                        stanice[i] = stanice[j];
                        stanice[j] = temp;
                    }
                }
        }

        private void izbrisiLiniju()
        {
            if (SelectedLinija == null)
            {
                MessageBox.Show("Nijedna linija nije selektovana!");
                return;
            }
            MessageBoxResult dr = MessageBox.Show("Da li ste sigurni?", "Oprez!", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (dr == MessageBoxResult.Yes)
            {
                BazaFunkcije.izbrisiLiniju(SelectedLinija);
                UpdateDataGridLinijama();
                UpdateListBoxAutobusima();
                MessageBox.Show("Linija uspjesno obrisana!");
            }
            
        }

        #endregion


        # region DodavanjeLinije
        private string _vrijemePolaskaLinije;
        private string _vrijemeDolaskaLinije;
        private DaniVoznjeLinije _daniVoznjeLinije = new DaniVoznjeLinije();
        private bool _linijeTabSelected;
        private ICommand _btnDodajAutobusZaLiniju;
        private ICommand _btnDodajStanicuZaLiniju;
        private ICommand _btnDodajLiniju;
        private ICommand _btnDodajNovuStanicu;
        private Autobus _selectedAutobusZaLiniju;
        private Stanica _selectedStanicaZaLiniju;
        private List<Autobus> tempAutobusiZaLiniju = new List<Autobus>();
        private List<Stanica> tempStaniceZaLiniju = new List<Stanica>();
        private StanicaViewModel dijeteViewModel;
        
        private List<Stanica> _stanice = new List<Stanica>();
        private List<Autobus> _autobusi = new List<Autobus>();

        public Autobus SelectedAutobusZaLiniju
        {
            set
            {
                if(_selectedAutobusZaLiniju != value)
                {
                    _selectedAutobusZaLiniju = value;
                    OnPropertyChanged("SelectedAutobusZaLiniju");
                }
            }
            get { return _selectedAutobusZaLiniju; }
        }
        public Stanica SelectedStanicaZaLiniju
        {
            set
            {
                if(_selectedStanicaZaLiniju != value)
                {
                    _selectedStanicaZaLiniju = value;
                    OnPropertyChanged("SelectedStanicaZaLiniju");
                }
            }
            get { return _selectedStanicaZaLiniju; }
        }
        public ICommand BtnDodajLiniju
        {
            set
            {
                if (_btnDodajLiniju != value)
                    _btnDodajLiniju = value;
            }
            get { return _btnDodajLiniju; }
        }
        
        public ICommand BtnDodajStanicuZaLiniju
        {
            set
            {
                if (_btnDodajStanicuZaLiniju != value)
                    _btnDodajStanicuZaLiniju = value;
            }
            get
            {
                return _btnDodajStanicuZaLiniju;
            }
        }

        public ICommand BtnDodajAutobusZaLiniju
        {
            set 
            {
                if (_btnDodajAutobusZaLiniju != value)
                    _btnDodajAutobusZaLiniju = value;
            }
            get { return _btnDodajAutobusZaLiniju; }

                
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

        public string VrijemePolaskaLinije
        {
            set
            {
                if(_vrijemePolaskaLinije != value)
                {
                    _vrijemePolaskaLinije = value;
                    OnPropertyChanged("VrijemePolaskaLinije");
                }
            }
            get { return _vrijemePolaskaLinije; }
        }
        public string VrijemeDolaskaLinije
        {
            set
            {
                if (_vrijemeDolaskaLinije != value)
                {
                    _vrijemeDolaskaLinije = value;
                    OnPropertyChanged("VrijemeDolaskaLinije");
                }
            }
            get { return _vrijemeDolaskaLinije; }
        }
        public DaniVoznjeLinije daniVoznjeLinije
        {
            set
            {
                if(_daniVoznjeLinije != value)
                {
                    _daniVoznjeLinije = value;
                    OnPropertyChanged("DaniVoznjeLinije");
                }
            }
            get { return _daniVoznjeLinije; }
        }
        public List<Autobus> ListAutobusi
        {
            set
            {
                if(_autobusi != value)
                {
                    _autobusi = value;
                    OnPropertyChanged("ListAutobusi");
                }
            }
            get { return _autobusi; }
        }
        public List<Stanica> ListStanica
        {
            set
            {
                if(_stanice != value)
                {
                    _stanice = value;
                    OnPropertyChanged("ListStanica");
                }
            }
            get { return _stanice; }
        }
        public bool LinijeTabSelected
        {
            set
            {
                if(_linijeTabSelected != value)
                {
                    _linijeTabSelected = value;
                    if(_linijeTabSelected)
                    {
                        UpdateListBoxAutobusima();
                        UpdateListBoxStanicama();
                        UpdateDataGridLinijama();
                    }
                    OnPropertyChanged("LinijeTabSelected");
                }
            }
            get { return _linijeTabSelected; }
        }
        
        public void UpdateListBoxStanicama()
        {
            ListStanica = BazaFunkcije.dajStanice();
        }
        public void UpdateListBoxAutobusima()
        {
            ListAutobusi = BazaFunkcije.GetAutobuseSaLinijama();
        }
       

        private void dodajNovuStanicu()
        {
            StanicaView novaStanicaView = new StanicaView();
            dijeteViewModel = new StanicaViewModel(novaStanicaView);
            novaStanicaView.DataContext = dijeteViewModel;
            novaStanicaView.Show();
            novaStanicaView.Closed += hanlder;  
        }

        private void hanlder(object sender, EventArgs e)
        {

            bool f = tempStaniceZaLiniju.Exists(x => x.nazivGrada == dijeteViewModel.NazivStanice);
            if(f)
            {
                MessageBox.Show("Stanica vec postoji!");
                return;
            }
            f = ListStanica.Exists(x => x.nazivGrada == dijeteViewModel.NazivStanice);
            if (f)
            {
                MessageBox.Show("Stanica vec postoji!");
                return;
            }
            if (dijeteViewModel.novaStanica == null) return;
            tempStaniceZaLiniju.Add(dijeteViewModel.novaStanica);
            BazaFunkcije.UpisiStanicuUBazu(dijeteViewModel.novaStanica);
            UpdateListBoxStanicama();
            MessageBox.Show("Stanica uspješno dodana!!");
        }

        private void dodajLiniju()
        {
            if(validirajVrijeme(VrijemeDolaskaLinije) && validirajVrijeme(VrijemePolaskaLinije))
            {
                if(tempAutobusiZaLiniju.Count == 0 || tempStaniceZaLiniju.Count == 0)
                {
                    MessageBox.Show("Nijedan autobus ili stanica nisu selektovani!");
                        return;
                }

                sortirajStanice(tempStaniceZaLiniju);
              
                Linija novaLinija = new Linija()
                {
                    odredisteLinije = tempStaniceZaLiniju[tempStaniceZaLiniju.Count-1].nazivGrada,
                    cijenaZaGlavnoOdrediste = tempStaniceZaLiniju[tempStaniceZaLiniju.Count-1].cijenaVoznje,
                    daniVoznje = daniVoznjeLinije,
                    vrijemeDolaska = DateTime.Parse(VrijemeDolaskaLinije),
                    vrijemePolaska = DateTime.Parse(VrijemePolaskaLinije)
                };
                

                novaLinija.brojRaspolozivihMjesta = 0;
                for (int i = 0; i < tempAutobusiZaLiniju.Count; i++)
                    novaLinija.brojRaspolozivihMjesta += tempAutobusiZaLiniju[i].brojRaspolozivihMjesta;
                novaLinija.brojSlobodnihMjesta = novaLinija.brojRaspolozivihMjesta;

                BazaFunkcije.dodajLinijuUBazu(novaLinija, tempAutobusiZaLiniju, tempStaniceZaLiniju);
                UpdateDataGridLinijama();
                UpdateListBoxAutobusima();
                SelectedAutobusZaLiniju = null;
                tempStaniceZaLiniju.Clear();
                VrijemePolaskaLinije = "";
                VrijemeDolaskaLinije = "";
                postaviDaneVoznjeNaFalse(daniVoznjeLinije);
                MessageBox.Show("Linija uspjesno dodana!");

            }
        }

        private void postaviDaneVoznjeNaFalse(DaniVoznjeLinije daniVoznjeLinije)
        {
            daniVoznjeLinije = new DaniVoznjeLinije();
        }

        
        private bool validirajVrijeme(string vrijeme)
        {
            try
            {
                DateTime tm = DateTime.Parse(vrijeme);
                return true;
            }
            catch(Exception e)
            {
                MessageBox.Show("Vrijeme mora biti u formatu HH:mm !");
                return false;
            }
            return false;
        }

        private void dodajStanicuZaLiniju()
        {
            Stanica novaStanicaZaLiniju;
            if(SelectedStanicaZaLiniju != null)
            {
                novaStanicaZaLiniju = SelectedStanicaZaLiniju;

                bool f = tempStaniceZaLiniju.Exists(x => x.nazivGrada == SelectedStanicaZaLiniju.nazivGrada);
                if (f)
                {
                    MessageBox.Show("Stanica vec postoji!");
                    return;
                }
                tempStaniceZaLiniju.Add(novaStanicaZaLiniju);
                MessageBox.Show("Stanica uspjesno dodana!");
                
            }
            
        }
        private void dodajAutobusZaLiniju()
        {
                Autobus noviBusZaLiniju;
                if (SelectedAutobusZaLiniju != null)
                {
                    if(SelectedAutobusZaLiniju.linija != null)
                    {
                        MessageBox.Show("Nije moguće: Autobus saobraća na drugoj liniji!");
                        return;
                    }
                    
                    noviBusZaLiniju = SelectedAutobusZaLiniju;
                    tempAutobusiZaLiniju.Add(noviBusZaLiniju);
                    MessageBox.Show("Autobus uspješno dodan na liniju!");
                }
           
        }
        #endregion


        #region ImplementacijaAutobusTab
        private string statusBarText;
        private string markaAutobusa;
        private DateTime datumProizvodnjeAutobusa = DateTime.Now;
        private int brojRaspolozivihMjestaAutobusa;
        private DateTime datumRegistracijeAutobusa = DateTime.Now;
        private string registracijaAutobusa;
        private ICommand btnDodajAutobus;
        private ICommand btnIzmjeniAutobus;
        private ICommand btnObrisiAutobus; 
        private bool tabAutobusiSelected = true;
        private List<Autobus> dataGridAutobusi;
        private Autobus selectedAutobusInDataGrid;
        

        public string MarkaAutobusa
        {
            set
            {
                if(markaAutobusa != value)
                {
                    markaAutobusa = value;
                    OnPropertyChanged("MarkaAutobusa");
                }
            }
            get
            {
                return markaAutobusa;
            }
        }
        public DateTime DatumProizvodnjeAutobusa
        {
            set
            {
                if(datumProizvodnjeAutobusa != value)
                {
                    datumProizvodnjeAutobusa = value;
                    OnPropertyChanged("DatumProizvodnjeAutobusa");
                }
            }
            get { return datumProizvodnjeAutobusa; }
        }
        public int BrojRaspolozivihMjestaAutobusa
        {
            set
            {
                if(brojRaspolozivihMjestaAutobusa != value)
                {
                    brojRaspolozivihMjestaAutobusa = value;
                    OnPropertyChanged("BrojRaspolozivihMjestaAutobusa");
                }
            }
            get { return brojRaspolozivihMjestaAutobusa; }
        }
        public DateTime DatumRegistracijeAutobusa
        {
            set
            {
                if(datumRegistracijeAutobusa != value)
                {
                    datumRegistracijeAutobusa = value;
                    OnPropertyChanged("DatumRegistracijeAutobusa");
                }
            }
            get { return datumRegistracijeAutobusa; }
        }
        public string RegistracijaAutobusa
        {
            set { 
                  if(registracijaAutobusa != value)
                  {
                      registracijaAutobusa = value;
                      OnPropertyChanged("RegistracijaAutobusa");
                  }
            }
            get { return registracijaAutobusa; }
        }     
        public ICommand BtnDodajAutobus
        {
            set
            {
                if(btnDodajAutobus != value)
                {
                    btnDodajAutobus = value;
                }
            }
            get
            {
                return btnDodajAutobus;
            }
        }
        public string StatusBarText
        {
            set
            {
                if (statusBarText != value)
                {
                    statusBarText = value;
                    OnPropertyChanged("StatusBarText");
                }
            }
            get { return statusBarText; }

        }
        public bool TabAutobusiSelected
        {
            set
            {
                if(tabAutobusiSelected != value)
                {
                    tabAutobusiSelected = value;
                    OnPropertyChanged("TabAutobusiSelected");
                    UpdateDataGridAutobusima();
                }
            }
            get
            {
                return tabAutobusiSelected;
            }
        }
        public List<Autobus> DataGridAutobusi
        {
            set {
                if (dataGridAutobusi != value)
                {
                    dataGridAutobusi = value;
                    OnPropertyChanged("DataGridAutobusi");
                }
                }
            get { return dataGridAutobusi; }
        }
        public ICommand BtnIzmjeniAutobus
        {
            set
            {
                if (btnIzmjeniAutobus != value)
                    btnIzmjeniAutobus = value;
            }
            get { return btnIzmjeniAutobus; }
        }
        public ICommand BtnObrisiAutobus
        {
            set
            {
                if (btnObrisiAutobus != value)
                    btnObrisiAutobus = value;
            }
            get
            {
                return btnObrisiAutobus;
            }
        }
        public Autobus SelectedAutobusInDataGrid
        {
            set
            {
                if(selectedAutobusInDataGrid != value)
                {
                    selectedAutobusInDataGrid = value;
                    OnPropertyChanged("SelectedAutobuInDataGrid");
                }
            }
            get
            {
                return selectedAutobusInDataGrid;
            }
        }    
        public void izmjeniAutobus()
        {
           if (selectedAutobusInDataGrid != null)
           {
                BazaFunkcije.izmjeniAutobus(SelectedAutobusInDataGrid);
                UpdateDataGridAutobusima();
                StatusBarText = "Izmjena obavljena uspješno!!";
           }
           else
           {
                 StatusBarText = "Nije moguća izmjena-nijedan autobus nije selektovan!!";
           }
        }
        public void obrisiAutobus()
        {
            if (selectedAutobusInDataGrid != null)
            {
                BazaFunkcije.obrisiAutobus(selectedAutobusInDataGrid);
                UpdateDataGridAutobusima();
                StatusBarText = "Brisanje obavljeno uspješno!";
            }
            else
            {
                StatusBarText = "Nije moguće brisanje-nijedan autobus nije selektovan!!";
            }
        }
        public void registrujAutobus()
        {
            Autobus noviAutobus = new Autobus() { marka = MarkaAutobusa, datumProizvodnje = DatumProizvodnjeAutobusa, datumRegistracije = DatumRegistracijeAutobusa, brojRaspolozivihMjesta = BrojRaspolozivihMjestaAutobusa, registracija = RegistracijaAutobusa };
            /*ThreadStart nit = new ThreadStart(new Action(() =>  */     
            BazaFunkcije.upisiAutobusUBazu(noviAutobus);  // ));
            UpdateDataGridAutobusima();
            StatusBarText = "Autobus uspjesno dodan!!";
            
        }
        public void UpdateDataGridAutobusima()
        {
            if (DataGridAutobusi == null)
                DataGridAutobusi = new List<Autobus>();
            DataGridAutobusi = BazaFunkcije.GetAutobusiSaVozacem();
        }
        #endregion 

       
    }
}
