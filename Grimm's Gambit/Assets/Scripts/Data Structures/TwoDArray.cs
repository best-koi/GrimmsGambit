using System;
using UnityEngine;

[Serializable]
public class TwoDArrayRow
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
    [SerializeField] public TwoDArrayRow[] Arr;
    private GameObject temp; 

    public TwoDArray (int rows, int cols)
    {
        Arr = new TwoDArrayRow[rows];

        for (int i = 0; i < rows; i++)
        {
            Arr[i] = new TwoDArrayRow(cols);
        }
    }

    public GameObject GetValue(int row, int col)
    {
        return Arr[row].row[col];
    }
}
