using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETFTrans.Model
{
    public class Autobus
    {
        public int AutobusID { get; set; }
        public string marka { get; set; }
        public DateTime datumProizvodnje { get; set; }
        public DateTime datumRegistracije { get; set; }

        public string registracija { get; set; }

        public int brojRaspolozivihMjesta { get; set; }
        public virtual Linija linija { get; set; }
        public virtual Vozac vozac { get; set; }

    }
}
