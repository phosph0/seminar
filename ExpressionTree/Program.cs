using System.Text;

namespace ExpressionTree
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string expression = "65 3 5 * - 2 3 + /";
            string[] exp = expression.Split();

            Stack<Uzel> uzly = new Stack<Uzel>();

            try
            {
                foreach (string znak in exp)
                {
                    if (double.TryParse(znak, out double value))
                    {
                        uzly.Push(new Uzel(value));
                    }
                    else
                    {
                        if (uzly.Count < 2)
                            throw new Exception("chybny vyraz");

                        Uzel pravy = uzly.Pop();
                        Uzel levy = uzly.Pop();

                        uzly.Push(new Uzel(znak, levy, pravy));
                    }
                }

                if (uzly.Count != 1)
                    throw new Exception("chybny vyraz");

                Uzel koren = uzly.Pop();

                StringBuilder sb = new StringBuilder();

                koren.Prefix(sb);
                Console.WriteLine("prefix: " + sb);

                sb.Clear();
                koren.Postfix(sb);
                Console.WriteLine("postfix: " + sb);

                sb.Clear();
                koren.Infix(sb);
                Console.WriteLine("infix: " + sb);

                Console.WriteLine("vysledek: " + koren.Vyhodnot());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }

    class Uzel
    {
        public Uzel LevySyn { get; set; }
        public Uzel PravySyn { get; set; }
        public double Cislo { get; set; }
        public string Operator { get; set; }

        public Uzel(double cislo)
        {
            Cislo = cislo;
        }

        public Uzel(string op, Uzel levy, Uzel pravy)
        {
            Operator = op;
            LevySyn = levy;
            PravySyn = pravy;
        }

        public void Prefix(StringBuilder sb)
        {
            if (LevySyn == null)
                sb.Append(Cislo + " ");
            else
                sb.Append(Operator + " ");

            if (LevySyn != null)
                LevySyn.Prefix(sb);

            if (PravySyn != null)
                PravySyn.Prefix(sb);
        }

        public void Postfix(StringBuilder sb)
        {
            if (LevySyn != null)
                LevySyn.Postfix(sb);

            if (PravySyn != null)
                PravySyn.Postfix(sb);

            if (LevySyn == null)
                sb.Append(Cislo + " ");
            else
                sb.Append(Operator + " ");
        }

        public void Infix(StringBuilder sb)
        {
            if (LevySyn != null)
                sb.Append("( ");

            if (LevySyn != null)
                LevySyn.Infix(sb);

            if (LevySyn == null)
                sb.Append(Cislo + " ");
            else
                sb.Append(Operator + " ");

            if (PravySyn != null)
                PravySyn.Infix(sb);

            if (PravySyn != null)
                sb.Append(") ");
        }

        public double Vyhodnot()
        {
            if (LevySyn == null)
                return Cislo;

            double levy = LevySyn.Vyhodnot();
            double pravy = PravySyn.Vyhodnot();

            if (Operator == "+")
                return levy + pravy;
            else if (Operator == "-")
                return levy - pravy;
            else if (Operator == "*")
                return levy * pravy;
            else if (Operator == "/")
                return levy / pravy;
            else
                throw new Exception("chybny operator");
        }
    }
}