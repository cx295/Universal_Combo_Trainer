using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EDS.MVVM
{
    [Serializable]
    public class ObservableObject : INotifyPropertyChanged
    {
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            try
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            catch
            {

            }
        }

        protected void OnPropertyChanged(params string[] propertyNames)
        {
            if (propertyNames != null)
            {
                foreach (var propertyName in propertyNames)
                {
                    OnPropertyChanged(propertyName);
                }
            }
        }
    }
}
