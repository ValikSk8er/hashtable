using GenericHashTable;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var hashTable = new VHashTable();

            while (true)
            {
                Console.Write("enter a key: ");
                var key = Console.ReadLine();
                if(key == "q")
                {
                    break;
                }
                var hash = hashTable.GetHash(key);
                Console.WriteLine($"hash: {hash}");
                Console.WriteLine($"index: {hashTable.GetIndex(key)}");
                PrintLine();
                hashTable.Add(key, hash);
                
                hashTable.ShowHashTable();
                PrintLine();
                
                Console.WriteLine($"Get value by key '{key}': {hashTable.GetValue(key)}");
                hashTable.ShowCharacteristics();
                PrintLine();


            }
            
        }

        private static void PrintLine()
        {
            Console.WriteLine(new string('-', 16));
        }
    }
}
