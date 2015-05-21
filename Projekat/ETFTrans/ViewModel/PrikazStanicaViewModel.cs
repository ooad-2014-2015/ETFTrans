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
    public class PrikazStanicaViewModel : BaseViewModel
    {
        
        private List<Stanica> _listaStanica;
        private List<Stanica> _sveStaniceIzBaze;
        private Stanica _selectedStanica;
        private Stanica _selectedStanicaIzBaze;
        public List<Stanica> listaIzbrisanihStanica;
        public List<Stanica> listaDodanihStanica;
        private ICommand _btnIzbrisiStanicuSaLinije;
        private ICommand _btnDodajStanicuNaLiniju;
        public ICommand BtnIzbrisiStanicuSaLinije
        {
            set
            {
                if (_btnIzbrisiStanicuSaLinije != value)
                    _btnIzbrisiStanicuSaLinije = value;
            }
            get
            {
                return _btnIzbrisiStanicuSaLinije;
            }
        }
        public ICommand BtnDodajStanicuNaLiniju
        {
            set
            {
                if (_btnDodajStanicuNaLiniju != value)
                    _btnDodajStanicuNaLiniju = value;
            }
            get
            {
                return _btnDodajStanicuNaLiniju;
            }
        }
        public List<Stanica> ListaStanica
        {
            set
            {
                if(_listaStanica != value)
                {
                    _listaStanica = value;
                    OnPropertyChanged("ListaStanica");
                }
            }
            get
            {
                return _listaStanica;
            }
        }
        public List<Stanica> StaniceIzBaze
        {
            set
            {
                if (_sveStaniceIzBaze != value)
                    _sveStaniceIzBaze = value;
                OnPropertyChanged("staniceIzBaze");
            }
            get
            {
                return _sveStaniceIzBaze;
            }
        }
        public Stanica SelectedStanica
        {
            set
            {
                if(_selectedStanica != value)
                {
                    _selectedStanica = value;
                    OnPropertyChanged("SelectedStanica");
                }
            }
            get { return _selectedStanica; }
        }
        public Stanica SelectedStanicaIzBaze
        {
            set
            {
                if (_selectedStanicaIzBaze != value)
                {
                    _selectedStanicaIzBaze = value;
                    OnPropertyChanged("SelectedStanicaIzBaze");
                }
            }
            get { return _selectedStanicaIzBaze; }
        }
        public PrikazStanicaViewModel(List<Stanica> stanice)
        {
            ListaStanica = stanice;
            StaniceIzBaze = BazaFunkcije.dajStanice();
            BtnIzbrisiStanicuSaLinije = new RelayCommand(izbrisiStanicuSaLinije);
            BtnDodajStanicuNaLiniju = new RelayCommand(dodajStanicuNaLiniju);
            listaIzbrisanihStanica = new List<Stanica>();
            listaDodanihStanica = new List<Stanica>();
        }

        private void dodajStanicuNaLiniju()
        {
            if(SelectedStanicaIzBaze == null)
            {
                MessageBox.Show("Nijedna stanica nije selektovana!");
                return;
            }
                foreach(Stanica s in ListaStanica)
                if(s.StanicaId == SelectedStanicaIzBaze.StanicaId)
                {
                    MessageBox.Show("Nije moguce: Stanica vec postoji na liniji!");
                    return;
                }
                listaDodanihStanica.Add(SelectedStanicaIzBaze);
                ListaStanica.Add(SelectedStanicaIzBaze);
                for (int i = 0; i < listaIzbrisanihStanica.Count; i++)
                {
                    if (SelectedStanicaIzBaze.StanicaId == listaIzbrisanihStanica[i].StanicaId)
                        listaIzbrisanihStanica.RemoveAt(i);
                }
                updateStaniceDataGrid();
                MessageBox.Show("Stanica uspjesno dodana na liniju!");

        }

        private void izbrisiStanicuSaLinije()
        {
            if(SelectedStanica == null )
            {
                MessageBox.Show("Nijedna stanica nije selektovana!");
                return;
            }
            MessageBoxResult dr = MessageBox.Show("Da li ste sigurni?", "Oprez!", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if(dr == MessageBoxResult.Yes)
            {
                if(ListaStanica.Count == 1)
                {
                    MessageBox.Show("Nije moguce: Linija ne moze biti bez ijedne stanice!");
                    return;
                }
                ListaStanica.Remove(SelectedStanica);
                listaIzbrisanihStanica.Add(SelectedStanica);
                for (int i = 0; i < listaDodanihStanica.Count; i++ )
                {
                    if (SelectedStanica.StanicaId == listaDodanihStanica[i].StanicaId)
                        listaDodanihStanica.RemoveAt(i);
                }
                    updateStaniceDataGrid();
                
            }
        }
        private void updateStaniceDataGrid()
        {
            List<Stanica> novaLista = new List<Stanica>();
            foreach(Stanica s in ListaStanica)
            {
                novaLista.Add(s);
            }
            ListaStanica.Clear();
            ListaStanica = novaLista;
        }
    }
}
