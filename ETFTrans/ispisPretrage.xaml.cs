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
    public partial class ispisPretrage : PhoneApplicationPage
    {
        

        public ispisPretrage()
        {
            List<tipPretrage> l = new List<tipPretrage>();
            List<string> listaDana =  new List<string>(new string[]{"Pon", "Uto", "Sri"});
            List<string> listaLinija =  new List<string>(new string []{"Banja Luka", "Gradacac"});
            l.Add(new tipPretrage("Sarajevo", "Zenica", 31, new DateTime(1994, 5, 31), new DateTime(2008,5,5), listaDana,
                listaLinija));

            //pretragaIspis.ItemsSource = l;
            
        }

        
    }

   
}