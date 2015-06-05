using ETFTrans.DataAcces;
using ETFTrans.Model;
using ETFTrans.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ETFTrans.ViewModel
{
    public class ProdajaKarataViewModel : BaseViewModel
    {
        #region OdabirOdredisteILinije
        private List<Stanica> _svaOdredista;
        private List<Linija> _linijeZaOdrediste;
        private bool _tabProdajaSelected = true;
        private bool _pretragaPoDatumu;
        private bool _pretragaPoDanu;
        private Stanica _selectedOdrediste;
        private Linija _selectedLinija;
        private DateTime _datumPretrage = DateTime.Today;
        private bool _ponedjeljak;
        private bool _utorak;
        private bool _srijeda;
        private bool _cetvrtak;
        private bool _petak;
        private bool _subota;
        private bool _nedjelja;
        public List<Stanica> ListaOdredista
        {
            set
            {
                if(_svaOdredista != value)
                {
                    _svaOdredista = value;
                    OnPropertyChanged("ListaOdredista");
                }
            }
            get { return _svaOdredista; }
        }
        public List<Linija> LinijeOdredista
        {
            set
            {
                if(_linijeZaOdrediste != value)
                {
                    _linijeZaOdrediste = value;
                    OnPropertyChanged("LinijeOdredista");
                }
            }
            get { return _linijeZaOdrediste; }
        }
        public bool TabProdajaSelected
        {
            set
            {
                if(_tabProdajaSelected != value)
                {
                    _tabProdajaSelected = value;
                    SelectedOdrediste = null;
                    LinijeOdredista = null;
                    PretragaPoDanu = true;
                    Rezervacija = false;
                    postaviDanPretrage();
                    OnPropertyChanged("TabProdajaSelected");
                }
            }
            get { return _tabProdajaSelected; }
        }
        public bool PretragaPoDatumu
        {
            set
            {
                if(_pretragaPoDatumu != value)
                {
                    _pretragaPoDatumu = value;
                    PretragaPoDanu = !value;
                    if (SelectedOdrediste != null)
                        napuniDataGridLinijama();
                    OnPropertyChanged("PretragaPoDatumu");
                }
            }
            get { return _pretragaPoDatumu; }
        }
        public bool PretragaPoDanu
        {
            set
            {
                if (_pretragaPoDanu != value)
                {
                    _pretragaPoDanu = value;
                    PretragaPoDatumu = !value;
                    OnPropertyChanged("PretragaPoDanu");
                }
            }
            get { return _pretragaPoDanu; }
        }
        public Stanica SelectedOdrediste
        {
            set
            {
                if(_selectedOdrediste != value)
                {
                    _selectedOdrediste = value;
                    if (_selectedOdrediste != null)
                    {
                        cijenaVoznjeDoOdredista = _selectedOdrediste.cijenaVoznje;
                        napuniDataGridLinijama();
                    }
                    else
                    {
                        SelectedLinija = null;
                        LinijeOdredista = null;
                    }
                    OnPropertyChanged("SelectedOdrediste");
                }
            }
            get { return _selectedOdrediste; }
        }
        public Linija SelectedLinija
        {
            set
            {
                if (_selectedLinija != value)
                {
                    _selectedLinija = value;
                    if(_selectedLinija != null && _selectedLinija.brojSlobodnihMjesta == 0)
                    {
                        MessageBox.Show("Zao nam je sva slobodna mjesta su zauzeta!");
                        _selectedLinija = null;
                    }
                    CijenaVoznje = postaviCijenu();
                    OnPropertyChanged("SelectedLinija");
                }
            }
            get { return _selectedLinija; }
        }
        public DateTime DatumPretrage
        {
            set
            {
                if(_datumPretrage != value)
                {
                    _datumPretrage = value;
                    if (SelectedOdrediste != null)
                        napuniDataGridLinijama();
                    OnPropertyChanged("DatumPretrage");
                }
            }
            get { return _datumPretrage; }
        }
        public bool Ponedjeljak
        {
            set
            {
                if(_ponedjeljak != value)
                {
                    _ponedjeljak = value;
                    if (SelectedOdrediste != null)
                        napuniDataGridLinijama();
                    OnPropertyChanged("Ponedjeljak");
                }
            }
            get { return _ponedjeljak; }
        }
        public bool Utorak
        {
            set
            {
                if (_utorak != value)
                {
                    _utorak = value;
                    if (SelectedOdrediste != null)
                        napuniDataGridLinijama();
                    OnPropertyChanged("Utorak");
                }
            }
            get { return _utorak; }
        }
        public bool Srijeda
        {
            set
            {
                if (_srijeda != value)
                {
                    _srijeda = value;
                    if (SelectedOdrediste != null)
                        napuniDataGridLinijama();
                    OnPropertyChanged("Srijeda");
                }
            }
            get { return _srijeda; }
        }
        public bool Cetvrtak
        {
            set
            {
                if (_cetvrtak != value)
                {
                    _cetvrtak = value;
                    if (SelectedOdrediste != null)
                        napuniDataGridLinijama();
                    OnPropertyChanged("Cetvrtak");
                }
            }
            get { return _cetvrtak; }
        }
        public bool Petak
        {
            set
            {
                if (_petak != value)
                {
                    _petak = value;
                    if (SelectedOdrediste != null)
                        napuniDataGridLinijama();
                    OnPropertyChanged("Petak");
                }
            }
            get { return _petak; }
        }
        public bool Subota
        {
            set
            {
                if (_subota != value)
                {
                    _subota = value;
                    if (SelectedOdrediste != null)
                        napuniDataGridLinijama();
                    OnPropertyChanged("Subota");
                }
            }
            get { return _subota; }
        }
        public bool Nedjelja
        {
            set
            {
                if (_nedjelja != value)
                {
                    _nedjelja = value;
                    if (SelectedOdrediste != null)
                        napuniDataGridLinijama();
                    OnPropertyChanged("Nedjelja");
                }
            }
            get { return _nedjelja; }
        }
        
        private void napuniDataGridLinijama()
        {
            if (SelectedOdrediste == null)
            {
                LinijeOdredista = new List<Linija>();
                return;
            }
            
            List<Linija> linije = BazaFunkcije.dajLinijeZaOdrediste(SelectedOdrediste);
            LinijeOdredista = postaviBrojSlobodnihMjesta(formatirajLinijePoDanu(linije));
           // MessageBox.Show(LinijeOdredista.Count.ToString());
           
        }

        private List<Linija> postaviBrojSlobodnihMjesta(List<Linija> linije)
        {
            
            if (PretragaPoDanu)
            {
               
               foreach (Linija x in linije)
                {
                    if (x.datumiPolaskaLinije.Count == 0) continue;
                    foreach (DatumPolaskaLinije y in x.datumiPolaskaLinije)
                    {
                        if (y.datumPolaska.Date.ToString() == vratiDatumDana(vratiDan()).Date.ToString())
                        {
                            x.brojSlobodnihMjesta = y.brojSlobodnihMjesta;
                            break;
                        }
                    }
                }
               
            }
            else
            {
                foreach (Linija x in linije)
                {
                    if (x.datumiPolaskaLinije == null) break;
                    foreach (DatumPolaskaLinije y in x.datumiPolaskaLinije)
                    {
                        if (y.datumPolaska.Date == DatumPretrage.Date)
                        {
                            x.brojSlobodnihMjesta = y.brojSlobodnihMjesta;
                            break;
                        }
                    }
                }
            }
            return linije;

        }

        private List<Linija> formatirajLinijePoDanu(List<Linija> listaLinija)
        {
            List<Linija> novaListaLinija = new List<Linija>();
            switch (vratiDan())
            {
                case DayOfWeek.Monday:
                    {
                       
                        foreach (Linija a in listaLinija)
                        {
                            if (a.daniVoznje.pon)
                                novaListaLinija.Add(a);
                        }
                        break;
                    }
                case DayOfWeek.Tuesday:
                    {
                        foreach (Linija a in listaLinija)
                        {
                            if (a.daniVoznje.uto)
                                novaListaLinija.Add(a);
                        }
                        break;
                    }
                case DayOfWeek.Wednesday:
                    {
                        foreach (Linija a in listaLinija)
                        {
                            if (a.daniVoznje.sri)
                                novaListaLinija.Add(a);
                        }
                        break;
                    }
                case DayOfWeek.Thursday:
                    {
                        foreach (Linija a in listaLinija)
                        {
                            if (a.daniVoznje.cet)
                                novaListaLinija.Add(a);
                        }
                        break;
                    }
                case DayOfWeek.Friday:
                    {
                        foreach (Linija a in listaLinija)
                        {
                            if (a.daniVoznje.pet)
                                novaListaLinija.Add(a);
                        }
                        break;
                    }
                case DayOfWeek.Saturday:
                    {
                        foreach (Linija a in listaLinija)
                        {
                            if (a.daniVoznje.sub)
                                novaListaLinija.Add(a);
                        }
                        break;
                    }
                case DayOfWeek.Sunday:
                    {
                        foreach (Linija a in listaLinija)
                        {
                            if (a.daniVoznje.ned)
                                novaListaLinija.Add(a);
                        }
                        break;
                    }

            }
            return novaListaLinija;
        }
        private void napuniComboBoxOdredistima()
        {
            ListaOdredista = BazaFunkcije.dajStanice();
        }
        private DayOfWeek vratiDan()
        {
            if (PretragaPoDanu)
            {
                if (Ponedjeljak) return DayOfWeek.Monday;
                else if (Utorak) return DayOfWeek.Tuesday;
                else if (Srijeda) return DayOfWeek.Wednesday;
                else if (Cetvrtak) return DayOfWeek.Thursday;
                else if (Petak) return DayOfWeek.Friday;
                else if (Subota) return DayOfWeek.Saturday;
                else return DayOfWeek.Sunday;
            }
            else
                return DatumPretrage.DayOfWeek;
            
        }
        private DateTime vratiDatumDana(DayOfWeek dan)
        {
            DateTime datum = DateTime.Today;
            while (datum.DayOfWeek.ToString() != dan.ToString())
            {
                datum = datum.AddDays(1);
            }
            return datum.Date;
        }
        private void postaviDanPretrage()
        {
            if (DateTime.Today.DayOfWeek == DayOfWeek.Monday)
                Ponedjeljak = true;
            if (DateTime.Today.DayOfWeek == DayOfWeek.Tuesday)
                Utorak = true;
            if (DateTime.Today.DayOfWeek == DayOfWeek.Wednesday)
                Srijeda = true;
            if (DateTime.Today.DayOfWeek == DayOfWeek.Thursday)
                Cetvrtak = true;
            if (DateTime.Today.DayOfWeek == DayOfWeek.Friday)
                Petak = true;
            if (DateTime.Today.DayOfWeek == DayOfWeek.Saturday)
                Subota = true;
            if (DateTime.Today.DayOfWeek == DayOfWeek.Sunday)
                Nedjelja = true;    
        }
        #endregion

        #region ProdajaRezervacijaKarte
        private List<Korisnik> _listaKorisnika;
        private decimal cijenaVoznjeDoOdredista;
        private decimal cijenaSaPopustom;
        private string _cijenaVoznje;
        private bool _penzioner;
        private bool _student;
        private bool _registriraniKorisnik;
        private string _imeKorisnika;
        private bool _rezervacija;
        private bool _btnPotvrdiKupovinuEnabled;
        private string _rezervacijaNaIme;
        private bool _rezervacijaNaImeEnabled;
        public bool RezervacijaNaImeEnabled
        {
            set
            {
                if(_rezervacijaNaImeEnabled != value)
                {
                    _rezervacijaNaImeEnabled = value;
                    OnPropertyChanged("RezervacijaNaImeEnabled");
                }
            }
            get { return _rezervacijaNaImeEnabled; }
        }
     
        private Korisnik _selectedKorisnik;
        private ICommand _btnKupiKartu;
        private ICommand _btnRezervisiKartu;
        public string RezervacijaNaIme
        {
            set
            {
                if(_rezervacijaNaIme != value)
                {
                    _rezervacijaNaIme = value;
                    OnPropertyChanged("RezervacijaNaIme");
                }
            }
            get { return _rezervacijaNaIme; }
        }
        public bool BtnPotvrdiKupovinuEnabled
        {
            set
            {
                if(_btnPotvrdiKupovinuEnabled != value)
                {
                    _btnPotvrdiKupovinuEnabled = value;
                    OnPropertyChanged("BtnPotvrdiKupovinuEnabled");
                }
            }
            get { return _btnPotvrdiKupovinuEnabled; }
        }
        public bool Rezervacija
        {
            set
            {
                if(_rezervacija != value)
                {
                    _rezervacija = value;
                    if(!RegistriraniKorisnik || !value)
                    RezervacijaNaImeEnabled = value;
                    if (RegistriraniKorisnik && SelectedKorisnik != null && _rezervacija)
                        RezervacijaNaIme = SelectedKorisnik.ime + " " + SelectedKorisnik.prezime;

                    BtnPotvrdiKupovinuEnabled = !value;
                    if(!_rezervacija)
                    {
                        RezervacijaNaIme = "";
                    }
                    OnPropertyChanged("Rezervacija");
                }
            }
            get
            {
                return _rezervacija;
            }
        }
        public List<Korisnik> ListaKorisnika
        {
            set
            {
                if(_listaKorisnika != value)
                {
                    _listaKorisnika = value;
                    OnPropertyChanged("ListaKorisnika");
                }
            }
            get { return _listaKorisnika; }

        }
        public ICommand BtnKupiKartu
        {
            set { _btnKupiKartu = value; }
            get { return _btnKupiKartu; }
        }
        public ICommand BtnRezervisiKartu
        {
            set { _btnRezervisiKartu = value; }
            get { return _btnRezervisiKartu; }
        }

        public Korisnik SelectedKorisnik
        {
            set
            {
                if(_selectedKorisnik != value)
                {
                    _selectedKorisnik = value;
                    CijenaVoznje = postaviCijenu();
                    if(_selectedKorisnik != null)
                    {
                        ImeKorisnika = _selectedKorisnik.ime + " " + _selectedKorisnik.prezime;
                        if(Rezervacija)
                            RezervacijaNaIme = _selectedKorisnik.ime + " " + _selectedKorisnik.prezime;
                    }
                    else
                    {
                        ImeKorisnika = "";
                        if (Rezervacija)
                            RezervacijaNaIme = "";
                    }
                    OnPropertyChanged("SelectedKorisnik");
                }
            }
            get { return _selectedKorisnik; }
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
            get { return _cijenaVoznje; }
        }
        public bool Penzioner
           {
               set
               {
                   if(_penzioner != value)
                   {
                       _penzioner = value;
                       if(_penzioner)
                       {
                           RegistriraniKorisnik = false;
                           Student = false;
                       }
                       if(SelectedLinija != null)
                       {
                           CijenaVoznje = postaviCijenu();
                       }
                       OnPropertyChanged("Penzioner");
                   }
               }
               get { return _penzioner; }
           }
        public bool Student
        {
            set
            {
                if (_student != value)
                {
                    _student = value;
                    if (_student)
                    {
                        RegistriraniKorisnik = false;
                        Penzioner = false;
                    }
                    if (SelectedLinija != null)
                    {
                        CijenaVoznje = postaviCijenu();
                    }
                    OnPropertyChanged("Student");
                }
            }
            get { return _student; }
        }
        public bool RegistriraniKorisnik
        {
            set
            {
                if (_registriraniKorisnik != value)
                {
                    _registriraniKorisnik = value;
                    if(_registriraniKorisnik)
                    {
                        Student = false;
                        Penzioner = false;
                        RezervacijaNaImeEnabled = false;
                    }
                    else
                    {
                        SelectedKorisnik = null;
                        if (Rezervacija)
                            RezervacijaNaImeEnabled = true;
                    }
                    OnPropertyChanged("RegistriraniKorisnik");
                }
            }
            get { return _registriraniKorisnik; }
        }
        public string ImeKorisnika
        {
            set
            {
                if(_imeKorisnika != value)
                {
                    _imeKorisnika = value;
                    OnPropertyChanged("ImeKorisnika");
                }
            }
            get { return _imeKorisnika; }
        }
        private void UpdateComboBoxKorisnici()
        {
            ListaKorisnika = BazaFunkcije.dajKorisnike();
        }
        
        private string postaviCijenu()
        {
            if (SelectedLinija == null) return "";
            if (Penzioner)
            {
                cijenaSaPopustom = cijenaVoznjeDoOdredista * 0.8m;
                return cijenaSaPopustom.ToString();
            }
            else if (Student)
            {
                cijenaSaPopustom = cijenaVoznjeDoOdredista * 0.5m;
                return cijenaSaPopustom.ToString();
            }
            else if (RegistriraniKorisnik)
            {
                if (SelectedKorisnik == null)
                {
                    cijenaSaPopustom = cijenaVoznjeDoOdredista;
                    return cijenaSaPopustom.ToString() + "KM";
                }
                
                int brojKarata = 0;
                foreach(Karta z in SelectedKorisnik.karteKorisnika)
                if(z is ProdanaKarta) 
                            brojKarata++;
                if(brojKarata % 10 == 0 && brojKarata != 0) 
                    {
                        cijenaSaPopustom = 0;
                        return "Besplatno";
                    }
                
                else if (SelectedKorisnik.penzioner)
                {
                    cijenaSaPopustom = cijenaVoznjeDoOdredista * 0.8m;
                    return cijenaSaPopustom.ToString() + "KM";
                }
                else if (SelectedKorisnik.student)
                {
                    cijenaSaPopustom = cijenaVoznjeDoOdredista * 0.5m;
                    return cijenaSaPopustom.ToString() + "KM";
                }
                else
                {
                    cijenaSaPopustom = cijenaVoznjeDoOdredista;
                    return cijenaVoznjeDoOdredista.ToString() + "KM";
                }

            }
            else
            {
                cijenaSaPopustom = cijenaVoznjeDoOdredista;
                return cijenaVoznjeDoOdredista.ToString() + "KM";
            }
        }
        private void napraviRezervaciju()
        {
            if (SelectedLinija == null)
            {
                MessageBox.Show("Nijedna linija nije selektovana!");
                return;
            }
            if (SelectedKorisnik != null)
            {
                RezervacijaNaIme = SelectedKorisnik.ime + " " + SelectedKorisnik.prezime;
            }
            if (SelectedKorisnik == null && (RezervacijaNaIme == null || RezervacijaNaIme == ""))
            {
                MessageBox.Show("Morate unijeti ime za koga je rezervacija!");
                return;
            }
            Random broj = new Random(SelectedLinija.brojSlobodnihMjesta);

            RezervisanaKarta novaKarta = new RezervisanaKarta()
            {
                cijenaKarte = cijenaSaPopustom,
                sjediste = broj.Next() % SelectedLinija.brojSlobodnihMjesta,
                odrediste = SelectedOdrediste.nazivGrada,
                vrijemePolaska = SelectedLinija.vrijemePolaska.ToShortTimeString(),
                popust = (cijenaVoznjeDoOdredista - cijenaSaPopustom).ToString() + "KM",
                kartaZaDatum = vratiDanIliDatum().Date.ToShortDateString(),
                naIme = RezervacijaNaIme
            };
            int datumPolaskaID = -1;
            bool daLiPostoji;

            if (PretragaPoDanu)
            {
                daLiPostoji = daLiPostojiDatumPolaskaLinije(SelectedLinija, vratiDatumDana(vratiDan()), ref datumPolaskaID);
            }

            else
                daLiPostoji = daLiPostojiDatumPolaskaLinije(SelectedLinija, DatumPretrage, ref datumPolaskaID);

            BazaFunkcije.spremiKartuZaLinijuIKorisnika(novaKarta, SelectedLinija, SelectedKorisnik, daLiPostoji, datumPolaskaID);
            napuniDataGridLinijama();
            CijenaVoznje = "";
            MessageBox.Show("Karta uspješno rezervisana!");

        }
        private void potvrdiKupovinuKarte()
        {
            if (SelectedLinija == null)
            {
                MessageBox.Show("Nijedna linija nije selektovana!");
                return;
            }
            Random broj = new Random(SelectedLinija.brojSlobodnihMjesta);

            ProdanaKarta novaKarta = new ProdanaKarta()
            {
                cijenaKarte = cijenaSaPopustom,
                sjediste = broj.Next() % SelectedLinija.brojSlobodnihMjesta,
                odrediste = SelectedOdrediste.nazivGrada,
                vrijemePolaska = SelectedLinija.vrijemePolaska.ToShortTimeString(),
                popust = (cijenaVoznjeDoOdredista - cijenaSaPopustom).ToString() + "KM",
                kartaZaDatum = vratiDanIliDatum().Date.ToShortDateString()
            };
            int datumPolaskaID = -1;
            bool daLiPostoji;

            if (PretragaPoDanu)
            {
                daLiPostoji = daLiPostojiDatumPolaskaLinije(SelectedLinija, vratiDatumDana(vratiDan()), ref datumPolaskaID);
            }

            else
                daLiPostoji = daLiPostojiDatumPolaskaLinije(SelectedLinija, DatumPretrage, ref datumPolaskaID);

            BazaFunkcije.spremiKartuZaLinijuIKorisnika(novaKarta, SelectedLinija, SelectedKorisnik, daLiPostoji, datumPolaskaID);
            napuniDataGridLinijama();
            CijenaVoznje = "";
            MessageBox.Show("Sretan put i ugodnu vožnju želi vam ETFTRrans \n --------------------------------------------------- \n"+"Karta broj: "+novaKarta.KartaId.ToString()+"\n\n"+"Polazak: Sarajevo\n\nOdredište: "+novaKarta.odrediste + "\n\nDatum polaska: "+novaKarta.kartaZaDatum + "\n\nVrijeme polaska: "+novaKarta.vrijemePolaska+"\n\nCijena: "+novaKarta.cijenaKarte);
            

        }
        private DateTime vratiDanIliDatum()
        {
            if (PretragaPoDanu)
                return vratiDatumDana(vratiDan()).Date;
            else
                return DatumPretrage.Date;

        }

        private bool daLiPostojiDatumPolaskaLinije(Linija linija, DateTime datum, ref int datPolaskaID)
        {
            foreach (DatumPolaskaLinije x in linija.datumiPolaskaLinije)
            {
                if (x.datumPolaska.Date.ToString() == datum.Date.ToString())
                {
                    datPolaskaID = x.DatumPolaskaLinijeId;
                    return true;
                }
            }
            return false;
        }
        

        #endregion

        #region DodavanjeKorisnika

        private ICommand _btnDodajKorisnika;
        public ICommand BtnDodajKorisnika
        {
            set
            {
                _btnDodajKorisnika = value;
            }
            get { return _btnDodajKorisnika; }
        }

        private void dodajKorisnika()
        {
            DodavanjeKorisnikaView wiev = new DodavanjeKorisnikaView();
            DodavanjeKorisnikaViewModel dijeteViewModel = new DodavanjeKorisnikaViewModel(ListaKorisnika);
            wiev.DataContext = dijeteViewModel;
            wiev.Closed += closeHandler;
            wiev.Show();
        }

        private void closeHandler(object sender, EventArgs e)
        {
            UpdateComboBoxKorisnici();
        }
        #endregion

        #region NaplataRezervacija
        private bool _rezervacijeTabSelected;
        private List<Korisnik> _korisniciZaRezervaciju;
        private List<RezervisanaKarta> _karte;
        private List<RezervisanaKarta> _karteZaDataGrid;
        private RezervisanaKarta _naIme;
        private DateTime _naDatum = DateTime.Today.Date;
        private Korisnik _userID;
        private DateTime _korisnikNaDatum = DateTime.Today.Date;
        private Karta _selectedKarta;
        private ICommand _btnNaplatiKartu;
        private string UserName;

        public ICommand BtnNaplatiKartu
        {
            set
            {
                _btnNaplatiKartu = value;
            }
            get { return _btnNaplatiKartu; }
        }
        public Karta SelectedKarta
        {
            set
            {
                if (_selectedKarta != value)
                {
                    _selectedKarta = value;
                    OnPropertyChanged("SelectedKarta");
                }
            }
            get { return _selectedKarta; }
        }
        public DateTime NaDatum
        {
            set
            {
                if(_naDatum != value)
                {
                    _naDatum = value;
                    if (UserID != null || NaIme != null)
                    {
                        napuniDataGridKartama();
                    }
                    OnPropertyChanged("NaDatum");
                }
            }
            get { return _naDatum; }
        }
        public DateTime KorisnikNaDatum
        {
            set
            {
                if (_korisnikNaDatum != value)
                {
                    _korisnikNaDatum = value;
                    if (UserID != null || NaIme != null)
                    {
                        napuniDataGridKartama();
                    }
                    OnPropertyChanged("KorisnikNaDatum");
                }
            }
            get { return _korisnikNaDatum; }
        }
        public RezervisanaKarta NaIme
        {
            set
            {
                if(_naIme != value)
                {
                    _naIme = value;
                    if (_naIme != null)
                    {
                        UserID = null;
                        napuniDataGridKartama();
                    }
                    else if(UserID == null)
                    {
                        SelectedLinija = null;
                        ListaKartiZaDataGrid = null;
                    }
                    OnPropertyChanged("NaIme");
                }
            }
            get { return _naIme; }
        }
        public Korisnik UserID
        {
            set
            {
                if (_userID != value)
                {
                    _userID = value;
                    if (_userID != null)
                    {
                        NaIme = null;
                        napuniDataGridKartama();
                    }
                    else if (NaIme == null)
                    {
                        SelectedLinija = null;
                        ListaKartiZaDataGrid = null;
                    }
                    OnPropertyChanged("UserID");
                }
            }
            get { return _userID; }
        }
        public List<Korisnik> ListaKorisnikaZaRezervaciju
        {
            set
            {
                if(_korisniciZaRezervaciju != value)
                {
                    _korisniciZaRezervaciju = value;
                    OnPropertyChanged("ListaKorisnikaZaRezervaciju");
                }
            }
            get { return _korisniciZaRezervaciju; }
        }
        public List<RezervisanaKarta> ListaKarti
        {
            set
            {
                if(_karte != value)
                {
                    _karte = value;
                    OnPropertyChanged("ListaKarti");
                }
            }
            get { return _karte; }
        }
        public List<RezervisanaKarta> ListaKartiZaDataGrid
        {
            set
            {
                if (_karteZaDataGrid != value)
                {
                    _karteZaDataGrid = value;
                    OnPropertyChanged("ListaKartiZaDataGrid");
                }
            }
            get { return _karteZaDataGrid; }
        }

        private void napuniDataGridKartama()
        {
            if(NaIme == null)
            {
                List<RezervisanaKarta> nova = new List<RezervisanaKarta>();
                foreach(RezervisanaKarta x in ListaKarti )
                {

                        if (UserID.userId == x.korisnik.userId && KorisnikNaDatum.Date.ToShortDateString() == x.kartaZaDatum)
                        nova.Add(x);
                }
                ListaKartiZaDataGrid = nova;
            }
            else
            {
                List<RezervisanaKarta> nova = new List<RezervisanaKarta>();
                foreach (RezervisanaKarta x in ListaKarti)
                {
                    if (NaIme.naIme == x.naIme && NaDatum.Date.ToShortDateString() == x.kartaZaDatum)
                        nova.Add(x);
                }
                ListaKartiZaDataGrid = nova;
            }


        }
        public bool RezervacijeTabSelected
        {
            set
            {
                if(_rezervacijeTabSelected != value)
                {
                    _rezervacijeTabSelected = value;
                    if (_rezervacijeTabSelected)
                    {
                        napuniKorisnikeZaRezervaciju();
                        napuniNaImeZaRezervaciju();
                    }
                    OnPropertyChanged("RezervacijeTabSelected");
                }
            }
            get { return _rezervacijeTabSelected; }
        }

        private void napuniNaImeZaRezervaciju()
        {
            List<RezervisanaKarta> nova = BazaFunkcije.dajRezervisaneKarte();

            foreach (RezervisanaKarta x in nova)
            {
                x.korisnik = null;
                foreach (Korisnik y in ListaKorisnikaZaRezervaciju)
                    foreach (Karta z in y.karteKorisnika)
                    {
                        if (z is RezervisanaKarta && z.KartaId == x.KartaId)
                        {
                            x.korisnik = y;
                            break;
                        }
                    }
            }
            ListaKarti = nova;

        }

        private void napuniKorisnikeZaRezervaciju()
        {
            ListaKorisnikaZaRezervaciju = BazaFunkcije.dajKorisnike();
        }
        private void naplatiRezevisanuKartu()
        {
            if (SelectedKarta == null)
            {
                MessageBox.Show("Nijedna karta nije selektovana!");
                return;
            }

            ProdanaKarta novaKarta = new ProdanaKarta()
            {
                cijenaKarte = SelectedKarta.cijenaKarte,
                kartaZaDatum = SelectedKarta.kartaZaDatum,
                odrediste = SelectedKarta.odrediste,
                popust = SelectedKarta.popust,
                sjediste = SelectedKarta.sjediste,
                vrijemePolaska = SelectedKarta.vrijemePolaska
            };
            BazaFunkcije.naplatiRezervisanuKartu(novaKarta, SelectedKarta);
            napuniKorisnikeZaRezervaciju();
            napuniNaImeZaRezervaciju();
            napuniDataGridKartama();
            MessageBox.Show("Karta uspjesno naplacena!");

        }
        #endregion

        public ProdajaKarataViewModel()
        {
            napuniComboBoxOdredistima();
            Rezervacija = false;
            BtnPotvrdiKupovinuEnabled = true;
            PretragaPoDanu = true;
            postaviDanPretrage();
            UpdateComboBoxKorisnici();
            BtnKupiKartu = new RelayCommand(potvrdiKupovinuKarte);
            BtnRezervisiKartu = new RelayCommand(napraviRezervaciju);
            BtnDodajKorisnika = new RelayCommand(dodajKorisnika);
            BtnNaplatiKartu = new RelayCommand(naplatiRezevisanuKartu);

        }

        public ProdajaKarataViewModel(string UserName, Window view)
        {
            prijavljeniUser = UserName;
            window = view;
            window.Closing += closeHandler1;
            napuniComboBoxOdredistima();
            Rezervacija = false;
            BtnPotvrdiKupovinuEnabled = true;
            PretragaPoDanu = true;
            postaviDanPretrage();
            UpdateComboBoxKorisnici();
            BtnKupiKartu = new RelayCommand(potvrdiKupovinuKarte);
            BtnRezervisiKartu = new RelayCommand(napraviRezervaciju);
            BtnDodajKorisnika = new RelayCommand(dodajKorisnika);
            BtnNaplatiKartu = new RelayCommand(naplatiRezevisanuKartu);

        }

        


    }
}
