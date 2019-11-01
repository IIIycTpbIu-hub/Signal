using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.IO;

namespace Signal
{
    /// <summary>
    /// Логика взаимодействия для SearchSettings.xaml
    /// </summary>
    public partial class SearchSettings : Window
    {
        private WaveFile whatToFind;
        private string filePath = null;
        private bool emptyFIlePath = false;
        string[] Files = null;

        public bool EmptyPath
        {
            get { return emptyFIlePath; }
            private set {emptyFIlePath = value;}
        }

        public SearchSettings()
        {
            //открываем диалоговое окно, в котором выбираем звуковой файл для поиска
            OpenFileDialog fileDialogWindow = new OpenFileDialog();
            //fileDialogWindow.InitialDirectory = "C:\\";
            fileDialogWindow.InitialDirectory = Environment.CurrentDirectory + "\\Data\\Sounds";
            fileDialogWindow.DefaultExt = ".wav";
            MessageBox.Show("Выберите файл для поиска");
            fileDialogWindow.ShowDialog();
            filePath = fileDialogWindow.FileName.ToString();
            whatToFind = new WaveFile(filePath);
            InitializeComponent();
            this.Show();
        }

        private void SearchSettingsWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (filePath == "")
            {
                this.EmptyPath = true;
                MainWindow.searchSettingsWindow = null;
            }
            //Получаем список всех файлов в каталоге Саундс
            //Files = Directory.GetFiles(Environment.CurrentDirectory + "\\Data\\Sounds");
            progressBar.Maximum = (double)Files.Length;
            
        }

        private void SearchSettingsWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindow.searchSettingsWindow = null;
        }

        private async void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            SearchButton.Visibility = System.Windows.Visibility.Hidden;
            MessageLable.Visibility = System.Windows.Visibility.Visible;
            Shazam searcher = new Shazam();
            Dictionary<double, string> result = new Dictionary<double,string>();
            Files = Directory.GetFiles(Environment.CurrentDirectory + "\\Data\\Sounds");
            foreach (var file in Files)
            {
                
                WaveFile whereToFind = new WaveFile(file);
                if ((bool)QuickSearch.IsChecked)
                {
                    int whatToFindMiddlePosition = (int)(whatToFind.Data.Length / 2);
                    searcher.Search(whatToFind, whereToFind, whatToFindMiddlePosition, whatToFindMiddlePosition - 256);
                }
                if ((bool)DefaultSearch.IsChecked)
                {
                    int stepInWhatToFind = whatToFind.Data.Length / 3;
                    searcher.Search(whatToFind, whereToFind, stepInWhatToFind, 0);
                }
                if ((bool)DeepSearch.IsChecked)
                {
                    searcher.Search(whatToFind, whereToFind);
                }
                await Task.Delay(10);
                progressBar.Value++;
                MessageLable.Content = searcher.outputPersents + " " + whereToFind.Name.Replace(Environment.CurrentDirectory + "\\Data\\Sounds\\", null);


                if (searcher.MaxFileCorell > 0.75)
                {
                    string name = whereToFind.Name.Replace(Environment.CurrentDirectory + "\\Data\\Sounds\\", null);
                    name = name.Replace(".wav", null);
                    result.Add(searcher.MaxFileCorell, name);
                }
            }
            progressBar.Value = 0;
            MessageLable.Content = "";
            SearchButton.Visibility = System.Windows.Visibility.Visible;
            MessageBox.Show("Максимальное сходство: " + searcher.outputPersents + " " + searcher.fileName.Replace(Environment.CurrentDirectory + "\\Data\\Sounds\\", null));
            FilterExecuter.CreateSearchDTable(result);
            this.Close();
        }
    }
}
