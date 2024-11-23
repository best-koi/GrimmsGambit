using System;
using UnityEngine;

[Serializable]
public class TwoDArrayRow<T>
{
    [SerializeField] public T[] row;
}

[Serializable]
public class TwoDArray<T>
{
    [SerializeField] private TwoDArrayRow<T>[] rows;
}
