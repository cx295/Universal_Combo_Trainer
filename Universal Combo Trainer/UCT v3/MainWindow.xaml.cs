using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UCT_v3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        static string currentDirectory = Directory.GetCurrentDirectory();
        private void getKeybindingsFileButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.DefaultExt = ".json";
            openFileDialog.Filter = "JSON files (.json)|*.json";
            string keybindingsDirectory = System.IO.Path.GetFullPath(System.IO.Path.Combine(currentDirectory, @"..\..\..\Keybindings"));
            openFileDialog.InitialDirectory = keybindingsDirectory;
            Nullable<bool> result = openFileDialog.ShowDialog();

            if (result == true)
            {
                keyFilePathTextbox.Text = openFileDialog.FileName;
            }
        }

        private void getComboFileButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.DefaultExt = ".json";
            openFileDialog.Filter = "JSON files (.json)|*.json";
            string combosDirectory = System.IO.Path.GetFullPath(System.IO.Path.Combine(currentDirectory, @"..\..\..\Combos"));
            openFileDialog.InitialDirectory = combosDirectory;
            Nullable<bool> result = openFileDialog.ShowDialog();
            if(result == true)
            {
                comboFilePathTextbox.Text = openFileDialog.FileName;
            }
        }
    }
}
