namespace GenericHashTable
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Custom Valik's HashTable
    /// </summary>
    /// <typeparam name="TKey">HashTable key</typeparam>
    /// <typeparam name="TValue">HashTable value</typeparam>
    public class VHashTable<TKey, TValue>
    {      
        private LinkedList<Bucket>[] _hashTable;
        private int _size = 8;
        private int _capacity = 0;
        private const float LoadFactor = 0.72F;
        private int _tableSizeChangesCounter = 0;

        private struct Bucket
        {
            public object key;
            public object value;
        }

        public VHashTable()
        {
            _hashTable = new LinkedList<Bucket>[_size];
        }

        public void Add(TKey key, TValue value)
        {
            if (key.Equals(string.Empty))
            {
                throw new NullReferenceException("key is absent");
            }

            if (GetCurrentLoadFactor() > LoadFactor)
            {
                CreateBigestHashTable();
                _tableSizeChangesCounter++;
            }

            var index = GetIndex(key);
            if (_hashTable[index] == null)
            {
                _hashTable[index] = new LinkedList<Bucket>();
            }
            var currentBucket = GetBucketByKey(_hashTable[index], key);
            if (currentBucket.key == null)
            {
                _hashTable[index].AddFirst(new Bucket { key = key, value = value });
                _capacity++;
            }
            else
            {
                if(currentBucket.key.Equals(key) && !currentBucket.value.Equals(value))
                {
                    RemoveValueByKey(key);
                    _hashTable[index].AddFirst(new Bucket { key = key, value = value });
                }
            }
        }

        public TValue GetValueByKey(TKey key)
        {
            var currentIndex = GetIndex(key);

            if(_hashTable[currentIndex] != null)
            {
                return (TValue)GetBucketByKey(_hashTable[currentIndex], key).value;
            }
            else
            {
                throw new NullReferenceException($"Key '{key}' is absent");
            }
        }

        public void RemoveValueByKey(TKey key)
        {
            var index = GetIndex(key);
            var currentBucket = GetBucketByKey(_hashTable[index], key);
            _hashTable[index].Remove(currentBucket);
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

        public int GetHash(TKey key)
        {
            return Math.Abs(key.GetHashCode());
        }

        public int GetIndex(TKey key)
        {
            return GetHash(key) % _size;
        }


        private Bucket GetBucketByKey(LinkedList<Bucket> bucketsList, object key)
        {
            return bucketsList.FirstOrDefault(_ => _.key.Equals(key));
        }

        private float GetCurrentLoadFactor()
        {
            return (float)_capacity / _size;
        }

        private void CreateBigestHashTable()
        {
            do
            {
                _size = (int)(_size + (_size * LoadFactor) + (_size * Math.Sqrt(_tableSizeChangesCounter)));
            }
            while (GetCurrentLoadFactor() > LoadFactor);
            
            var biggestHashTable = new LinkedList<Bucket>[_size];

            foreach(var buckets in _hashTable)
            {
                if (buckets != null)
                {
                    foreach(var bucket in buckets)
                    {
                        var key = (TKey)bucket.key;
                        var value = bucket.value;
                        var index = GetIndex(key);

                        if (biggestHashTable[index] == null)
                        {
                            biggestHashTable[index] = new LinkedList<Bucket>();
                        }
                        if (GetBucketByKey(biggestHashTable[index], key).key == null)
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
