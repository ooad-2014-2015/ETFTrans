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
    public class DodavanjeKorisnikaViewModel : BaseViewModel
    {
        private string _ime;
        private string _prezime;
        private string _jmbg;
        private string _userId;
        private ICommand _btnUnesiKorisnika;
        private List<Korisnik> _korisnici;
        private bool _penzioner;
        private bool _student;
        public bool Penzioner
        {
            set
            {
                if(_penzioner != value)
                {
                    _penzioner = value;
                    if (_penzioner)
                        Student = false;
                    OnPropertyChanged("Penzioner");
                }
            }
            get { return _penzioner; }
        }
        public bool Student
        {
            set
            {
                if(_student != value)
                {
                    _student = value;
                    if (_student)
                        Penzioner = false;
                    OnPropertyChanged("Student");
                }
            }
            get { return _student; }
        }
        
        public string Ime
        {
            set
            {
                _ime = value;
                OnPropertyChanged("Ime");
            }
            get { return _ime; }
        }
        public string Prezime
        {
            set
            {
                _prezime = value;
                OnPropertyChanged("Prezime");
            }
            get { return _prezime; }
        }
        public string JMBG
        {
            set
            {
                _jmbg = value;
                OnPropertyChanged("JMBG");
            }
            get { return _jmbg; }
        }
        public string UserID
        {
            set
            {
                _userId = value;
                OnPropertyChanged("UserID");
            }
            get { return _userId; }
        }
        public ICommand BtnUnesiKorisnika
        {
            set
            {
                _btnUnesiKorisnika = value;
            }
            get { return _btnUnesiKorisnika; }
        }
        public DodavanjeKorisnikaViewModel(List<Korisnik> korisnik)
        {
            _korisnici = korisnik;
            BtnUnesiKorisnika = new RelayCommand(registrujKorisnika);
        }
        private bool validirajPodatke()
        {
            
            if (Ime == null || Ime == "")
            {
                MessageBox.Show("Ime ne moze biti prazno!");
                return false;
            }
            if (Prezime == null || Prezime == "")
            {
                MessageBox.Show("Prezime ne moze biti prazno!");
                return false;
            }
            if (JMBG == null || JMBG == "")
            {
                MessageBox.Show("Polje JMBG ne moze biti prazno!");
                return false;
            }
            if(JMBG.Length != 13)
            {
                MessageBox.Show("Polje JMBG mora imati duzinu 13!");
                return false;
            }
            foreach(Korisnik k in _korisnici)
            {
                if(k.userId == UserID)
                {
                    MessageBox.Show("UserID vec zauzet!");
                    return false;
                }
            }
            return true;
            
            
        }
        private void registrujKorisnika()
        {
            if (!validirajPodatke()) return;
            Korisnik korisnik = new Korisnik()
            {
                 ime = Ime,
                 prezime = Prezime,
                  jmbg = JMBG,
                   userId = UserID,
                   penzioner = Penzioner,
                   student = Student
            };

            BazaFunkcije.upisiKorisnikaUBazu(korisnik);
            MessageBox.Show("Korisnik uspjesno dodan!");
        }
    }
}
