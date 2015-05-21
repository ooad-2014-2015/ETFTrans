using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETFTrans.Model
{
    public class dolazakOdlazakAutobusa
    {
        public int dolazakOdlazakAutobusaId { get; set; }
        public DateTime datumOtpreme { get; set; }
        public virtual Otpremnik otpremio { get; set; }
        public virtual Autobus autobus { get; set; }

        public string stigaoOtisao { get; set; }
    }
}
