using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETFTrans.Model
{
    public class Vozac : Uposlenik
    {
        public virtual List<Autobus> voziAutobuse { get; set; }
       
    }
}
