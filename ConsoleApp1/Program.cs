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
                Console.WriteLine($"index: {hashTable.GetIndex(hash)}");
                Console.WriteLine(new string('-', 16));

                hashTable.Add(key, hash);
                hashTable.ShowLoadFactor();
                hashTable.ShowHashTable();
                Console.WriteLine(new string('-', 16));
            }
            
        }
    }
}
