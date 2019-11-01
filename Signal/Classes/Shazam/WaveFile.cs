using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Signal
{
    public class WaveFile
    {
        private string wavePath;
        private string waveName;
        private Complex[] waveData;

        /// <summary>
        /// Представляет для чтения содержимое вэйв файла
        /// </summary>
        public Complex[] Data
        {
            get {return waveData;}
            private set {waveData = value;}
        }
        public string Name
        {
            get { return waveName; }
            private set { waveName = value; }
        }

        public WaveFile(string path)
        {
            wavePath = path;
            this.ReadData();       
        }
        /// <summary>
        /// выполняет чтение блока данных вэйв файла после заголовка
        /// </summary>
        /// <returns></returns>
        private void ReadData()
        {
            byte[] wave;
            try
            {
                using (System.IO.FileStream WaveFile = System.IO.File.OpenRead(wavePath))
                {
                    Name = WaveFile.Name;
                    wave = new byte[WaveFile.Length];
                    Data = new Complex[(wave.Length - 44) / 4];//shifting the headers out of the PCM data;
                    WaveFile.Read(wave, 0, Convert.ToInt32(WaveFile.Length));//read the wave file into the wave variable
                    /***********Converting and PCM accounting***************/
                    for (int i = 0; i < Data.Length; i++)
                    {
                        Data[i] = (BitConverter.ToInt32(wave, 44 + i * 4)) / 4294967296.0;
                    }
                }
            }
            catch (Exception)
            {
                
            }     
        }
    }
}
