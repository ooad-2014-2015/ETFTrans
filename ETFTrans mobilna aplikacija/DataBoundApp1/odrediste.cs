using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBoundApp1
{
    public class odrediste
    {
        static string dugmeOdrediste = "Odaberi odrediste";

        public static string DugmeOdrediste
        {
            get { return odrediste.dugmeOdrediste; }
            set { odrediste.dugmeOdrediste = value; }
        }

        static string imeOdredista = "Sve linije";

        public static string ImeOdredista
        {
            get { return odrediste.imeOdredista; }
            set { odrediste.imeOdredista = value; }
        }
    }
}
