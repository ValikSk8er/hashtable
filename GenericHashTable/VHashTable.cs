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
        public const float LoadFactor = 0.72F;
        

        public VHashTable()
        {
            _hashTable = new LinkedList<Bucket>[_size];
        }

        //public void Add(TKey key, TValue value)
        public void Add(object key, object value)
        {
            float _currentLoadFactor = (float)_capacity / _size;

            if ((string)key == string.Empty)
            {
                throw new NullReferenceException("key is absent");
            }

            if (_currentLoadFactor > LoadFactor)
            {
                CreateBigestHashTable();
            }

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
            var currentIndex = GetIndex(key);

            if(_hashTable[currentIndex] != null)
            {
                return GetBucketValue(_hashTable[currentIndex], key);
            }
            else
            {
                return null;
            }
        }

        public int GetIndex(object key)
        {
            return GetHash(key) % _size;
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
                }                
            }
        }

        public void ShowLoadFactor()
        {
            Console.WriteLine($"Load factor is: {(float)_capacity / _size}");
            Console.WriteLine($"Capacity is: {_capacity}");
        }

        private bool ContainsKey(LinkedList<Bucket> bucketsList, object key)
        {
            return GetBucketValue(bucketsList, key) != null;
        }

        private object GetBucketValue(LinkedList<Bucket> bucketsList, object key)
        {
            return bucketsList.FirstOrDefault(_ => _.key.Equals(key)).value;
        }

        private void CreateBigestHashTable()
        {
            do
            {
                _size = (int)(_size + _size * LoadFactor);
            }
            while (_capacity / _size > LoadFactor);
            
            var biggestHashTable = new LinkedList<Bucket>[_size];

            foreach(var buckets in _hashTable)
            {
                if (buckets != null)
                {
                    foreach(var bucket in buckets)
                    {
                        var key = bucket.key;
                        var value = bucket.value;
                        var index = GetIndex(key);

                        if (biggestHashTable[index] == null)
                        {
                            biggestHashTable[index] = new LinkedList<Bucket>();
                        }
                        if (!ContainsKey(biggestHashTable[index], key))
                        {
                            biggestHashTable[index].AddFirst(new Bucket { key = key, value = value });
                        }
                    }
                }                
            }
            _hashTable = biggestHashTable;
        }
    }
}
