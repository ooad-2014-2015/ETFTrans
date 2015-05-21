using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETFTrans.Model
{
    public class Vozac : Uposlenik
    {
        //public virtual int UposlenikId { get; set; }
        public virtual List<Autobus> voziAutobuse { get; set; }

        public override bool ValidirajLogIn(string userName, string password)
        {
            return false;
        }
    }
}
