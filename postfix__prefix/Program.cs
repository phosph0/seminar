namespace postfix__prefix
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("zadej typ (po/pre): ");
            string funkce = Console.ReadLine();

            Console.Write("zadej priklad: ");
            string[] vstup = Console.ReadLine().Split();

            try
            {
                if (funkce == "po")
                {
                    Console.WriteLine("vysledek: " + Postfix(vstup));
                }
                else if (funkce == "pre")
                {
                    Console.WriteLine("vysledek: " + Prefix(vstup));
                }
                else
                {
                    Console.WriteLine("chyba, zadejte po (postfix), nebo pre (prefix)");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static double Postfix(string[] vstup)
        {
            Stack<double> s = new Stack<double>();
            Stack<string> vyrazy = new Stack<string>();

            for (int i = 0; i < vstup.Length; i++)
            {
                if (double.TryParse(vstup[i], out double cislo))
                {
                    s.Push(cislo);
                    vyrazy.Push(vstup[i]);
                }
                else
                {
                    if (s.Count < 2)
                        throw new Exception("neplatny priklad: chybi operandy");

                    double cislo1 = s.Pop();
                    double cislo2 = s.Pop();

                    string vyraz1 = vyrazy.Pop();
                    string vyraz2 = vyrazy.Pop();

                    if (vstup[i] == "+")
                        s.Push(cislo1 + cislo2);
                    else if (vstup[i] == "-")
                        s.Push(cislo2 - cislo1);
                    else if (vstup[i] == "*")
                        s.Push(cislo1 * cislo2);
                    else if (vstup[i] == "/")
                    {
                        if (cislo1 == 0)
                            throw new Exception("delis nulou!!");

                        s.Push(cislo2 / cislo1);
                    }
                    else
                    {
                        throw new Exception("neplatny operator");
                    }

                    vyrazy.Push("(" + vyraz2 + " " + vstup[i] + " " + vyraz1 + ")");
                }
            }

            if (s.Count != 1)
                throw new Exception("neplatny priklad: chybi operatory");

            Console.WriteLine("infix: " + vyrazy.Peek());

            return s.Pop();
        }

        static double Prefix(string[] vstup)
        {
            Stack<double> s = new Stack<double>();
            Stack<string> vyrazy = new Stack<string>();

            for (int i = vstup.Length - 1; i >= 0; i--)
            {
                if (double.TryParse(vstup[i], out double cislo))
                {
                    s.Push(cislo);
                    vyrazy.Push(vstup[i]);
                }
                else
                {
                    if (s.Count < 2)
                        throw new Exception("neplatny priklad: chybi operandy");

                    double cislo1 = s.Pop();
                    double cislo2 = s.Pop();

                    string vyraz1 = vyrazy.Pop();
                    string vyraz2 = vyrazy.Pop();

                    if (vstup[i] == "+")
                        s.Push(cislo1 + cislo2);
                    else if (vstup[i] == "-")
                        s.Push(cislo1 - cislo2);
                    else if (vstup[i] == "*")
                        s.Push(cislo1 * cislo2);
                    else if (vstup[i] == "/")
                    {
                        if (cislo2 == 0)
                            throw new Exception("delis nulou!!");

                        s.Push(cislo1 / cislo2);
                    }
                    else
                    {
                        throw new Exception("neplatny operator");
                    }

                    vyrazy.Push("(" + vyraz1 + " " + vstup[i] + " " + vyraz2 + ")");
                }
            }

            if (s.Count != 1)
                throw new Exception("neplatny priklad: chybi operatory");

            Console.WriteLine("infix: " + vyrazy.Peek());

            return s.Pop();
        }
    }
}