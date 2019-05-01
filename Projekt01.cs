using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp11
{
    class Program
    {
        const int NIter = 10;
        static int[] tablica;
        static long Licz;
        static double srednia;
        
        static ulong wielkosc;
        static long ilosc;
        static ulong dlugosc;

        //---------Liniowe-----------

        static bool Liniowe(int[] tab, int szukana)  //BEZ INSTRUMENTACJI
        {
            for (int i = 0; i < tab.Length; i++)
                if (tab[i] == szukana)
                    return true;
            return false;
        }
        static void LinioweCzas()
        {
            double elapsed;
            long elapsedTime = 0, MinTime = long.MaxValue, MaxTime = long.MinValue, czas;
            for (int n = 0; n < (NIter + 1 + 1); ++n)
            {
                long start = Stopwatch.GetTimestamp();
                bool result = Liniowe(tablica, tablica.Length - 1);
                long end = Stopwatch.GetTimestamp();
                czas = end - start;
                elapsedTime += czas;

                if (czas < MinTime) MinTime = czas;
                if (czas > MaxTime) MaxTime = czas;
            }
            elapsedTime -= (MinTime + MaxTime);
            elapsed = elapsedTime * (1.0 / (NIter * Stopwatch.Frequency));
            Console.Write("\t" + elapsed.ToString("F10"));
        }
        static bool LinioweInstrSpr(int[] tab, int szukana)
        {
            for (int i = 0; i < tab.Length; i++)
            {
                Licz++;
                if (tab[i] == szukana) return true;
            }
            return false;
        }
        static void LinioweInstr()
        {
            Licz = 0;
            bool result = LinioweInstrSpr(tablica, tablica.Length - 1);
            Console.Write("\t" + Licz);

        }


        static bool LinioweInstrSrSpr(int[] tab, int szukana)
        {
            for (int i = 0; i < tab.Length; i++)
            {
                Licz++;
                srednia += Licz;
                if (tab[i] == szukana) return true;
            }
            return false;
        }
        static void LinioweInstrSr()
        {
            Licz = 0;
            srednia = 0.00000;
            bool result = LinioweInstrSrSpr(tablica, tablica.Length - 1);
            Console.Write("\t" + srednia / Licz);

        }


        //------------Binarne----------

        static int Binarne(int[] tablica, int szukana) //BEZ INSTRUMENTACJI
        {
            /*Random los = new Random();

            int szukana = los.Next(1, 50);*/
            //Sortowanie(tablica);
            for (int i = 0; i < tablica.Length; i++)
            {
                int left = 0, right = tablica.Length - 1, middle;
                while (left <= right)
                {
                    middle = (left + right) / 2;
                    if (tablica[middle] == szukana) return middle + 1;
                    else if (tablica[middle] > szukana) right = middle - 1;
                    else left = middle + 1;


                }
            }
            return -1;

        }

        static bool BinarneInstrSpr(int[] tab, int szukana)
        {
            int left = 0, right = tab.Length - 1, mid;
            while (left <= right)
            {
                mid = (left + right) / 2;
                Licz++;
                if (tab[mid] == szukana) return true;
                else
                {

                    if (tab[mid] == szukana) right = mid - 1;
                    else left = mid + 1;
                }
            }
            return false;
        }
        static int BinarneInstrSre(int[] tab, int szukana, int tablicaLength)
        {
            int left = 0;
            int right = tab.Length - 1;
            int middle = 0;
            wielkosc = 0;
            ilosc = 0;
            dlugosc = 0;
            while (left <= right)
            {
                ilosc++;
                middle = (left + right) / 2;
                wielkosc += (ulong)tab[ilosc];
                dlugosc += (ulong)Math.Pow(2, ilosc - 1);
                if (tab[middle] == szukana)
                {
                    ilosc++;
                    wielkosc += (ulong)tab[ilosc];
                    dlugosc += (ulong)Math.Pow(2, ilosc - 1);

                    return middle;
                }
                else if (tab[middle] < szukana)
                {
                    ilosc++;
                    wielkosc += (ulong)tab[ilosc];
                    dlugosc += (ulong)Math.Pow(2, ilosc - 1);

                    left = middle + 1;
                }
                else
                {
                    ilosc++;
                    wielkosc += (ulong)tab[ilosc];
                    dlugosc += (ulong)Math.Pow(2, ilosc - 1);

                    right = middle - 1;

                }
            }
            return -1;

        }
        static void BinarneInstr()
        {
            Licz = 0;
            bool result = BinarneInstrSpr(tablica, tablica.Length - 1);
            Console.Write("\t" + Licz);
        }

        static void BinarneCzas(int[] tablica, int szukana)
        {

            double elapsedSeconds;
            long elapsedTime = 0, MinTime = long.MaxValue, MaxTime = long.MinValue;
            for (int n = 0; n < (NIter + 1 + 1); ++n)
            {
                long start = Stopwatch.GetTimestamp();
                int result = Binarne(tablica, szukana);
                int left = 0, right = tablica.Length - 1, mid;
                while (left <= right)
                {
                    mid = (left + right) / 2;

                    if (tablica[mid] == szukana) return;

                    else


                        if (tablica[mid] == szukana) right = mid - 1;
                    else left = mid + 1;


                }
                long end = Stopwatch.GetTimestamp();
                long elapsed = end - start;
                elapsedTime += elapsed;
                if (elapsed < MinTime) MinTime = elapsed;
                if (elapsed > MaxTime) MaxTime = elapsed;


            }
            elapsedTime -= (MinTime + MaxTime);
            elapsedSeconds = elapsedTime * (1.0 / (NIter * Stopwatch.Frequency));
            Console.WriteLine("\t{0}", elapsedSeconds.ToString("F10"));
        }
        static int[] Sortowanie(int[] tablica)
        {
            int sort;
            for (long j = 0; j < tablica.Length; j++)
            {
                for (long i = 0; i < tablica.Length - 1; i++)
                {
                    if (tablica[i] > tablica[i + 1])
                    {
                        sort = tablica[i];
                        tablica[i] = tablica[i + 1];
                        tablica[i + 1] = sort;
                    }
                }

            }

            return tablica;

        }

        //----------MAIN------------

        static void Main(string[] args)
        {

            Console.WriteLine("Pesymistyczna złożoność");
            Random los = new Random();
            int szukana = los.Next(1);
            //Random r = new Random();
            //int zmienna = r.Next(1001, 2000);
            Console.WriteLine("Size \tLinMaxInstr \tLinMaxTim \tBinMaxInstr \tBinMaxTim");
            //for (int k = 1; k <= 10; k += 1)
            for (int k = 33554432; k <= 268435456; k += 33554432)
            {
                Console.Write(k);
                tablica = new int[k];
                for (int i = 2; i < tablica.Length; i++)
                {

                    tablica[i] = i;
                }

                LinioweInstr();
                //LinioweInstrSr();
                LinioweCzas();
                BinarneInstr();
                BinarneCzas(tablica, szukana);

            }

            Console.WriteLine();
            Console.WriteLine("Zakończono pomiar pesymistyczny, naciśnij dowolny klawisz...");
            Console.ReadKey();


            int index = 0;
            ilosc = 0;
            wielkosc = 0;
            dlugosc = 0;
            
            Console.WriteLine("Średnia złożoność");
            Console.WriteLine("Size \tLinSrInstr \tBinSRInstr");
            //for (int k = 1; k <= 10; k += 1)
            for (int k = 33554432; k <= 268435456; k += 33554432)
            {
                Console.Write(k);
                tablica = new int[k];
                int y = tablica.Length - 1;
                for (int i = 2; i < tablica.Length; i++)
                {

                    tablica[i] = i;
                }

                
                LinioweInstrSr();
                for (int l = 0; l < NIter + 2; l++)
                {
                    
                    index = BinarneInstrSre(tablica, y, tablica.Length);


                }
                Console.WriteLine("\t" + wielkosc);



            }
            Console.WriteLine("Zakończono pomiar średni, naciśnij dowolny klawisz...");
            Console.ReadKey();


        }
    

       

    }
}
