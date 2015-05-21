using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETFTrans.Model
{
    public class Linija
    {
        public int LinijaID { get; set; }
        public DateTime vrijemePolaska { get; set; }
        public DateTime vrijemeDolaska { get; set; }

        public string odredisteLinije { get; set; }
        public decimal cijenaZaGlavnoOdrediste { get; set; }
        public virtual DaniVoznjeLinije daniVoznje {get; set; } 

        public int brojSlobodnihMjesta { get; set; }
        public int brojRaspolozivihMjesta {get; set;}

        public string tipLinije { get; set; }

        public virtual List<Stanica> staniceLinije { get; set; }
        public virtual List<Autobus> autobusiNaLiniji { get; set; }

    }
}
