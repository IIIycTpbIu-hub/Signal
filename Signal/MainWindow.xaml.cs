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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.Data.SQLite;
using System.Data;
using System.IO;
using System.Drawing;
using System.Media;
using Microsoft.Win32;

namespace Signal
{
    delegate void Filter(string query);
    delegate void Searcher(Dictionary<double,string> result);
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static SQLiteConnection dbConnection;
        public static SQLiteCommand dbCmd;
        public static string dbPath = Environment.CurrentDirectory + "\\Data\\dataBase\\SigBaseV2.s3db";
        DataTable dTable;
        DBProvider provider = new DBProvider();
        string nameOfSignal;
        public static string sqlQuery = "SELECT Название_сигнала, F1, F2, Режим_модуляции, F3, F4, Принадлежность, Модуляция FROM SigTable ";
        public static Filters filtersWindow;
        public static SearchSettings searchSettingsWindow;


        public MainWindow()
        {
            InitializeComponent();
        }

        private void Main_Initialized(object sender, EventArgs e)
        {
            Main.Visibility = System.Windows.Visibility.Hidden;
            LoadingWindow w = new LoadingWindow();
            w.Show();
            Thread.Sleep(2000);
            w.Close();
            Main.Visibility = System.Windows.Visibility.Visible;
            DG.CanUserAddRows = false;
            nameOfSignal = dTable.DefaultView[DG.SelectedIndex]["Название_сигнала"].ToString();
            this.FormUpdate(nameOfSignal);
            FilterExecuter.CreateSearchDTable = this.CreateSearchTable;
        }

        private void DataGrid_Initialized_1(object sender, EventArgs e)
        {
            dTable = new DataTable();
            dTable = provider.GetTable(sqlQuery);
            DG.ItemsSource = dTable.DefaultView;
            DG.SelectionMode = DataGridSelectionMode.Single;
            FilterExecuter.Execute = this.ExecuteFilter;
        }
        /// <summary>
        /// Нумерует строки в таблице
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DG_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }

        private void DG_MouseUp(object sender, MouseButtonEventArgs e)
        {
            nameOfSignal = dTable.DefaultView[DG.SelectedIndex]["Название_сигнала"].ToString();
            this.FormUpdate(nameOfSignal);
        }
        
        private void playSound_Click(object sender, RoutedEventArgs e)
        {
            SystemSounds.Beep.Play();
            try
            {
                SoundPlayer player = new SoundPlayer(Environment.CurrentDirectory + "\\Data\\Sounds\\" + nameOfSignal + ".wav");
                if (playSound.Content.ToString() != "Остановить")
                {
                    player.Play();
                    playSound.Content = "Остановить";
                }
                else
                {
                    player.Stop();
                    playSound.Content = "Воспроизвести сигнал";
                }
            }
            catch
            {
                MessageBox.Show("Для выбранного сигнала нет звукового файла");
            }
            
        }
        /// <summary>
        /// поиск по названию
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void searchByName_Click(object sender, RoutedEventArgs e)
        {
            string lookingSignal = "";
            string command = "";
            DataTable tempResult = new DataTable();
            if (signalNameTextBox.Text != "")
            {
                lookingSignal = signalNameTextBox.Text;
                command = "SELECT Название_сигнала, F1, F2, Режим_модуляции, F3, F4, Принадлежность, Модуляция FROM SigTable WHERE [Название_сигнала] = \"" + lookingSignal + "\"";
                signalNameTextBox.Text = "";
                nameOfSignal = lookingSignal;
            }
            else
            {
                command = "SELECT Название_сигнала, F1, F2, Режим_модуляции, F3, F4, Принадлежность, Модуляция FROM SigTable";
            }
            tempResult = provider.GetTable(command);
            if (tempResult.Rows.Count == 0)
            {
                MessageBox.Show("Ничего не найдено");
                return;
            }
            dTable = tempResult;
            DG.ItemsSource = dTable.DefaultView;
            this.FormUpdate(nameOfSignal);
        }
        /// <summary>
        /// вызывается при нажатии стрелок на клавиатуре. Перемещает положение фокуса строки вниз или вверх
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DG_KeyUp(object sender, KeyEventArgs e)
        {
            nameOfSignal = dTable.DefaultView[DG.SelectedIndex]["Название_сигнала"].ToString();
            this.FormUpdate(nameOfSignal);
        }

        /// <summary>
        /// обновляет картинки и описание радиосигналов
        /// </summary>
        /// <param name="nameOfSignal"></param>
        private void FormUpdate(string nameOfSignal)
        {
            waterfall.Source = provider.GetWaterFall(nameOfSignal);
            spectr.Source = provider.GetSpectr(nameOfSignal);
            description.Text = provider.GetDescription(nameOfSignal);
            RFR_bottom.Text = provider.GetRFR_bottom(nameOfSignal);
            RFR_top.Text = provider.GetRFR_top(nameOfSignal);
            Spectr_bottom.Text = provider.GetBottomSpectrValue(nameOfSignal);
            Spectr_top.Text = provider.GetTopSpectrValue(nameOfSignal);
        }

        private void searchBySound_Click(object sender, RoutedEventArgs e)
        {
            this.SearchSettingsWindowAppear();
        }
        /// <summary>
        /// выполняет запрос по строке фильтров, обновляя содержимое таблицы
        /// </summary>
        /// <param name="query"></param>
        public void ExecuteFilter(string query)
        {
            if (!Main.IsVisible)
                Main.Visibility = System.Windows.Visibility.Visible;
            dTable = new DataTable();
            dTable = provider.GetTable(query);
            DG.ItemsSource = dTable.DefaultView;
            DG.SelectionMode = DataGridSelectionMode.Single;
            if (dTable.Rows.Count != 0)
            {
                nameOfSignal = dTable.DefaultView[DG.SelectedIndex]["Название_сигнала"].ToString();
                this.FormUpdate(nameOfSignal);
            }
            else MessageBox.Show("Ничего не найдено");
        }

        private void FiltersWindow_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (filtersWindow == null)
            {
                filtersWindow = new Filters();
                filtersWindow.Show();
            }
            else
            {
                //filtersWindow.Show();
                filtersWindow.Focus();
            }
        }

        private void Main_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (filtersWindow != null)
                filtersWindow.Close();
            if (searchSettingsWindow != null)
                searchSettingsWindow.Close();
        }

        private void SearchSettinsWindow_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.SearchSettingsWindowAppear();           
        }

        /// <summary>
        /// Открывает окно настроек поиска
        /// </summary>
        private void SearchSettingsWindowAppear()
        {
            if (searchSettingsWindow == null)
            {
                searchSettingsWindow = new SearchSettings();
                //searchSettingsWindow = SearchSettings.GetInstance();
                if (!searchSettingsWindow.EmptyPath)
                {
                    searchSettingsWindow.Show();
                }
                else searchSettingsWindow.Close();
            }
            else searchSettingsWindow.Focus();
            
        }
        /// <summary>
        /// создает запрос после отработки поска по звуку
        /// </summary>
        /// <param name="result"></param>
        private void CreateSearchTable(Dictionary<double, string> result)
        {
            string soundResultSignalNames = "";
            foreach (var item in result)
                soundResultSignalNames += "SigTable.Название_сигнала = " + "\"" + item.Value + "\"" + " or ";
            soundResultSignalNames = soundResultSignalNames.Remove(soundResultSignalNames.Length - 3);
            string soundQuery = sqlQuery + "WHERE " + soundResultSignalNames;
            this.ExecuteFilter(soundQuery);
        }
    }
}
