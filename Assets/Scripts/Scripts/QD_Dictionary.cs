using System.Collections.Generic;
using UnityEngine;

namespace QuantumTek.QuantumDialogue
{
    [System.Serializable]
    public class QD_IntList
    {
        public List<int> Array;

        public QD_IntList(List<int> array)
        { Array = array; }
    }

    [System.Serializable]
    public class QD_KnobDictionary : ISerializationCallbackReceiver
    {
        public List<int> keys = new List<int> { };
        public List<QD_IntList> values = new List<QD_IntList> { };

        public Dictionary<int, List<int>> dictionary = new Dictionary<int, List<int>>();

        public void OnBeforeSerialize()
        {
            keys.Clear();
            values.Clear();

            foreach (var kvp in dictionary)
            {
                keys.Add(kvp.Key);
                values.Add(new QD_IntList(kvp.Value));
            }
        }

        public void OnAfterDeserialize()
        {
            dictionary = new Dictionary<int, List<int>>();

            for (int i = 0; i != Mathf.Min(keys.Count, values.Count); i++)
                dictionary.Add(keys[i], values[i].Array);
        }

        /// <summary>
        /// Returns the value attached to the given key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<int> Get(int key) => dictionary[key];
        public int Count => dictionary.Count;
        public bool ContainsKey(int key) => dictionary.ContainsKey(key);
        public void Add(int key, List<int> value) => dictionary.Add(key, value);
        public void Remove(int key) => dictionary.Remove(key);
    }
}