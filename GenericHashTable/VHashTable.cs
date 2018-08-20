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
            public TKey key;
            public TValue value;
        }

        public VHashTable()
        {
            _hashTable = new LinkedList<Bucket>[_size];
        }

        public void Add(TKey key, TValue value)
        {
            EmptyKeyVerification(key);

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

            if (currentBucket.key == null || currentBucket.key.Equals(0))
            {
                _hashTable[index].AddFirst(new Bucket { key = key, value = value });
                _capacity++;
            }
            else
            {
                throw new ArgumentException($"key '{key}' is exist");
            }
        }

        public TValue GetValueByKey(TKey key)
        {
            EmptyKeyVerification(key);
            var index = GetIndex(key);

            if(_hashTable[index] != null)
            {
                return GetBucketByKey(_hashTable[index], key).value;
            }

            throw new NullReferenceException($"Key '{key}' is absent");
        }

        public void UpdateValueByKey(TKey key, TValue value)
        {
            EmptyKeyVerification(key);
            var index = GetIndex(key);
            if (_hashTable[index] != null)
            {
                var currentBucket = GetBucketByKey(_hashTable[index], key);
                if (currentBucket.key != null && !currentBucket.value.Equals(value))
                {
                    RemoveValueByKey(key);
                    _hashTable[index].AddFirst(new Bucket { key = key, value = value });
                    _tableSizeChangesCounter++;
                }
                else
                {
                    KeyIsNotExistException(key);
                }
            }
            else
            {
                KeyIsNotExistException(key);
            }
        }

        public void RemoveValueByKey(TKey key)
        {
            EmptyKeyVerification(key);
            var index = GetIndex(key);

            if (_hashTable[index] != null)
            {
                var currentBucket = GetBucketByKey(_hashTable[index], key);
                _hashTable[index].Remove(currentBucket);
                _tableSizeChangesCounter--;
            }
            else
            {
                KeyIsNotExistException(key);
            }
        }

        public void ShowHashTable()
        {
            for (int i = 0; i < _size; i++)
            {
                Console.WriteLine($"Bucket with index[{i}]");
                if(_hashTable[i] == null || _hashTable[i].Count == 0)
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
            Console.WriteLine(new string('-', 16));
        }

        private int GetHash(TKey key)
        {
            return Math.Abs(key.GetHashCode());
        }

        private int GetIndex(TKey key)
        {
            return GetHash(key) % _size;
        }

        private static void EmptyKeyVerification(TKey key)
        {
            if (key.Equals(0) || key == null)
            {
                throw new ArgumentNullException("key is empty");
            }
        }

        private static void KeyIsNotExistException(TKey key)
        {
            throw new NullReferenceException($"Key '{key}' is not exist");
        }

        private Bucket GetBucketByKey(LinkedList<Bucket> bucketsList, TKey key)
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
                _size = (int)(_size + _size * LoadFactor + _size * Math.Sqrt(_tableSizeChangesCounter));
            }
            while (GetCurrentLoadFactor() > LoadFactor);
            
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
