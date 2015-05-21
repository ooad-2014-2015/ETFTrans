using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETFTrans.Model
{
    public class Stanica
    {
        public int StanicaId { get; set; }
        public string nazivGrada { get; set; }
        public decimal cijenaVoznje {get; set;}

        public virtual List<Linija> stanicaZaLinije { get; set; }
    }
}
