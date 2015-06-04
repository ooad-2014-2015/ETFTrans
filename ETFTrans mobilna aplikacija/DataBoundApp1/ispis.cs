using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataBoundApp1
{
    class ispis
    {
        private string odrediste;

        public string Odrediste
        {
            get { return odrediste; }
            set
            {
                this.odredisteZaPretragu = value;
                odrediste += value;
            }
        }

        public string odredisteZaPretragu { get; set; }

        

        private string dani = "Linija nije aktivna";

        public string Dani
        {
            get { return dani; }
            set {
                if (dani == "Linija nije aktivna")
                    dani = string.Empty;
                if (value != "Pon" && value != "")
                    value = ", " + value;
                dani += value; }
        }

        private string _listaGradova = "Nema međulinija";

        public string listaGradova 
        {
            get { return _listaGradova; }
            set {
                if (_listaGradova == "Nema međulinija")
                    _listaGradova = string.Empty;
                
                if (listaGradova != string.Empty)
                    value = ", " + value;
       
                    
                _listaGradova += value; }
        }

        public List<string> listaGradovaList = new List<string>();

        public string Cijena { get; set; }

        public string VrijemePolaskaiDolaska { get; set; }

        

        public ispis(string odr = "Sarajevo - ")
        {
            this.Odrediste = odr;
        }

         

        
    
    }
}
