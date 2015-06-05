using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETFTrans.Model
{
    public class DatumPolaskaLinije
    {
        public int DatumPolaskaLinijeId { get; set; }
        public DateTime datumPolaska { get; set; }
        public int brojSlobodnihMjesta { get; set; }
        public DatumPolaskaLinije()
        {
        }
        public virtual Linija datumPolaskaZaLiniju { get; set; }
    }
}
