using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Wac2015.ViewModels
{
    public class BaseViewModel: INotifyPropertyChanged
    {
        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool _isBusy;
        /// <summary>
        /// Gets or sets if the view is busy.
        /// </summary>
        public bool IsBusy
        {
            get { return _isBusy; }
            set { _notBusy = !value; SetProperty(ref _isBusy, value); }
        }

        private bool _notBusy = true;
        /// <summary>
        /// Gets or sets if the view is busy.
        /// </summary>
        public bool NotBusy
        {
            get { return _notBusy; }
            set { SetProperty(ref _notBusy, value); }
        }

        protected void SetProperty<T>(ref T backingStore, T value, Action onChanged = null, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return;

            backingStore = value;

            if (onChanged != null)
                onChanged();

            NotifyPropertyChanged(propertyName);
        }
    }
}
