using System;
using UnityEngine;

[Serializable]
public class TwoDArray<T>
{
    [SerializeField]
    private int rows;
    [SerializeField]
    private int columns;
    [SerializeField]
    private T[] data;

    public int Rows => rows;
    public int Columns => columns;

    public TwoDArray(int rows, int columns)
    {
        this.rows = rows;
        this.columns = columns;
        data = new T[rows * columns];
    }

    public T this[int row, int column]
    {
        get => data[row * columns + column];
        set => data[row * columns + column] = value;
    }

    public void Resize(int newRows, int newColumns)
    {
        var newData = new T[newRows * newColumns];
        for (int i = 0; i < Math.Min(rows, newRows); i++)
        {
            for (int j = 0; j < Math.Min(columns, newColumns); j++)
            {
                newData[i * newColumns + j] = this[i, j];
            }
        }

        rows = newRows;
        columns = newColumns;
        data = newData;
    }
}
