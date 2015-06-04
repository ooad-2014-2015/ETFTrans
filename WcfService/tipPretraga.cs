using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfService
{
    public class tipPretraga
    {
        private List<string> listaDanaVoznje;

        public List<string> ListaDanaVoznje1
        {
          get { return listaDanaVoznje; }
          set { listaDanaVoznje = value; }
}
        private List<string> listaStanica;

        public List<string> ListaStanica1
        {
          get { return listaStanica; }
          set { listaStanica = value; }
        }

        public void ListaDanaVoznje(string s)
        {
            listaDanaVoznje.Add(s);
        }

        public void ListaStanica(string s)
        {
            listaStanica.Add(s);
        }

        public string Polaziste { get; set; }
        public string Odrediste { get; set; }
        public decimal Cijena { get; set; }
        public DateTime VrijemePolaska { get; set; }
        public DateTime VrijemeDolaska { get; set; }
        

    }
}
