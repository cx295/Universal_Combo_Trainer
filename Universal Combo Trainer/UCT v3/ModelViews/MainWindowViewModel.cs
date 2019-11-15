using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using UCT_v3.Models;

namespace UCT_v3.ModelViews
{
    sealed class MainWindowViewModel : INotifyPropertyChanged
    {
        private Models.ResourceFile comboresource;
        
        private Models.ResourceFile keyresource;
       
        private ICommand browsecombocommand;
       
        private ICommand browsekeycommand;

        private bool canExecute = true;

        public string BrowseComboContent
        {
            get
            {
                return "Browse";
            }
        }

        public string BrowseKeyContent
        {
            get
            {
                return "Browse";
            }
        }

        public bool CanExecute
        {
            get
            {
                return this.canExecute;
            }
            set
            {
                if (this.canExecute == value)
                {
                    return;
                }
                this.canExecute = value;
            }
        }

        public ICommand BrowseComboCommand
        {
            get
            {
                return browsecombocommand;
            }
            set
            {
                browsecombocommand = value;
            }
        }

        public ICommand BrowseKeyCommand
        {
            get
            {
                return browsekeycommand;
            }
            set
            {
                browsekeycommand = value;
            }
        }

        public MainWindowViewModel()
        {
            comboresource = new Models.ResourceFile();
            keyresource = new Models.ResourceFile();

            BrowseComboCommand = new RelayCommand(BrowseForFile, param => comboresource);
            BrowseKeyCommand = new RelayCommand(BrowseForFile, param => keyresource);
        }

        public void BrowseForFile(Models.ResourceFile resourceFile)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = resourceFile.DefaultExtention;
            dlg.Filter = resourceFile.Filter;

            Nullable<bool> result = dlg.ShowDialog();

            if(result == true)
            {
                resourceFile.ResourceFullPath = dlg.FileName;
            }
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
