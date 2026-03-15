namespace BVS
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8; //nevypisovaly se mi hacky v terminale

            BinarySearchTree<string> binarySearchTree = new BinarySearchTree<string>();
            binarySearchTree.Insert(4, "a");
            binarySearchTree.Insert(1, "b");
            binarySearchTree.Insert(6, "c");
            binarySearchTree.Insert(3, "d");
            binarySearchTree.Insert(5, "e");
            binarySearchTree.Insert(2, "f");
            Console.ReadLine();

            // odtud by mělo být přístupné jen to nejdůležitější, žádné vnitřní pomocné implementace.
            // Strom a jeho metody mají fungovat jako černá skříňka, která nám nabízí nějaké úkoly a my se nemusíme starat o to, jakým postupem budou splněny.
            // rozhodně také nechceme mít možnost datovou stukturu nějak měnit jinak, než je dovoleno (třeba nějakým jiným způsobem moct přidat nebo odebrat uzly, aniž by platili invarianty struktury)

            BinarySearchTree<Student> tree = new BinarySearchTree<Student>();

            // čteme data z CSV souboru se studenty (soubor je uložen ve složce projektu bin/Debug u exe souboru)
            // CSV je formát, kdy ukládáme jednotlivé hodnoty oddělené čárkou
            // v tomto případě: Id,Jméno,Příjmení,Věk,Třída
            using (StreamReader streamReader = new StreamReader("../../../../studenti_shuffled.csv"))
            {
                string line = streamReader.ReadLine();
                while (line != null)
                {
                    string[] studentData = line.Split(',');

                    Student student = new Student(
                        Convert.ToInt32(studentData[0]),    // Id
                        studentData[1],                     // Jméno
                        studentData[2],                     // Příjmení
                        Convert.ToInt16(studentData[3]),    // Věk
                        studentData[4]);                    // Třída

                    // vložíme studenta do stromu, jako klíč slouží jeho Id
                    tree.Insert(student.Id, student);
                    line = streamReader.ReadLine();
                }
            }
            // Najděte studenta s ID 20 (David Urban (ID: 20) ze třídy 4.A)
            Node<Student> student20 = tree.Search(20);
            Console.WriteLine("student s ID 20: " + student20.Value);

            // Najděte studenta s nejnižším ID (Kateřina Sedláček (ID: 1) ze třídy 1.B)
            Node<Student> min = tree.Min();
            Console.WriteLine("studenta s nejnižším ID: " + min.Value);

            // Vložte vlastního studenta s ID > 100 (je potřeba vytvořit nový objekt typu Student) a zkuste ho pak najít
            Student novyStudent = new Student(150, "Veronika", "Goreva", 66, "99.C");

            tree.Insert(novyStudent.Id, novyStudent);

            Node<Student> ja = tree.Search(150);
            Console.WriteLine("pridany student: " + ja.Value);

            // Smažte všechny studenty se sudým ID
            for (int i = 2; i <= 200; i = i + 2)
            {
                tree.Delete(i);
            }

            // Vypište strom (měli byste vidět jen ID lichá a seřazená)
            tree.Print();


        }
    }

    class BinarySearchTree<T>
    {
        public Node<T> Root;

        public void Insert(int newKey, T newValue)
        {

            void _insert(Node<T> node, int newKey, T newValue)
            {
                if (newKey < node.Key) // jdeme doleva
                    if (node.LeftSon== null)
                        node.LeftSon = new Node<T>(newKey, newValue);
                    else
                        _insert(node.LeftSon, newKey, newValue);
                else if (newKey > node.Key) // jdeme doprava
                    if (node.RightSon == null)
                        node.RightSon = new Node<T>(newKey, newValue);
                    else
                        _insert(node.RightSon, newKey, newValue);
                else // našli jsme náš klíč, což bychom neměli, mají být unikátní.... :/
                    throw new Exception(); // vyhodíme chybu
            }

            if (Root == null) // pokud ještě není definován kořen
                Root = new Node<T>(newKey, newValue);
            else
                _insert(Root, newKey, newValue);
        }
        public Node<T> Search(int key)
        {
            Node<T> _search(Node<T> node, int key)
            {
                if (node == null)
                    return null;

                if (key == node.Key)
                    return node;

                if (key < node.Key)
                    return _search(node.LeftSon, key);

                else
                    return _search(node.RightSon, key);
            }

            return _search(Root, key);
        }


        public Node<T> Min()
        {
            Node<T> _min(Node<T> node)
            {
                if (node.LeftSon == null)
                    return node;
                return _min(node.LeftSon);
            }

            return _min(Root);

        }

        public void Print()
        {
            void _print(Node<T> node)
            {
                if (node == null)
                    return;
                else
                {
                    _print(node.LeftSon);
                    Console.WriteLine($"{node.Key} : {node.Value}");
                    _print(node.RightSon);
                }
            }

            _print(Root);
        }

        public void Delete(int key)
        {
            Node<T> _delete(Node<T> node, int key)
            {
                //hledame dany key
                if (key < node.Key)
                {
                    if (node.LeftSon != null)
                        node.LeftSon = _delete(node.LeftSon, key);
                }

                else if (key > node.Key)
                {
                    if (node.RightSon != null)
                        node.RightSon = _delete(node.RightSon, key);
                }

                //nasli jsme key
                else
                {
                    // zadny syn
                    if (node.LeftSon == null && node.RightSon == null)
                        return null;

                    // 1 syn
                    if (node.LeftSon == null)
                        return node.RightSon;

                    if (node.RightSon == null)
                        return node.LeftSon;

                    // 2 syny
                    Node<T> min = node.RightSon;
                    while (min.LeftSon != null)
                        min = min.LeftSon;

                    node.Key = min.Key;
                    node.Value = min.Value;

                    node.RightSon = _delete(node.RightSon, min.Key);
                }

                return node;
            }

            Root = _delete(Root, key);
        }


    }

    class Node<T> // T může být libovolný typ
    {
        public Node(int key, T value)
        {
            Key = key;
            Value = value;
        }
        public int Key;
        public T Value;

        public Node<T> LeftSon;
        public Node<T> RightSon;



    }

    class Student
    {
        public int Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public int Age { get; }

        public string ClassName { get; }

        public Student(int id, string firstName, string lastName, int age, string className)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            ClassName = className;
        }

        // aby se nám při Console.WriteLine(student) nevypsala jen nějaká adresa v paměti,
        // upravíme výpis objektu typu student na něco čitelného
        public override string ToString()
        {
            return string.Format("{0} {1} (ID: {2}) ze třídy {3}", FirstName, LastName, Id, ClassName);
        }
    }

}
