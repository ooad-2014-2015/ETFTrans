using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETFTrans.Model
{
    public abstract class Karta
    {
        [Key]
        public int KartaId { get; set; }
        public string kartaZaDatum { get; set; }
        public string odrediste { get; set; }
        public string vrijemePolaska { get; set; }
        public string popust { get; set; }
        public decimal cijenaKarte {get; set;}
        public int sjediste { get; set; }

        public virtual Linija kartaZaLiniju { get; set; }
        public virtual Korisnik korisnik { get; set; }
    }
}
