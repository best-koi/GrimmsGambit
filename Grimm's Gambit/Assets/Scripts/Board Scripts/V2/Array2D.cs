using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public class Array2DRow
{
    [SerializeField] private Object[] _row;

    public Object[] Row { get => _row; }

    internal Array2DRow (int cols)
    {
        _row = new GameObject[cols];
    }
}

[System.Serializable]
public class Array2D
{
    [SerializeField] public Array2DRow[] _baseArray;

    public Array2DRow[] BaseArray { get => _baseArray; }

    public Array2D (int rows, int cols)
    {
        _baseArray = new Array2DRow[rows];

        for (int i = 0; i < rows; i++)
            _baseArray[i] = new Array2DRow(cols);
    }

    public Object GetValue(int row, int col)
    {
        return _baseArray[row].Row[col];
    }

    public void SetValue(int row, int col, Object value)
    {
        _baseArray[row].Row[col] = value;
    }
}

#if UNITY_EDITOR
[CanEditMultipleObjects]
[CustomPropertyDrawer(typeof(Array2DRow))]
public class Array2DRowDrawer : PropertyDrawer
{
    private const string _rowPropertyName = "_row";

    // Draw the property inside the given rect
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        SerializedProperty assets = property.FindPropertyRelative(_rowPropertyName); //use this instead of property from now on
        EditorGUI.BeginProperty(position, new GUIContent(property.displayName), assets); //display using name of the "wrapper" object
        //Rect contentPosition = EditorGUI.PrefixLabel(position, label);

        EditorGUI.PropertyField(position, assets, new GUIContent("Row"), true); //and remove the wrapped property's name here
        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        SerializedProperty assets = property.FindPropertyRelative(_rowPropertyName);
        float baseHeight = base.GetPropertyHeight(assets, label);
        return assets.isExpanded ? baseHeight * (assets.arraySize + 2) + 2 * (assets.arraySize + 18) : baseHeight;
    }
}

[CanEditMultipleObjects]
[CustomPropertyDrawer(typeof(Array2D))]
public class Array2DDrawer : PropertyDrawer
{
    private const string _rowPropertyName = "_baseArray";

    // Draw the property inside the given rect
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        SerializedProperty assets = property.FindPropertyRelative(_rowPropertyName); //use this instead of property from now on
        EditorGUI.BeginProperty(position, new GUIContent(property.displayName), assets); //display using name of the "wrapper" object
        //Rect contentPosition = EditorGUI.PrefixLabel(position, label);

        EditorGUI.PropertyField(position, assets, new GUIContent("Base Array"), true); //and remove the wrapped property's name here
        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        SerializedProperty assets = property.FindPropertyRelative(_rowPropertyName);
        float baseHeight = base.GetPropertyHeight(assets, label);

        if (!assets.isExpanded)
            return baseHeight;
        else
        {
            float finalHeight = baseHeight * 3;
            int arraySize = assets.arraySize;

            for (int i = 0; i < arraySize; i++)
            {
                SerializedProperty row = assets.GetArrayElementAtIndex(i).FindPropertyRelative("_row");
                finalHeight += row.isExpanded ? baseHeight * (row.arraySize + 2) + 2 * (row.arraySize + 18) : baseHeight;
            }

            return finalHeight;
        }
    }
}
#endif
