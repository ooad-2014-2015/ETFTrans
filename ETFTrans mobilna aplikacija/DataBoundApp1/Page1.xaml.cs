using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace DataBoundApp1
{
    public partial class Page1 : PhoneApplicationPage
    {
        public Page1()
        {
            InitializeComponent();

            using (bazaContext db = new bazaContext(bazaContext.ConnectionString))
            {
                db.CreateIfNotExists();
                List<Stanica> list = db.Stanica.ToList();
                List<string> l = new List<string>();
                string x = string.Empty;
                foreach (Stanica s in list)
                {
                    l.Add(s.ImeStanice);
                }

                l.Sort();
                l.Insert(0, "Sve linije");

                odredistaLongList.ItemsSource = l;
            }
        }

        private void odredistaLongList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            var myItem = ((LongListSelector)sender).SelectedItem as string;

            odrediste.DugmeOdrediste = "Odrediste: " + myItem;
            odrediste.ImeOdredista = myItem;
            NavigationService.GoBack();
        }
    }
}