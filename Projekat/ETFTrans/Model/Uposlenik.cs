using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace ETFTrans.Model
{
    public abstract class Uposlenik
    { 
        [Key]
        public int UposlenikId { get; set; }
        public string ime { get; set; }
        public string prezime { get; set; }
        public decimal plata { get; set; }
        public string jmbg { get; set; }

        public string userName { get; set; }
        public string password { get; set; }
        public DateTime datumRodenja { get; set; }
        public DateTime datumZaposlenja { get; set; }
        public DateTime ugovorDo { get; set; }
       

        public virtual List<Log> logoviUposlenika { get; set; }

        public abstract bool ValidirajLogIn(string userName, string password);

    }
}
