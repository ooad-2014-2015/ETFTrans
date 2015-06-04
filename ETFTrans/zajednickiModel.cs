using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETFTrans
{
    class zajednickiModel
    {
        private static readonly testViewModel model = new testViewModel();

        public static testViewModel Model
        {
            get { return zajednickiModel.model; }
        } 



    }
}
