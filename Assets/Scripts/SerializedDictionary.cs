using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerializedDictionary<T1, T2>
{
    [SerializeField] DictionaryItem[] items;
    public Dictionary<T1, T2> ToDictionary()
    {
        Dictionary<T1, T2> dict = new Dictionary<T1, T2>();
        foreach (DictionaryItem kv in items)
        {
            dict.Add(kv.Key, kv.Value);
        }
        return dict;
    }
    [Serializable]
    private class DictionaryItem
    {
        [SerializeField] public T1 Key;
        [SerializeField] public T2 Value;
    }
}
