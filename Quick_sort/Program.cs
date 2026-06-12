
namespace Quick_sort
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<int> list = new List<int>();

            Console.WriteLine("zzadej cisla:");

            string[] vstup = Console.ReadLine().Split(' ');

            foreach (string cislo in vstup)
            {
                list.Add(int.Parse(cislo));
            }

            List<int> serazene = QuickSort(list);

            Console.WriteLine("serazeny:");
            Console.WriteLine(string.Join(" ", serazene));
        }


        static List<int> QuickSort(List<int> list)
        {
            if (list.Count <= 1)
            {
                return list;
            }

            int pivot = list[0];

            List<int> levy = new List<int>();
            List<int> pravy = new List<int>();
            List<int> rovnyPivotu = new List<int>();

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] < pivot)
                {
                    levy.Add(list[i]);
                }
                else if (list[i] == pivot)
                {
                    rovnyPivotu.Add(list[i]);
                }
                else
                {
                    pravy.Add(list[i]);
                }
            }

            List<int> sorted = new List<int>();

            sorted.AddRange(QuickSort(levy));
            sorted.AddRange(rovnyPivotu);
            sorted.AddRange(QuickSort(pravy));

            return sorted;
        }
    }
}
//4. bonus Pro jakou vstupní posloupnost se bude výrazně lišit její medián a hodnota nejbližší průměru?: 
// všechny posloupnosti s extrémně odlišujímíci se hodnotamy od většiny, např. 1, 2, 3, 4, 999999