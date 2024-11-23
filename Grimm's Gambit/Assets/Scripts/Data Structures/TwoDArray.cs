using System;
using UnityEngine;

[Serializable]
internal class TwoDArrayRow
{
    [SerializeField] internal GameObject[] row;

    internal TwoDArrayRow (int cols)
    {
        row = new GameObject[cols];
    }
}

[Serializable]
public class TwoDArray
{
    [SerializeField] private TwoDArrayRow[] Arr;

    public TwoDArray (int rows, int cols)
    {
        Arr = new TwoDArrayRow[rows];

        for (int i = 0; i < rows; i++)
        {
            Arr[i] = new TwoDArrayRow(cols);
        }
    }
}
