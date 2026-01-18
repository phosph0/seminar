using System;

namespace kino
{
    internal class Program
    {
        const int PocetRad = 8;
        const int PocetSedadelVRade = 10;
        const int ZakladniCena = 180;
        const int VIPPriplatek = 70;

        static void Main(string[] args)
        {
            bool[,] sal = new bool[PocetRad, PocetSedadelVRade]; // false = volne, true = obsazene
            bool program = true;

            while (program)
            {
                ZobrazMenu();

                int volba;
                try
                {
                    Console.Write("zadejte volbu: ");
                    volba = int.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("neplatny vstup");
                    continue;
                }

                if (volba == 1)
                    ZobrazSal(sal);
                else if (volba == 2)
                    RezervujSedadlo(sal);
                else if (volba == 3)
                    program = false;
                else
                    Console.WriteLine("neplatna volba");
            }
        }

        static void ZobrazMenu()
        {
            Console.WriteLine("\nmenu:");
            Console.WriteLine("1 - zobrazit kinosal");
            Console.WriteLine("2 - rezervovat sedadlo");
            Console.WriteLine("3 - konec");
        }

        static void ZobrazSal(bool[,] sal)
        {
            Console.WriteLine("\nkinosal:");
            for (int r = 0; r < PocetRad; r++)
            {
                for (int s = 0; s < PocetSedadelVRade; s++)
                {
                    if (sal[r, s])
                        Console.Write("X ");
                    else if (r >= 6) // vip rady 7-8
                        Console.Write("V ");
                    else
                        Console.Write("O ");
                }
                Console.WriteLine();
            }
        }

        static void RezervujSedadlo(bool[,] sal)
        {
            int rada, sedadlo;
            try
            {
                Console.Write("zadejte radu (1 - " + PocetRad + "): ");
                rada = int.Parse(Console.ReadLine()) - 1;

                Console.Write("zadejte sedadlo (1 - " + PocetSedadelVRade + "): ");
                sedadlo = int.Parse(Console.ReadLine()) - 1;
            }
            catch
            {
                Console.WriteLine("neplatny vstup");
                return;
            }

            if (rada < 0 || rada >= PocetRad || sedadlo < 0 || sedadlo >= PocetSedadelVRade)
            {
                Console.WriteLine("sedadlo neexistuje");
                return;
            }

            if (sal[rada, sedadlo])
            {
                Console.WriteLine("sedadlo je obsazene");
                return;
            }

            sal[rada, sedadlo] = true;

            // cena se podle toho, jestli je sedadlo VIP
            int cena = ZakladniCena;
            if (rada > 6)
            {
                cena = ZakladniCena + VIPPriplatek;
            }
            Console.WriteLine($"rezervace probehla uspesne, cena listku: {cena} kc");
        }
    }
}
