using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Signal
{
    /// <summary>
    /// Суперкласс для работы с интерфейсом IQueryPartConstructor
    /// </summary>
    class FILTER
    {
        List<CheckBox> chosenCountryes = new List<CheckBox>();
        List<CheckBox> chosenCategories = new List<CheckBox>();
        List<CheckBox> chosenDiapazon = new List<CheckBox>();
        List<CheckBox> chosenModulation = new List<CheckBox>();
        List<CheckBox> chosenDemodulation = new List<CheckBox>();
        IQueryPartConstructor queryConstructor;
        string COUNTRY, CATEGORY, DIAPAZON, MODULATION, DEMODULATION, RESULT;
        /// <summary>
        /// При инициализации сохраняет только нажатые чекбоксы
        /// </summary>
        /// <param name="countryes"></param>
        /// <param name="categoryes"></param>
        /// <param name="diapazon"></param>
        /// <param name="modulation"></param>
        /// <param name="demodulation"></param>
        public FILTER(List<CheckBox> countryes,List<CheckBox> categoryes, List<CheckBox> diapazon, List<CheckBox> modulation,List<CheckBox> demodulation)
        {
            foreach (var item in countryes)
            {
                if ((bool)item.IsChecked) chosenCountryes.Add(item);
            }
            foreach (var item in categoryes)
            {
                if ((bool)item.IsChecked) chosenCategories.Add(item);
            }
            foreach (var item in diapazon)
            {
                if ((bool)item.IsChecked) chosenDiapazon.Add(item);
            }
            foreach (var item in modulation)
            {
                if ((bool)item.IsChecked) chosenModulation.Add(item);
            }
            foreach (var item in demodulation)
            {
                if ((bool)item.IsChecked) chosenDemodulation.Add(item);
            }
        }
        /// <summary>
        /// Возвращает строку фильтров для запроса в базу
        /// </summary>
        /// <returns></returns>
        public string GetFilterQuery()
        {
            this.GetCountry();
            this.GetCategory();
            this.GetDiapazon();
            this.GetModulation();
            this.GetDemodulation();
            return RESULT = COUNTRY + CATEGORY + DIAPAZON + MODULATION + DEMODULATION;
        }

        private string GetCountry()
        {
            queryConstructor = new CountrySelector(chosenCountryes);
            return COUNTRY = queryConstructor.MakeQueryPart();
        }

        private string GetCategory()
        {
            queryConstructor = new CategorySelector(chosenCategories);
            return CATEGORY = queryConstructor.MakeQueryPart();
        }

        private string GetDiapazon()
        {
            queryConstructor = new DiapazonSelector(chosenDiapazon);
            return DIAPAZON = queryConstructor.MakeQueryPart();
        }

        private string GetModulation()
        {
            queryConstructor = new ModulationSelector(chosenModulation);
            return MODULATION = queryConstructor.MakeQueryPart();
        }

        private string GetDemodulation()
        {
            queryConstructor = new DemodulationSelector(chosenDemodulation);
            return DEMODULATION = queryConstructor.MakeQueryPart();
        }
    }
}
