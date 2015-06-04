using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

using vm = ETFTrans;

namespace ETFTrans
{
    public partial class MainPage : PhoneApplicationPage
    {
        public string stanica;
        List<string> l {get; set; }

        ServiceReference1.IservisClient klijent;
        
        

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            this.l = new List<string>(new string[] { "Dino", "Enis" });
            klijent = new ServiceReference1.IservisClient();
            this.DataContext = zajednickiModel.Model;

            klijent.pretraziCompleted += klijent_pretraziCompleted;
        }

        void klijent_pretraziCompleted(object sender, ServiceReference1.pretraziCompletedEventArgs e)
        {
            try
            {
                var rez = e.Result;  
            }
            catch (Exception ex)
            {
                tb1.Text = ex.Message;
            }
        }

        private void traziDugme_Click(object sender, RoutedEventArgs e)
        {
            klijent.pretraziAsync("Sarajevo", "Bihac");

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/polaziste.xaml", UriKind.Relative));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/odrediste.xaml", UriKind.Relative));
        }

        private void gfd(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {

        }

        private void llsStanica_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void testPretrage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/ispisPretrage.xaml", UriKind.Relative));
        }

        

    }
}