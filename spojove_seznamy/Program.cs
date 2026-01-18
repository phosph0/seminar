
using System.ComponentModel;

namespace Spojak
{
    internal class Program
    {
        static void Main(string[] args)
        {
            LinkedList spojak = new LinkedList();
            Console.Write("prvni list: ");
            spojak.AddToEnd(3);
            spojak.AddToEnd(4);
            spojak.AddToEnd(5);
            spojak.AddToEnd(4);
            spojak.AddToEnd(6);
            spojak.Print();

            LinkedList spojak2 = new LinkedList();
            Console.Write("druhy list: ");
            spojak2.AddToEnd(0);
            spojak2.AddToEnd(-5);
            spojak2.AddToEnd(1);
            spojak2.AddToEnd(5);
            spojak2.AddToEnd(3);
            spojak2.AddToEnd(1);
            spojak2.Print();

            //testovani
            Console.WriteLine("maximalni hodnota v listu 1: " +spojak.FindMax());

            spojak.RemoveFromEnd();
            Console.Write("list 1 bez posledniho prvku: ");
            spojak.Print();

            int hledaneCislo = 4;
            Console.WriteLine($"je v listu 1 cislo {hledaneCislo}?: {spojak.Exists(hledaneCislo)}");

            spojak.RemoveAll(hledaneCislo);
            Console.Write($"list 1 bez {hledaneCislo}: ");
            spojak.Print();

            Console.Write("list 2 bez duplikatu: ");
            spojak2.RemoveDuplicate();
            spojak2.Print();

            Console.Write("sjednoceni listu a ulozeni do listu 2: ");
            Union(spojak2, spojak);
            spojak2.Print();

            Console.Write("prunik listu: ");
            Intersection(spojak , spojak2);
            spojak.Print();

            //testovani bonusu
            LinkedList cislo1 = new LinkedList();
            cislo1.AddToEnd(5);
            cislo1.AddToEnd(6);
            cislo1.AddToEnd(7);
            cislo1.AddToEnd(8);
            cislo1.AddToEnd(9);  // 98765 ulozene pozpatku

            LinkedList cislo2 = new LinkedList();
            cislo2.AddToEnd(1);
            cislo2.AddToEnd(2);
            cislo2.AddToEnd(3);
            cislo2.AddToEnd(4);  // 4321 ulozene pozpatku

            LinkedList vysledek = AddLongNumbers(cislo1, cislo2);

            Console.WriteLine("\nbonus");
            Console.Write("prvni cislo: ");
            PrintNumber(cislo1);

            Console.Write("druhy cislo: ");
            PrintNumber(cislo2);

            Console.Write("vysledek scitani: ");
            PrintNumber(vysledek);

        }

        //Destruktivní průnik dvou seznam (funkce se provadi do listu 1)
        //projde list1, pouziva Exists na list2 a RemoveDuplicate - O(n*m + n^2)
        static void Intersection(LinkedList list1, LinkedList list2)
        {
            Node node1 = list1.Head;
            Node predeslyNode = null;

            while (node1 != null)
            {
                // projdeme prvni seznam a odstranime prvky, ktere nejsou v druhem seznamu
                if (!list2.Exists(node1.Value))
                {
                    if (predeslyNode == null)
                    {
                        list1.Head = node1.Next;
                        node1 = list1.Head;
                    }
                    else
                    {
                        predeslyNode.Next = node1.Next;
                        node1 = predeslyNode.Next;
                    }
                }
                else
                {
                    predeslyNode = node1;
                    node1 = node1.Next;
                }
            }

            list1.RemoveDuplicate();
        }

        // Destruktivní sjednocení (funkce se provadi do listu 1)
        //projde list2 a pouziva Exists na list1 → O(n*m)
        static void Union(LinkedList list1, LinkedList list2)
        {
            Node node2 = list2.Head;

            while (node2 != null)
            {
                if (!list1.Exists(node2.Value))
                {
                    list1.AddToEnd(node2.Value);
                }
                node2 = node2.Next;
            }
        }

        // scita dve dlouha cisla ulozena v seznamech (kazda cifra je jeden uzel)
        // poradek listu: 1. jednotky, 2. desitky, 3. stovky, atd.
        //projde vsechny cifry obou cisel - O(n), nebo O(m) podle toho co je delsi
        static LinkedList AddLongNumbers(LinkedList cislo1, LinkedList cislo2)
        {
            Node node1 = cislo1.Head;
            Node node2 = cislo2.Head;

            LinkedList vysledek = new LinkedList();
            int prenos = 0;

            // dokud jsou cisla nebo prenos
            while (node1 != null || node2 != null || prenos != 0)
            {
                int soucet = prenos;

                if (node1 != null)
                {
                    soucet += node1.Value;
                    node1 = node1.Next;
                }

                if (node2 != null)
                {
                    soucet += node2.Value;
                    node2 = node2.Next;
                }

                vysledek.AddToEnd(soucet % 10);  // ulozime cifru
                prenos = soucet / 10;            // preneseni
            }

            return vysledek;
        }

        // vypise cislo pozpatku
        //projde vsechny node - O(n)
        static void PrintNumber(LinkedList cislo)
        {
            List<int> seznam = new List<int>();
            Node node = cislo.Head;

            // ulozime cislo do listu
            while (node != null)
            {
                seznam.Add(node.Value);
                node = node.Next;
            }

            // vypis zleva doprava
            for (int i = seznam.Count - 1; i >= 0; i--)
            {
                Console.Write(seznam[i]);
            }
            Console.WriteLine();
        }


    }
    class Node
    {
        // konstruktor
        public Node(int value)
        {
            Value = value;
            Next = null;
        }
        public int Value { get; set; }
        public Node Next { get; set; }
    }

    class LinkedList
    {
        public Node Head { get; set; }

        //projde list na konec - O(n)
        public void AddToEnd(int value)
        {
            if (Head == null)
            {
                Head = new Node(value);
            }
            else
            {
                Node currentNode = Head;
                while (currentNode.Next != null)
                {
                    currentNode = currentNode.Next;
                }
                currentNode.Next = new Node(value);
            }
        }

        //projde vsechny node - O(n)
        public void Print()
        {
            Node node = Head;
            while (node!=null)
            {
                Console.Write(node.Value + " ");
                node = node.Next;
            }
            Console.WriteLine();

        }

        // TODO: Najít maximum
        // projde vsechny node - O(n)
        public int? FindMax()
        // int s otazníkem znamená nullovatelný int - může obsahovat číslo i null 
        {
            if (Head == null)
            {
                Console.WriteLine("Tento seznam je przádný");
                return null; // nullem naznačíme, že maximum nebylo nalezeno
            }
            else
            {
                Node node = Head;
                int x = node.Value;
                while (node != null)
                {
                    if (node.Value > x)
                    {
                        x = node.Value;
                    }
                    node = node.Next;
                }
                return x;
            }

        }

        // TODO: odebrat prvek z konce
        //projde vsechny node - O(n)
        public void RemoveFromEnd()
        {
            if (Head == null)
            {
                Console.WriteLine("tento seznam je prazdny");
                return;
            }

            if (Head.Next == null)
            {
                Head = null;
                return;
            }

            Node node = Head;
            while (node.Next.Next != null)
            {
                node = node.Next;
            }

            node.Next = null;
        }

        // TODO: najít prvek a vrátit True nebo False, jestli tam je
        //projde list az do hledane hodnoty - O(n)
        public bool Exists(int value)
        {
            Node currentNode = Head;
            while (currentNode != null)
            {
                if (currentNode.Value == value)
                {
                    return true;
                }
                currentNode = currentNode.Next;
            }
            return false;
        }

        //odebere ze seznamu všechny prvky obsahující hodnotu shodnou s value v parametru
        //projde vsechny node - O(n)
        public void RemoveAll(int value)
        {
            if (!Exists(value))
            {
                Console.WriteLine("hodnota neni v listu");
                return;
            }

            Node node = Head;

            while (node != null && node.Next != null)
            {
                if (node.Next.Value == value)
                {
                    node.Next = node.Next.Next;
                }
                else
                {
                    node = node.Next;
                }
            }

        }

        //pomocna funkce vymazani stejnych hodnot
        //dvojity for cyklus - O(n^2)
        public void RemoveDuplicate()
        {
            Node node = Head;

            while (node != null)
            {
                Node pomocnyNode = node;
                while (pomocnyNode.Next != null)
                {
                    if (pomocnyNode.Next.Value == node.Value)
                    {
                        pomocnyNode.Next = pomocnyNode.Next.Next;
                    }
                    else
                    {
                        pomocnyNode = pomocnyNode.Next;
                    }
                }
                node = node.Next;
            }
        }

    }

}

