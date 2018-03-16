using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Dictionary;

namespace assignment6
{
    class Program
    {
        static void Main(string[] args)
        {

            DictionaryRedBlack<int, string> dictionaryRB = new DictionaryRedBlack<int, string>();
            AVLDictionary<int, string> dictionaryAVL = new AVLDictionary<int, string>();
            Stopwatch time = new Stopwatch();
            Random rand = new Random();
            int key;
            string str;

            time.Start();
            for (int i = 0; i < 320; i++)
            {
                key = rand.Next(1, 5000);
                str = key.ToString();
                dictionaryRB.Add(key, str);
            }
            time.Stop();
            Console.WriteLine(time.ElapsedMilliseconds);
            time.Reset();

            time.Start();
            for (int i = 0; i < 320; i++)
            {
                key = rand.Next(1, 5000);
                str = key.ToString();
                dictionaryAVL.Add(key, str);
            }
            time.Stop();
            Console.WriteLine(time.ElapsedMilliseconds);
            time.Reset();
            Console.ReadLine();
        }
    }
}
