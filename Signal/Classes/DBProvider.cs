using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
using System.IO;
using System.Windows.Media.Imaging;


namespace Signal
{
    public class DBProvider
    {
        SQLiteCommand DBComand;
        SQLiteDataReader dataReader = null;
        string DBPath = MainWindow.dbPath;

        
        /// <summary>
        /// Возвращает таблицу с БД согласно выполенному запросу
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public DataTable GetTable(string query)
        {
            DataTable table = new DataTable();
            using (var DBConnection = new SQLiteConnection("Data Source=" + DBPath + ";Version=3;"))
            {
                DBConnection.Open();
                SQLiteDataAdapter Adapter = new SQLiteDataAdapter(query, DBConnection);
                Adapter.Fill(table);
            } 
            return table;
        }
        /// <summary>
        /// Получает изображение водопада с БД, сохраняет в файл
        /// </summary>
        /// <param name="signalName"></param>
        public BitmapImage GetWaterFall(string signalName)
        {
            string command = "SELECT Waterfall FROM SigTable WHERE [Название_сигнала] = \"" + signalName + "\"";
            BitmapImage newImage = this.GetImageFromDB(command);
            return newImage;
        }
        /// <summary>
        /// Получает значение спектра сигнала с БД, сохраняет в файл
        /// </summary>
        /// <param name="signalName"></param>
        public BitmapImage GetSpectr(string signalName)
        {
            string command = "SELECT Spectr FROM SigTable WHERE [Название_сигнала] = \"" + signalName + "\"";
            BitmapImage newImage = this.GetImageFromDB(command);
            return newImage;
        }
        /// <summary>
        /// Возвращает описание сигнала из БД
        /// </summary>
        /// <param name="signalName"></param>
        /// <returns></returns>
        public string GetDescription(string signalName)
        {
            string command = "SELECT Description FROM SigTable WHERE [Название_сигнала] = \"" + signalName + "\"";
            string description = this.GetStringFromDB(command);
            return description;
        }
        /// <summary>
        /// Получает нижнюю границу диапазона радиочастот
        /// </summary>
        /// <param name="signalName"></param>
        /// <returns></returns>
        public string GetRFR_bottom(string signalName)
        {
            string command = "SELECT F1 FROM SigTable WHERE [Название_сигнала] = \"" + signalName + "\"";
            string RFR_bottom = this.GetStringFromDB(command);
            return RFR_bottom;
        }
        /// <summary>
        /// Получает верхнюю границу диапазона радиочастот
        /// </summary>
        /// <param name="signalName"></param>
        /// <returns></returns>
        public string GetRFR_top(string signalName)
        {
            string command = "SELECT F2 FROM SigTable WHERE [Название_сигнала] = \"" + signalName + "\"";
            string RFR_top = this.GetStringFromDB(command);
            return RFR_top;
        }
        /// <summary>
        /// Получает нижнюю границу спектра из БД
        /// </summary>
        /// <param name="signalName"></param>
        /// <returns></returns>
        public string GetBottomSpectrValue(string signalName)
        {
            string command = "SELECT F3 FROM SigTable WHERE [Название_сигнала] = \"" + signalName + "\"";
            string spectrBottomValue = this.GetStringFromDB(command);
            return spectrBottomValue;
        }
        /// <summary>
        /// Получает верхнюю границу спектра из БД
        /// </summary>
        /// <param name="signalName"></param>
        /// <returns></returns>
        public string GetTopSpectrValue(string signalName)
        {
            string command = "SELECT F4 FROM SigTable WHERE [Название_сигнала] = \"" + signalName + "\"";
            string spectrTopValue = this.GetStringFromDB(command);
            return spectrTopValue;
        }
        /// <summary>
        /// Получает картинку из базы данных
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        private BitmapImage GetImageFromDB(string query)
        {
            BitmapImage image = new BitmapImage();
            using (var DBConnection = new SQLiteConnection("Data Source=" + DBPath + ";Version=3;"))
            {
                DBConnection.Open();
                DBComand = new SQLiteCommand(query, DBConnection);
                dataReader = DBComand.ExecuteReader();
                dataReader.Read();
                if (dataReader[0] is DBNull)
                {
                    return image;
                }
                byte[] imageBytes = (byte[])dataReader[0];

                using (MemoryStream stream = new MemoryStream(imageBytes))
                {
                    stream.Position = 0;
                    image.BeginInit();
                    image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.UriSource = null;
                    image.StreamSource = stream;
                    image.EndInit();
                    image.Freeze();
                }


                return image;
            }
        }
        /// <summary>
        /// Получает строковое поле из БД
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        private string GetStringFromDB(string query)
        {
            string str;
            using (var DBConnection = new SQLiteConnection("Data Source=" + DBPath + ";Version=3;"))
            {
                DBConnection.Open();
                DBComand = new SQLiteCommand(query, DBConnection);
                dataReader = DBComand.ExecuteReader();
                dataReader.Read();
                str = dataReader[0].ToString();
            }
            return str;
        }
    }
}
