using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using DataBoundApp1.Resources;
using DataBoundApp1.ViewModels;

namespace DataBoundApp1
{
    public partial class MainPage : PhoneApplicationPage
    {

        List<ispis> listaIspis = new List<ispis>();
        List<ispis> listaSvihLinija = new List<ispis>();

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            dugmeOdrediste.Content = odrediste.DugmeOdrediste;

            using (bazaContext db = new bazaContext(bazaContext.ConnectionString))
            {
                db.CreateIfNotExists();
        
                db.LogDebug = true;
                List<Stanica> listaStanica = db.Stanica.ToList();
                List<Linije> listaLinija = db.Linije.ToList();
                List<StanicaLinija> listaVezeStanicaLinija = db.StanicaLinija.ToList();
                List<DaniVoznjeLinije> listaDanaVoznjeLinije = db.DaniVoznjeLinije.ToList();

                foreach (Linije linija in listaLinija)
                {
                    ispis i = new ispis();
                    i.Odrediste = linija.Odrediste;
                    i.Cijena = linija.CijenaZaOdrediste.ToString() + "KM";
                    string minute = linija.VrijemeDolaska.Minute.ToString();
                    if (minute == "0") minute += "0";
                    string vrijemeDolaska = linija.VrijemeDolaska.Hour.ToString() + ":" + minute;

                    minute = linija.VrijemePolaska.Minute.ToString();
                    if (minute == "0") minute += "0";
                    i.VrijemePolaskaiDolaska = linija.VrijemePolaska.Hour.ToString() + ":" + minute + " - " + vrijemeDolaska;

                    

                    foreach (StanicaLinija sl in listaVezeStanicaLinija)
                    {
                        
                        if (sl.IdLinija == linija.Id)
                        {
                            foreach (Stanica stanice in listaStanica)
                            {
                                if (sl.IdStanica == stanice.Id)
                                {
                                    i.listaGradovaList.Add(stanice.ImeStanice);
                                    i.listaGradova = stanice.ImeStanice;
                                }
                            }
                        }
                       
                    }

                    foreach (DaniVoznjeLinije ldv in listaDanaVoznjeLinije)
                    {
                        if (ldv.IdLinije == linija.Id)
                        {
                            if (ldv.Pon == 1) i.Dani = "Pon";
                            if (ldv.Uto == 1) i.Dani = "Uto";
                            if (ldv.Sri == 1) i.Dani = "Sri";
                            if (ldv.Cet == 1) i.Dani = "Cet";
                            if (ldv.Pet == 1) i.Dani = "Pet";
                            if (ldv.Sub == 1) i.Dani = "Sub";
                            if (ldv.Ned == 1) i.Dani = "Ned";


                        }
                    }
                    listaSvihLinija.Add(i);

                    

 
                }
            }
            
          
             
        }

        public bool daLiJeOdredisteUListi(List<string> l)
        {
            string odr = odrediste.ImeOdredista;

            foreach (string s in l)
            {
                if (s == odr) return true;
            }
            return false;
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            dugmeOdrediste.Content = odrediste.DugmeOdrediste;
            string odr = odrediste.ImeOdredista;
            MainLongListSelector.ItemsSource = listaSvihLinija;
            listaIspis.Clear();

            string zaBox = string.Empty;
            if (odr == "Sve linije") return;
            foreach (ispis i in listaSvihLinija)
            {

                if (odr == i.odredisteZaPretragu || daLiJeOdredisteUListi(i.listaGradovaList))
                {
                    listaIspis.Add(i);
                }
 
            }

           
            if (listaIspis.Count == 0)
            {
                ispis zaNeuspjeluPretragu = new ispis("Nista nije pronadjeno");
                zaNeuspjeluPretragu.Dani = "";
                zaNeuspjeluPretragu.listaGradova = "";
                listaIspis.Add(zaNeuspjeluPretragu);
            }
            MainLongListSelector.ItemsSource = listaIspis;
             

            
            
                
                
            
        }

        // Handle selection changed on LongListSelector
        private void MainLongListSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // If selected item is null (no selection) do nothing
            /*if (MainLongListSelector.SelectedItem == null)
                return;

            // Navigate to the new page
            NavigationService.Navigate(new Uri("/DetailsPage.xaml?selectedItem=" + (MainLongListSelector.SelectedItem as ItemViewModel).ID, UriKind.Relative));

            // Reset selected item to null (no selection)
            MainLongListSelector.SelectedItem = null;*/
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Page1.xaml", UriKind.Relative));
        }

        /*void NavigationService_LoadCompleted(object sender, NavigationEventArgs e)
        {
            using (bazaContext db = new bazaContext(bazaContext.ConnectionString))
            {
                db.CreateIfNotExists();
                db.LogDebug = true;


                List<BazaTest> lista = db.BazaTest.ToList();

                foreach (BazaTest bt in lista)
                {

                    listaIspis.Add(new ispis(bt.Ime, "gfdgd"));

                }

                MainLongListSelector.ItemsSource = listaIspis;
            }
        }*/

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}