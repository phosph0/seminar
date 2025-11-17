using System;
namespace graf_retizku
{

    internal class Program
    {
        static void Main(string[] args)
        {
            int pocet_lidi = Convert.ToInt32(Console.ReadLine());
            int[,] matice = new int[pocet_lidi + 1, pocet_lidi + 1];
            string vstupni_data = Console.ReadLine();
            string[] dvojice = vstupni_data.Split();

            for (int i = 0; i < dvojice.Length; i++)
            {
                string[] seznam_dvojice = dvojice[i].Split('-');

                int a = Convert.ToInt32(seznam_dvojice[0]);
                int b = Convert.ToInt32(seznam_dvojice[1]);

                matice[a, b] = 1;
                matice[b, a] = 1;
            }
            for (int i = 0; i < pocet_lidi + 1; i++)
            {
                for (int j = 0; j < pocet_lidi + 1; j++)
                {
                    Console.Write($"{matice[i, j]} ");
                }
                Console.WriteLine();
            }
        }
    }
}
