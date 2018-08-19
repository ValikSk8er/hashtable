namespace GenericHashTable
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class VHashTable//<TKey, TValue>
    {
        private struct Bucket
        {
            public object key;
            public object value;
        }

        private LinkedList<Bucket>[] _hashTable;
        private int _size = 8;
        private int _capacity = 0;
        public const float LoadFactorValue = 0.72F;

        public VHashTable()
        {
            _hashTable = new LinkedList<Bucket>[_size];
        }

        //public void Add(TKey key, TValue value)
        public void Add(object key, object value)
        {
            double _loadFactor = (float)_capacity / _size;

            if (key == null)
            {
                throw new NullReferenceException("key is absent");
            }

            //if(_loadFactor > LoadFactorValue)
            //{
            //    _size = _size + 2;
            //    CreateBigestHashTable();                
            //}

            var hash = GetHash(key);
            var index = GetIndex(hash);
            if (_hashTable[index] == null)
            {
                _hashTable[index] = new LinkedList<Bucket>();
            }
            if (!ContainsKey(_hashTable[index], key))
            {
                _hashTable[index].AddFirst(new Bucket { key = key, value = value });
                _capacity++;
            }
        }

        public object GetValue(object key)
        {
            throw new NotImplementedException();
        }

        private bool ContainsKey(LinkedList<Bucket> bucketsList, object key)
        {
            return bucketsList.FirstOrDefault(_ => _.key.Equals(key)).key != null;
        }

        private void CreateBigestHashTable()
        {
            throw new NotImplementedException();
        }

        public int GetIndex(int hash)
        {
            return hash % _size;
        }

        public int GetHash(object key)
        {
            return Math.Abs(key.GetHashCode());
        }

        public void ShowHashTable()
        {
            for (int i = 0; i < _size; i++)
            {
                Console.WriteLine($"Bucket with index[{i}]");
                if(_hashTable[i] == null)
                {
                    Console.WriteLine("buket is empty");
                }
                else
                {
                    foreach (var element in _hashTable[i])
                    {
                        Console.WriteLine($"key: {element.key}, value: {element.value}");
                    }
                    Console.WriteLine();
                }                
            }
        }

        public void ShowLoadFactor()
        {
            Console.WriteLine($"Load factor is: {(float)_capacity / _size}");
        }
    }
}
