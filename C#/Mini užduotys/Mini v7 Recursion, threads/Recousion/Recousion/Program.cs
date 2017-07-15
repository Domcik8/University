using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace Recousion
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch watch = new Stopwatch();
            Program manager = new Program(); 
            string path = @"D:\Muzyka\Wikipedias Top 2000\";
            string ext = @"*.mp3";
            long prime = 90, timer1, timer2;

            watch.Start();
            manager.GetFiles(ext, path);
            prime = manager.GetPrime(prime);
            watch.Stop();
            timer1 = watch.ElapsedMilliseconds;
            watch.Reset();

            watch.Start();
            Task task1 = new Task(() => manager.GetFiles(ext, path));
            task1.Start();
            Task<long> task2 = Task.Run(() => manager.GetPrime(prime));
            Task.WaitAll(task1, task2);
            watch.Stop();
            timer2 = watch.ElapsedMilliseconds;
            Console.WriteLine("Pirmas {0} antras {1}", timer1, timer2);
            Console.ReadLine();
        }

        public void GetFiles(string ext, string path)
        {
            FileInfo[] fileList;
            DirectoryInfo directorer = new DirectoryInfo(path);
            fileList = directorer.GetFiles(ext);


            Console.WriteLine("In " + path + ":");
            foreach(FileInfo file in fileList)
            {
                Console.WriteLine(file.Name);
            }
            Console.WriteLine();
            Console.WriteLine();

            string[] directoryList = Directory.GetDirectories(path);
            foreach(string directory in directoryList)
            {
                GetFiles(ext, directory);
            }
        }

        public long GetPrime(long n)
        {
            long prime = 0;
            Boolean div = false;
            if (n < 3) prime = 2;
            while(prime == 0)
            {
                for(long i = 2; i <= (n / 2); i++)
                {
                    if ((n % i) == 0)
                        div = true;
                }
                if (!div)
                    prime = n;
                div = false;
                n++;
            }
            Console.WriteLine(prime);
            Thread.Sleep(1000);
            return prime;
        }
    }
}
