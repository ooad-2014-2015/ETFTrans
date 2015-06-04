using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETFTrans
{
    public class tipPretrage 
    {
        public tipPretrage(string polaziste, string odrediste, decimal cijena, DateTime vrijemePolasaka, DateTime vrijemeDolaska, 
            List<string> listaDanaVoznje, List<string> listaStanica)
        {
            this.Polaziste = polaziste; this.Odrediste = odrediste; this.Cijena = cijena; this.VrijemeDolaska = vrijemeDolaska;
            this.VrijemePolaska = vrijemePolasaka; this.ListaDanaVoznje = listaDanaVoznje; this.ListaStanica = listaStanica;
 
        }
        public List<string> ListaDanaVoznje { get; set; }
        private List<string> ListaStanica { get; set; }
        public string Polaziste { get; set; }
        public string Odrediste { get; set; }
        public decimal Cijena { get; set; }
        public DateTime VrijemePolaska { get; set; }
        public DateTime VrijemeDolaska { get; set; }

        public override string ToString()
        {
            string s = this.Polaziste + this.Odrediste;
            return s;
        }


    }
}
