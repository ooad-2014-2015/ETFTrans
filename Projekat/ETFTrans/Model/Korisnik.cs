using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETFTrans.Model
{
    public class Korisnik
    {
        public int KorisnikId {get; set;}
        public string ime { get; set; }
        public string prezime { get; set; }
        public string jmbg { get; set; }
        public string userId { get; set; }
        public bool penzioner { get; set; }
        public bool student { get; set; }
        public virtual List<Karta> karteKorisnika { get; set; }
    }
}
