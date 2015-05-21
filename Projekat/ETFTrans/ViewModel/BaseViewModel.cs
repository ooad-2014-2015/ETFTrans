using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ETFTrans.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public virtual event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new  PropertyChangedEventArgs(propertyName));
        }

        public BaseViewModel()
        {
            btnOdjava = new RelayCommand(odjavaUposlenika);
        }

        private ICommand btnOdjava;
        public static string prijavljeniUser;
        public ICommand BtnOdjava
        {
            set
            {
                if (btnOdjava != value)
                    btnOdjava = value;
            }
            get
            {
                return btnOdjava;
            }
        }
        public void odjavaUposlenika()
        {
            MessageBox.Show("Not Implemented in BaseViewModel!");
        }
    }
}
