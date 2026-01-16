using System;


class Program
{
    static void Main()
    {
        List<string> jmena = new List<string>();
        List<int> veky = new List<int>();
        List<float> znamky = new List<float>();

        //nacteni studentu
        int pocet = 0;
        while (true)
        {
            try
            {
                Console.Write("zadej pocet studentu: ");
                pocet = int.Parse(Console.ReadLine());
                if (pocet <= 0) throw new Exception();
                break;
            }
            catch
            {
                Console.WriteLine("neplatny vstup - pocet studentu musi byt kladny");
            }
        }

        for (int i = 0; i < pocet; i++)
        {
            Console.Write("jmeno: ");
            jmena.Add(Console.ReadLine());

            int vek = 0;
            while (true)
            {
                try
                {
                    Console.Write("vek: ");
                    vek = int.Parse(Console.ReadLine());
                    if (vek <= 0) throw new Exception();
                    break;
                }
                catch
                {
                    Console.WriteLine("neplatny vstup - vek musi byt kladny");
                }
            }
            veky.Add(vek);

            float znamka = 0;
            while (true)
            {
                try
                {
                    Console.Write("prumerna znamka: ");
                    znamka = float.Parse(Console.ReadLine());
                    if (znamka < 1 || znamka > 5) throw new Exception();
                    break;
                }
                catch
                {
                    Console.WriteLine("neplatny vstup - prumerna znamka musi byt v rozmezi 1–5");
                }
            }
            znamky.Add(znamka);
        }

        // vypsani menu
        bool program = true;

        while (program)
        {
            Console.WriteLine("\nMENU");
            Console.WriteLine("a – vsichni studenti");
            Console.WriteLine("b – studenti s prumerem lepsim nez 2,0");
            Console.WriteLine("c – prumer veku");
            Console.WriteLine("d – je vase cislo prvocislo?");
            Console.WriteLine("e – rozklad cisla na prvocisla");
            Console.WriteLine("f – konec programu");
            Console.Write("\nco chcete vypsat: ");

            string volba = Console.ReadLine();

            if (volba == "a")
            {
                VypisVsechny(jmena, veky, znamky);
            }
            else if (volba == "b")
            {
                VypisLepsiNez2(jmena, veky, znamky);
            }
            else if (volba == "c")
            {
                VypisPrumerVeku(veky);
            }
            else if (volba == "d")
            {
                BonusPrvocislo();
            }
            else if (volba == "e")
            {
                BonusRozklad();
            }
            else if (volba == "f")
            {
                program = false;
            }
            else
            {
                Console.WriteLine("neplatny vstup - vyberte neco z menu");
            }
        }
    }


    static void VypisVsechny(List<string> jmena, List<int> veky, List<float> znamky)
    {
        for (int i = 0; i < jmena.Count; i++)
        {
            Console.WriteLine($"{jmena[i]}({veky[i]}): {znamky[i]}");
        }
    }

    static void VypisLepsiNez2(List<string> jmena, List<int> veky, List<float> znamky)
    {
        for (int i = 0; i < jmena.Count; i++)
        {
            if (znamky[i] < 2.0f)
            {
                Console.WriteLine($"{jmena[i]}({veky[i]}): {znamky[i]}");
            }
        }
    }

    static void VypisPrumerVeku(List<int> veky)
    {
        int soucet = 0;
        foreach (int vek in veky)
        {
            soucet += vek;
        }
        Console.WriteLine($"prumer veku: {(float)soucet / veky.Count}");
    }

    // bonusz

    static void BonusPrvocislo()
    {
        Console.Write("zadej cislo: ");
        int cislo = int.Parse(Console.ReadLine());

        if (JePrvocislo(cislo))
            Console.WriteLine("cislo je prvocislo");
        else
            Console.WriteLine("cislo neni prvocislo");
    }

    static void BonusRozklad()
    {
        Console.Write("zadej cislo: ");
        int cislo = int.Parse(Console.ReadLine());

        Console.Write("rozklad na prvocisla: ");
        RozkladNaPrvocisla(cislo);
    }

    static bool JePrvocislo(int n)
    {
        if (n < 2) return false;

        for (int i = 2; i <= Math.Sqrt(n); i++)
        {
            if (n % i == 0)
                return false;
        }
        return true;
    }

    static void RozkladNaPrvocisla(int n)
    {
        int delitel = 2;

        while (n > 1)
        {
            while (n % delitel == 0)
            {
                Console.Write(delitel + " ");
                n /= delitel;
            }
            delitel++;
        }
        Console.WriteLine();
    }
}
