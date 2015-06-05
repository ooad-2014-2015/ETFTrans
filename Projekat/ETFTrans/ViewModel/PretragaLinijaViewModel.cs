using ETFTrans.DataAcces;
using ETFTrans.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETFTrans.ViewModel
{
    public class PretragaLinijaViewModel :BaseViewModel
    {
        private decimal cijenaVoznjeDoOdredista;
        private List<Stanica> _svaOdredista;
        private List<Linija> _linijeZaOdrediste;
        private bool _pretragaPoDatumu;
        private bool _pretragaPoDanu;
        private Stanica _selectedOdrediste;
        private DateTime _datumPretrage = DateTime.Today;
        private bool _ponedjeljak;
        private bool _utorak;
        private bool _srijeda;
        private bool _cetvrtak;
        private bool _petak;
        private bool _subota;
        private bool _nedjelja;
        public PretragaLinijaViewModel()
        {
            napuniComboBoxOdredistima();
            PretragaPoDanu = true;
            postaviDanPretrage();
        }
        public PretragaLinijaViewModel(string UserName, View.PretragaLinijaView view)
        {
            prijavljeniUser = UserName;
            window = view;
            window.Closing += closeHandler1;
            napuniComboBoxOdredistima();    
            PretragaPoDanu = true;
            postaviDanPretrage();
        }
        public List<Stanica> ListaOdredista
        {
            set
            {
                if (_svaOdredista != value)
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
                if (_linijeZaOdrediste != value)
                {
                    _linijeZaOdrediste = value;
                    OnPropertyChanged("LinijeOdredista");
                }
            }
            get { return _linijeZaOdrediste; }
        }
        public bool PretragaPoDatumu
        {
            set
            {
                if (_pretragaPoDatumu != value)
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
                if (_selectedOdrediste != value)
                {
                    _selectedOdrediste = value;
                    if (_selectedOdrediste != null)
                    {
                        cijenaVoznjeDoOdredista = _selectedOdrediste.cijenaVoznje;
                        napuniDataGridLinijama();
                    }
                    else
                    {
                    
                        LinijeOdredista = null;
                    }
                    OnPropertyChanged("SelectedOdrediste");
                }
            }
            get { return _selectedOdrediste; }
        }
        public DateTime DatumPretrage
        {
            set
            {
                if (_datumPretrage != value)
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
                if (_ponedjeljak != value)
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

        
    }
}
