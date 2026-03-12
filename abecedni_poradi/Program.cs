namespace abecedni_poradi
{
    internal class Program
    {
        static void Main()
        {
            string vstup = Console.ReadLine();
            string[] useky = vstup.Split(' ');

            int MAX_POCET_CHAR = 26;
            int PISMENO_A = 97; //hodnota "a" a ascii tabulce pro prevod do indexu

            List<int>[] graf = new List<int>[MAX_POCET_CHAR];
            int[] pocetPredchudcu = new int[MAX_POCET_CHAR];
            bool[] existuje = new bool[MAX_POCET_CHAR];

            for (int i = 0; i < MAX_POCET_CHAR; i++)
                graf[i] = new List<int>();

            // zjistime jaky jsou znaky ve vstupu
            foreach (string slovo in useky)
            {
                foreach (char znak in slovo)
                {
                    existuje[znak - PISMENO_A] = true;
                }
            }

            // vytvorime graf znaku
            for (int i = 0; i < useky.Length - 1; i++)
            {
                string slovo1 = useky[i];
                string slovo2 = useky[i+1];

                int minimum = Math.Min(slovo1.Length, slovo2.Length);

                for (int j = 0; j < minimum; j++)
                {
                    if (slovo1[j] != slovo2[j])
                    {
                        int u = slovo1[j] - PISMENO_A;
                        int v = slovo2[j] - PISMENO_A;

                        if (!graf[u].Contains(v))
                        {
                            graf[u].Add(v);
                            pocetPredchudcu[v]++;
                        }

                        break;
                    }
                }
            }

            Queue<int> fronta = new Queue<int>();

            // najdeme zdroje
            for (int i = 0; i < MAX_POCET_CHAR; i++)
            {
                if (existuje[i] && pocetPredchudcu[i] ==0)
                    fronta.Enqueue(i);
            }

            List<int> vysledek = new List<int>();

            while (fronta.Count>0)
            {
                int vrchol = fronta.Dequeue();
                vysledek.Add(vrchol);

                foreach (int soused in graf[vrchol])
                {
                    pocetPredchudcu[soused] --;

                    if (pocetPredchudcu[soused] == 0)
                        fronta.Enqueue(soused);
                }
            }

            int pocetZnaku = 0;
            for (int i = 0; i < MAX_POCET_CHAR; i++)
                if (existuje[i])
                    pocetZnaku++;

            //vypiseme odpoved
            if (vysledek.Count != pocetZnaku)
            {
                Console.WriteLine("nelze, protoze obsahuje cyklus");
            }
            else
            {
                for (int i = 0; i < vysledek.Count; i++)
                {
                    Console.Write((char)(vysledek[i] + PISMENO_A));

                    if (i < vysledek.Count - 1)
                        Console.Write(" -> ");
                }
            }
        }
    }
}

/*Bude uspořádání abecedy vždy jednoznačné?
Ne, muze byt nedostatek informace z vstupu, jako napriklad v Př.5: -vstup: "xyz xz" ze zadani.
Vime, ze "y -> z" diky usporadani, ale nemuze nic rict o "x", protoze v obojich usecich je prvni.
Zpravna mozna poradi:
1. x -> y -> z
2. y -> x -> z
3. y -> z -> x */

/*Uveďte příklad vstupu, pro který takové uspořádání ani nexistuje.
Usporadani neexistuje, pokud pokud vznikne v grafu cyklus.
Napriklad: "xy xz zx yx"
y -> z
x -> z
z -> y => cyklus

Nebo pro prazdny vstup usporadani nema smysl*/



