﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ETFTrans.DataAcces;
using ETFTrans.Model;

namespace ETFTrans
{
    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
            try
            {
                using (ETFTransBaza db = new ETFTransBaza())
                {
                  /*
                    RadnikNaSalteruProdaja noviClan1 = new RadnikNaSalteruProdaja() { userName = "admin", password = "admin", ime = "Ragib", prezime = "Smajic", datumRodenja = DateTime.Now, datumZaposlenja = DateTime.Now, ugovorDo = DateTime.Now };     
                    db.uposlenici.Add(noviClan1);
                    db.SaveChanges();
                   /* DatumPolaskaLinije noviDatum = new DatumPolaskaLinije() { brojSlobodnihMjesta = 32, datumPolaska = DateTime.Today.Date.AddMonths(1).AddDays(-3) };
                    db.Linije.First(i => i.LinijaID == 1).datumiPolaskaLinije.Add(noviDatum);
                    db.SaveChanges();*/
                    ClanUprave noviClan = new ClanUprave() { userName = "admin", password = "admin", ime = "Ragib", prezime = "Smajic", datumRodenja = DateTime.Now, datumZaposlenja = DateTime.Now, ugovorDo = DateTime.Now };
                    db.uposlenici.Add(noviClan);
                    db.SaveChanges();
                   
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            
        }

     

    }
}
