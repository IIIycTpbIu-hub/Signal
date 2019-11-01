using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Signal
{
    class Pearson
    {
        public static double Correl(Complex[] data1, Complex[] data2)//корреляция методом пирсона
        {
            double x = 0, y = 0;
            double[] mas1 = new double[data1.Length];
            double[] mas2 = new double[data2.Length];
            for (int i = 0; i < data1.Length; i++)
            {
                mas1[i] = data1[i].Real;
                mas2[i] = data2[i].Real;
            }
            //вычисляем среднее арифметическое двух массивов
            for (int i = 0; i < mas1.Length; i++)
            {
                x += mas1[i];
                y += mas2[i];

                if (i == mas1.Length - 1)
                {
                    x = x / mas1.Length;
                    y = y / mas2.Length;
                }
            }

            //подсчет корреляции
            double chisl = 0, znam = 0, r = 0;
            for (int i = 0; i < mas1.Length; i++)
            {
                chisl += (mas1[i] - x) * (mas2[i] - y);
                znam += Math.Sqrt(Math.Pow(mas1[i] - x, 2)) * Math.Sqrt(Math.Pow(mas2[i] - y, 2));
            }
            r = chisl / znam;
            return r;
        }
    }
}
