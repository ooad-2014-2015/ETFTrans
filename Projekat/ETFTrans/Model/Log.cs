using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETFTrans.Model
{
    public class Log
    {
        public int LogId { get; set; }
        public string vrijeme { get; set; }
        public string tip { get; set; }
        public DateTime datum { get; set; }

        public virtual Uposlenik uposlenik { get; set; }


    }
}
