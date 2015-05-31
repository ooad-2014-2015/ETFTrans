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
        public string nazivKompanije { get; set; }
        public string datumOtpreme { get; set; }
        public string registracijaAutobusa { get; set; }
        public string vrijemeOtpreme { get; set; }
        public virtual Otpremnik otpremio { get; set; }
        public virtual Autobus autobus { get; set; }
        public virtual Linija linija { get; set; }

        public string stigaoOtisao { get; set; }
    }
}
