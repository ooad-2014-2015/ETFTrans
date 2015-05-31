using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETFTrans.Model
{
    public class DaniVoznjeLinije
    {
        public int DaniVoznjeLinijeId { get; set; }
        private bool _pon;
        private bool _uto;
        private bool _sri;
        private bool _cet;
        private bool _pet;
        private bool _sub;
        private bool _ned;
      
        public bool pon
        {
            set { _pon = value; }
            get { return _pon; }
        }
        public bool uto
        {
            set { _uto = value; }
            get { return _uto; }
        }
        public bool sri
        {
            set { _sri = value; }
            get { return _sri; }
        }
        public bool cet
        {
            set { _cet = value; }
            get { return _cet; }
        }
        public bool pet
        {
            set { _pet = value; }
            get { return _pet; }
        }
        public bool sub
        {
            set { _sub = value; }
            get { return _sub; }
        }
        public bool ned
        {
            set { _ned = value; }
            get { return _ned; }
        }

        public DaniVoznjeLinije()
        {
            pon = false;
            uto = false;
            sri = false;
            cet = false;
            pet = false;
            sub = false;
            ned = false;
        }
        public virtual List<Linija> daniVoznjeLinija { get; set; }
    }
}
