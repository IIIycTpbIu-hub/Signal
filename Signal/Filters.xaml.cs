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

namespace Signal
{
    /// <summary>
    /// Логика взаимодействия для Filters.xaml
    /// </summary>
    public partial class Filters : Window
    {
        string SELECT = "SELECT SigTable.Название_сигнала, F1, F2, Режим_модуляции, F3, F4, Принадлежность, Модуляция ";
        string FROM = "FROM SigTable join Categories where Categories.Название_сигнала = SigTable.Название_сигнала ";
        List<CheckBox> countryes = new List<CheckBox>();
        List<CheckBox> categoryes = new List<CheckBox>();
        List<CheckBox>  diapazon = new List<CheckBox>();
        List<CheckBox>  modulation = new List<CheckBox>();
        List<CheckBox> demodulation = new List<CheckBox>(); //коллекции для каждой вкладки фильтров
        bool countrySelected, categorySelected, diapazonSelected, modulationSelected, demodulationSelected;

        public Filters()
        {
            InitializeComponent();
        } 

        /// <summary>
        /// Заполняет коллекции чекбоксов
        /// </summary>
        private void FillCheckBoxCollections()
        {
            countryes = new List<CheckBox>();
            categoryes = new List<CheckBox>();
            diapazon = new List<CheckBox>();
            modulation = new List<CheckBox>();
            demodulation = new List<CheckBox>();

            countryes.Add(Country_Allower);
            countryes.Add(Country_avstralia);
            countryes.Add(Country_Canada);
            countryes.Add(Country_CentralEurop);
            countryes.Add(Country_China);
            countryes.Add(Country_easternEurop);
            countryes.Add(Country_Europ);
            countryes.Add(Country_Franse);
            countryes.Add(Country_Germany);
            countryes.Add(Country_greatBritan);
            countryes.Add(Country_Iran);
            countryes.Add(Country_Ispany);
            countryes.Add(Country_Israil);
            countryes.Add(Country_Japan);
            countryes.Add(Country_Kipr);
            countryes.Add(Country_NorthAmerica);
            countryes.Add(Country_NorthCorea);
            countryes.Add(Country_Poland);
            countryes.Add(Country_Roman);
            countryes.Add(Country_Russia);
            countryes.Add(Country_SouthAfrica);
            countryes.Add(Country_Sweden);
            countryes.Add(Country_Unknown);
            countryes.Add(Country_USA);
            countryes.Add(Country_vengria);

            categoryes.Add(Millitary);
            categoryes.Add(Radars);
            categoryes.Add(Active);
            categoryes.Add(Disabled);
            categoryes.Add(Radiolovers);
            categoryes.Add(Commerical);
            categoryes.Add(Avia);
            categoryes.Add(Sea);
            categoryes.Add(Analog);
            categoryes.Add(Digital);
            categoryes.Add(Trank);
            categoryes.Add(Official);
            categoryes.Add(Satellite);
            categoryes.Add(Navigation);
            categoryes.Add(Jamming);
            categoryes.Add(NumberStations);
            categoryes.Add(ExactTime);

            diapazon.Add(MF);
            diapazon.Add(HF);
            diapazon.Add(VHF);
            diapazon.Add(UHF);
            diapazon.Add(SHF);

            modulation.Add(FM);
            modulation.Add(AM);
            modulation.Add(CW);
            modulation.Add(C4FM);
            modulation.Add(QAM);
            modulation.Add(VSB);
            modulation.Add(FMCW);
            modulation.Add(CDMA);
            modulation.Add(TDMA);
            modulation.Add(FDMA);
            modulation.Add(OFDM);
            modulation.Add(FMOP);
            modulation.Add(FSK);
            modulation.Add(BFSK);
            modulation.Add(MFSK);
            modulation.Add(MSK);
            modulation.Add(GMSK);
            modulation.Add(FFSK);
            modulation.Add(AFSK);
            modulation.Add(GFSK);
            modulation.Add(IFK);
            modulation.Add(PSK);
            modulation.Add(PAM);
            modulation.Add(PPM);
            modulation.Add(OOK);
            modulation.Add(AM2);
            modulation.Add(FM2);
            modulation.Add(SB2);
            modulation.Add(CW2);

            demodulation.Add(NFM);
            demodulation.Add(WFM);
            demodulation.Add(DSB);
            demodulation.Add(USB);
            demodulation.Add(SC_FDMA);
            demodulation.Add(SSB);
            demodulation.Add(FHSS_TTDM);
            demodulation.Add(RAW);
            demodulation.Add(None);
        }
        /// <summary>
        /// Формирует строку фильтров
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FindSignals_Click(object sender, RoutedEventArgs e)
        {
            this.CheckCountrySelection();
            this.CheckCategorySelection();
            this.CheckDiapazonSelection();
            this.CheckModulationSelection();
            FILTER filter = new FILTER(countryes, categoryes, diapazon, modulation, demodulation);
            string FilterQuery = SELECT + FROM + filter.GetFilterQuery();
            FilterExecuter.Execute(FilterQuery);
        }

        private bool CheckDefolt()//проверка чекбоксов дефолтного состояния
        {
            if (countrySelected == categorySelected == diapazonSelected == modulationSelected == false)
                return true;
            else return false;
        }
        /// <summary>
        /// Производит проверку выбора на вкладке стран. Если выбрана одна из стран, снимает галку с вкладки все при нажатии кнопки поиска
        /// </summary>
        void CheckCountrySelection()
        {
            countrySelected = this.IsSomeChosen(countryes);
            if (countrySelected) Country_any.IsChecked = false;
            else Country_any.IsChecked = true;
        }
        /// <summary>
        /// Производит проверку выбора на вкладке категорий. Если выбрана одна из стран, снимает галку с вкладки все при нажатии кнопки поиска
        /// </summary>
        void CheckCategorySelection()
        {
            categorySelected = this.IsSomeChosen(categoryes);
            if (categorySelected) category_any.IsChecked = false;
            else category_any.IsChecked = true;
        }
        /// <summary>
        /// Производит проверку выбора на вкладке диапазона. Если выбрана одна из стран, снимает галку с вкладки все при нажатии кнопки поиска
        /// </summary>
        void CheckDiapazonSelection()
        {
            diapazonSelected = this.IsSomeChosen(diapazon);
            if (diapazonSelected) diap_any.IsChecked = false;
            else diap_any.IsChecked = true;
        }
        /// <summary>
        /// Производит проверку выбора на вкладке модуляции. Если выбрана одна из стран, снимает галку с вкладки все при нажатии кнопки поиска
        /// </summary>
        void CheckModulationSelection()
        {
            modulationSelected = this.IsSomeChosen(modulation);
            if (modulationSelected)
            {
                mod_any.IsChecked = false;
                mod_any2.IsChecked = false;
            }
            else
            {
                mod_any.IsChecked = true;
                mod_any2.IsChecked = true;
            }
        }
        /// <summary>
        /// проверка на то, что выбрана какая-то страна
        /// </summary>
        /// <param name="CheckBoxes"></param>
        /// <returns></returns>
        private bool IsSomeChosen(List<CheckBox> CheckBoxes)
        {
            foreach (var item in CheckBoxes)
            {
                if ((bool)item.IsChecked)
                    return true;
            }
            return false;
        }

        private void Filters1_Loaded(object sender, RoutedEventArgs e)
        {
            this.FillCheckBoxCollections();
            //this.OnGotFocus(e);
        }
        /// <summary>
        /// отменяет нажатые чекбоксы в коллекции
        /// </summary>
        /// <param name="chosenCheckBoxes"></param>
        private void ChangeSelection(List<CheckBox> chosenCheckBoxes)
        {
            foreach (var item in chosenCheckBoxes)
            {
                if ((bool)item.IsChecked) item.IsChecked = false;
            }
        }

        private void Country_any_Checked(object sender, RoutedEventArgs e)
        {
            this.ChangeSelection(countryes);
        }

        private void category_any_Checked(object sender, RoutedEventArgs e)
        {
            this.ChangeSelection(categoryes);
        }

        private void diap_any_Checked(object sender, RoutedEventArgs e)
        {
            this.ChangeSelection(diapazon);
        }

        private void mod_any_Checked(object sender, RoutedEventArgs e)
        {
            this.ChangeSelection(modulation);
        }

        private void Filters1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindow.filtersWindow = null;
        }
 
    }
}
