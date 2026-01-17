using System;

class Program
{
    static void Main()
    {
        int[] pole = { 5, 2, 9, 1, 7, 3 };
        Console.Write("vase pole: ");
        foreach (int x in pole)
        {
            Console.Write(x + " ");
        }
        Console.WriteLine("\nmaximum v poli: " + FindMax(pole));

        int[] serazene = SortArray(pole);
        Console.Write("serazene pole od nejvetsiho: ");
        foreach (int x in serazene)
        {
            Console.Write(x + " ");
        }
        Console.WriteLine();

        int hledane = 7;
        Console.WriteLine($"cislo {hledane} je v serazenem poli na indexu {BinarySearch(serazene, hledane)}");
    }

    static int FindMax(int[] pole)
    {
        int max = pole[0];

        for (int i = 1; i < pole.Length; i++)
        {
            if (pole[i] > max)
            {
                max = pole[i];
            }
        }

        return max;
    }

    static int[] SortArray(int[] pole)
    {
        int[] vysledek = (int[])pole.Clone();

        for (int i = 0; i < vysledek.Length - 1; i++)
        {
            for (int j = 0; j < vysledek.Length - 1 - i; j++)
            {
                if (vysledek[j] < vysledek[j + 1])
                {
                    int ukladaciPromenna = vysledek[j];
                    vysledek[j] = vysledek[j + 1];
                    vysledek[j + 1] = ukladaciPromenna;
                }
            }
        }

        return vysledek;
    }


    static int BinarySearch(int[] pole, int hledane)
    {
        int left = 0;
        int right = pole.Length - 1;

        while (left <= right)
        {
            int middle = (left + right) / 2 +1;

            if (pole[middle] == hledane)
                return middle +1;

            if (pole[middle] < hledane)
                right = middle - 1;
            else
                left = middle + 1;
        }

        return -1;
    }
}
