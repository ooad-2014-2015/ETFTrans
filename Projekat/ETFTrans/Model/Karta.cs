using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETFTrans.Model
{
    public class Karta
    {
        public int KartaId { get; set; }
        public int cijenaKarte {get; set;}
        public int sjediste { get; set; }

        public virtual Linija kartaZaLiniju { get; set; }
    }
}
