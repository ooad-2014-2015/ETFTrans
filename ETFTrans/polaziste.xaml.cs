using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace ETFTrans
{
    public partial class polaziste : PhoneApplicationPage
    {
        void sortiraj(List<string> l) { l.Sort(); }

        public polaziste()
        {
            InitializeComponent();
            this.DataContext = zajednickiModel.Model;
            List<string> listaStanica = new List<string>(new string[] 
                {"Sarajevo", "Banja Luka" , "Tuzla",	"Zenica", "Bihac", "Brcko", "Bijeljina", "Prijedor", "Trebinje", "Travnik",	"Kiseljak",	"Doboj",
                "Cazin", "Bugojno",	"Velika Kladusa", "Visoko",	"Gorazde",	"Konjic",	"Gračanica", "Gradačac", "Bosanska Krupa", "Mrkonjic Grad",
                "Foca",	"Zavidovici", "Zivinice", "Sanski Most", "Bosanska Gradiska", "Bileca", "Lukavac","Kakanj",	"Livno", "Odzak", "Sipovo",
                "Prozor", "Novi Travnik" });
            sortiraj(listaStanica);
            listaStanicaPolaziste.ItemsSource = listaStanica;

        }

        private void listaStanicaPolaziste_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LongListSelector lls = (LongListSelector)sender;
            var llsItem = lls.SelectedItem;
            string polaziste = llsItem.ToString();
            zajednickiModel.Model.Polaziste = polaziste;
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));

        }
    }
}