using System;
using UnityEngine;

[Serializable]
public class GenericPair<T, T2>
{
   [SerializeField] internal T key;
   [SerializeField] private T2 value;

    internal T2 GetVal(T key)
    {
        if (!key.Equals(this.key)) return default(T2); 

        return value;
    }

    internal void SetVal(T2 value)
    {
        this.value = value;
    }
}

[Serializable]
public class GenericDict<T, T2>
{
    [SerializeField] private GenericPair<T, T2>[] DictArray;

    public GenericDict(int size) 
    {
        DictArray = new GenericPair<T, T2>[size];

        for (int i = 0; i < size; i++)
        {
            DictArray[i] = new GenericPair<T, T2>();
        }
    }

    public T GetKey(int i = 0)
    {
        return DictArray[i].key;
    }

    public T2 GetValue(T key)
    {
        foreach (var pair in DictArray)
        {
            T2 value = pair.GetVal(key);
            if (!value.Equals(default(T2))) return value;
        }
        return default(T2); 
    }

    public void SetKey(T key, int i = 0)
    {
        DictArray[i].key = key;
    }

    public bool SetValue(T key, T2 value)
    {
        foreach (var pair in DictArray)
        {
            if (key.Equals(pair.key))
            {
                pair.SetVal(value);
                return true;
            }
        }
        return false; 
    }

}
