using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETFTrans.DataAcces;

namespace ETFTrans.Model
{
    public class RadnikNaSalteruPretraga : Uposlenik
    {
        // public int UposlenikId { get; set; }

        public override bool ValidirajLogIn(string userName, string password)
        {
            return BazaFunkcije.ValidirajLogInRadnikPretraga(userName, password);
        }
    }
}
