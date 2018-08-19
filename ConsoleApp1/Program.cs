using GenericHashTable;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var hashTable = new VHashTable<int, string>();

            while (true)
            {
                Console.Write("enter a key: ");
                var key = int.Parse(Console.ReadLine());

                if (key == -1)
                {
                    break;
                }

                Console.Write("enter a value: ");
                var value = Console.ReadLine();

                hashTable.Add(key, value);
            }
            PrintLine();
            hashTable.ShowHashTable();
            PrintLine();
            while (true)
            {
                Console.Write("Get value by key: ");
                var key = int.Parse(Console.ReadLine());
                if (key == -1)
                {
                    break;
                }
                Console.WriteLine($"{hashTable.GetValueByKey(key)}");
            }
            Console.ReadLine();
        }

        private static void PrintLine()
        {
            Console.WriteLine(new string('-', 16));
        }
    }
}
