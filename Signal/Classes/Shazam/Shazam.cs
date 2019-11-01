using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Signal
{
    class Shazam
    {
        //поля должны быть кратны степени двойки
        private int CorrelWindow = 512;
        private double enoughCorrelCoef = 0.7;//минимаьный коеффициент сходства файлов
        private double maxFileCorell = 0.0;
        private double outMaxFileCorell = 0.0;
        private string outValye = null;
        public double absMaxCorell = 0.0;
        public string fileName = null;
        private Dictionary<double, string> result = new Dictionary<double, string>();
        Complex[] whereToFindBuff;
        Complex[] whatToFindBuff;

        public string outputPersents
        {
            get { return outValye; }
        }

        public double MaxFileCorell
        {
            get { return outMaxFileCorell; }
        }

        public string AbsMaxCorell
        {
            get { return Convert.ToString(absMaxCorell * 100).Remove(2) + "%"; }
        }

        public void Search(WaveFile whatToFind, WaveFile whereToFind, int stepInWhatToFind = 2048, int whatToFindStartPosition = 300, int stepInWhereToFind = 2048)
        {
            whatToFindBuff = new Complex[CorrelWindow];
            whereToFindBuff = new Complex[CorrelWindow];
            while (whatToFindStartPosition + CorrelWindow < whatToFind.Data.Length)//цикл по whatToFind
            {
                this.FillTheBuff(ref whatToFindBuff, whatToFind, whatToFindStartPosition);
                whatToFindBuff = FFT.fft(whatToFindBuff);
                //считываем в буфер файлы с БД, преобразуем и считаем корелляцию
                //цикл по файлу из БД
                int startBDFilePoint = 300;
                while (startBDFilePoint + CorrelWindow < whereToFind.Data.Length)//цикл по whereToFind
                {
                    this.FillTheBuff(ref whereToFindBuff, whereToFind, startBDFilePoint, 256);
                    startBDFilePoint += 512;
                    whereToFindBuff = FFT.fft(whereToFindBuff);
                    double correl = Pearson.Correl(whatToFindBuff, whereToFindBuff);
                    if (maxFileCorell < correl) maxFileCorell = correl;
                    if (maxFileCorell > absMaxCorell)
                    {
                        absMaxCorell = maxFileCorell;
                        fileName = whereToFind.Name;
                    }
                    if (maxFileCorell > enoughCorrelCoef)
                    {
                        fileName = whereToFind.Name;
                        break;
                    }
                }
                whatToFindStartPosition += stepInWhatToFind;
            }
            outValye = Convert.ToString(maxFileCorell * 100);
            outValye = outValye.Remove(2) + "%";
            outMaxFileCorell = maxFileCorell;
            maxFileCorell = 0.0;
        }
        /// <summary>
        /// заполняе буфер с лобой точки из файла
        /// </summary>
        /// <param name="Buff"></param>
        /// <param name="waveFile"></param>
        /// <param name="startPosition"></param>
        /// <returns></returns>
        private Complex[] FillTheBuff(ref Complex[] Buff, WaveFile waveFile, int startPosition, int step = 512)
        {
            if (startPosition + 512 <= waveFile.Data.Length)
            {
                int j = 0;
                for (int i = startPosition; i < startPosition + step; i++)
                {
                    Buff[j] = waveFile.Data[i];
                    j++;
                }
                return Buff;
            }
            else return null;
        }

    }
}
