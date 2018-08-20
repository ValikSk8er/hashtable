using GenericHashTable;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var hashTable = new VHashTable<int, string>();

            int key = 5023423;

            hashTable.Add(key, "one");

            hashTable.ShowHashTable();

            hashTable.RemoveValueByKey(key);

            hashTable.ShowHashTable();

            hashTable.Add(key, "two");
            

            hashTable.ShowHashTable();

            hashTable.UpdateValueByKey(key, "three");

            hashTable.ShowHashTable();

            Console.WriteLine(hashTable.GetValueByKey(key));

            Console.ReadKey();
        }

        private static void PrintLine()
        {
            Console.WriteLine(new string('-', 16));
        }
    }
}
