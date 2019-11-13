using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;

namespace UCT_v3.ModelViews
{
    sealed class MainWindowViewModel : INotifyPropertyChanged
    {
        private Models.ResourceFile comboresource;
        
        private Models.ResourceFile keyresource;
       
        private ICommand browsecombocommand;
       
        private ICommand browsekeycommand;

        private bool canExecute = true;

        public MainWindowViewModel()
        {
            comboresource = new Models.ResourceFile();
            keyresource = new Models.ResourceFile();
        }

        public string ComboPathandFile
        {
            get { return comboresource.ResourceFullPath; }
            set
            {
                if(comboresource.ResourceFullPath != value)
                {
                    comboresource.ResourceFullPath = value;
                    OnPropertyChange("ComboPathandFile");
                }
            }
        }

        public string KeyPathandFile
        {
            get { return keyresource.ResourceFullPath; }
            set
            {
                if(keyresource.ResourceFullPath != value)
                {
                    keyresource.ResourceFullPath = value;
                    OnPropertyChange("KeyPathandFile");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChange(string propertyName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
