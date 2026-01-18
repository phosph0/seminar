using System;
using System.Collections.Generic;

//casova slozitost O(n^2)
// pametova O(n + m)
namespace graf_retizku
{

    internal class Program
    {
        static void Main(string[] args)
        {
            int pocetLidi = Convert.ToInt32(Console.ReadLine());
            int[,] matice = new int[pocetLidi, pocetLidi];
            string vstupniData = Console.ReadLine();
            string[] dvojice = vstupniData.Split();

            for (int i = 0; i < dvojice.Length; i++)
            {
                string[] seznamDvojice = dvojice[i].Split('-');

                int a = Convert.ToInt32(seznamDvojice[0]) - 1;
                int b = Convert.ToInt32(seznamDvojice[1]) - 1;

                matice[a, b] = 1;
                matice[b, a] = 1;
            }
            string [] startCil = Console.ReadLine().Split();
            int start = Convert.ToInt32(startCil[0]) - 1;
            int cil = Convert.ToInt32(startCil[1]) - 1;

            bool[] nalezeno = new bool[pocetLidi];
            int[] predchudce = new int[pocetLidi];
            nalezeno[start] = true;
            predchudce[start] = -1;
            bool existuje = false;

     
            // seznam pro předchůdce
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(start);
            while (queue.Count > 0)
            {
                // odebereme vrchol z fronty
                int vrchol = queue.Dequeue();
                if (vrchol == cil)
                {
                    existuje = true;
                    break;
                }

                // projdeme následníky
                for (int i = 0; i < pocetLidi; i++)
                {
                    if (matice[vrchol, i] == 1 && nalezeno[i]==false)
                    {
                        nalezeno[i] = true; // už je nalezený
                        predchudce[i] = vrchol;
                        queue.Enqueue(i);
                    }
                }
            }
            if (!existuje)
            {
                Console.WriteLine("neexistuje");
                return;
            }

            // rekonstrukce cesty
            List<int> cesta = new List<int>();
            int aktualni = cil;

            while (aktualni != -1)
            {
                cesta.Add(aktualni);
                aktualni = predchudce[aktualni];
            }

            for (int i = 0; i < cesta.Count; i++)
            {
                cesta[i] = cesta[i] + 1;
            }

            cesta.Reverse();
                Console.WriteLine(string.Join(" ", cesta));
            }
        }
    }