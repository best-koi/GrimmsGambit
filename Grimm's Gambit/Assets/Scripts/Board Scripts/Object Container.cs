using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public abstract class ObjectContainer : MonoBehaviour
{
    #region Private Fields

    protected List<Transform> m_ChildTransforms;

    public int ChildListSize
    {
        get
        {
            return m_ChildTransforms.Count;
        }
    }

    #endregion

    #region MonoBehaviour Callbacks

    protected virtual void Awake()
    {
        m_ChildTransforms = new List<Transform>();
    }

    protected virtual void FixedUpdate()
    {
        if (transform.childCount != m_ChildTransforms.Count)
        {
            UpdateList();
        }

        UpdatePositions();
    }

    #endregion

    #region Public Methods

    public List<Transform> GetAll() 
    { 
        return m_ChildTransforms;
    }

    public Transform GetChild(int index)
    {
        string methodName = nameof(GetChild);

        if (CheckIndex(index, methodName))
            return m_ChildTransforms[index];
        else
            return null;
    }

    public void UpdateList()
    {
        m_ChildTransforms.Clear();
        for (int i = 0; i < transform.childCount; i++)
            m_ChildTransforms.Add(transform.GetChild(i));
    }

    public int IndexOf(Transform obj)
    {
        return m_ChildTransforms.IndexOf(obj);
    }

    public void SwitchIndices(int firstIndex, int secondIndex)
    {
        string methodName = nameof(SwitchIndices);

        if (CheckIndex(firstIndex, methodName) && CheckIndex(secondIndex, methodName))
        {
            Transform first = m_ChildTransforms[firstIndex];
            Transform second = m_ChildTransforms[secondIndex];

            if (CheckReference(first, methodName))
                first.transform.SetSiblingIndex(secondIndex);
            if (CheckReference(second, methodName))
                second.transform.SetSiblingIndex(firstIndex);

            m_ChildTransforms.RemoveAt(firstIndex);
            m_ChildTransforms.Insert(firstIndex, second);
            m_ChildTransforms.RemoveAt(secondIndex);
            m_ChildTransforms.Insert(secondIndex, first);

            UpdatePositions();
        }
    }

    public void SwitchChildren(Transform firstChild, Transform secondChild)
    {
        string methodName = nameof(SwitchIndices);

        int firstIndex = IndexOf(firstChild);
        if (!CheckIndex(firstIndex, methodName))
            return;

        int secondIndex = IndexOf(secondChild);
        if (!CheckIndex(secondIndex, methodName))
            return;

        if (CheckReference(firstChild, methodName))
            secondChild.transform.SetSiblingIndex(firstIndex);
        if (CheckReference(secondChild, methodName))
            firstChild.transform.SetSiblingIndex(secondIndex);

        m_ChildTransforms.RemoveAt(firstIndex);
        m_ChildTransforms.Insert(firstIndex, secondChild);
        m_ChildTransforms.RemoveAt(secondIndex);
        m_ChildTransforms.Insert(secondIndex, firstChild);

        UpdatePositions();
    }

    public void ShiftList(int increment)
    {
        int length = ChildListSize;
        int shift = Mathf.Abs(increment) % length;

        if (shift == 0)
            return;

        int index = shift;
        int count = length - shift;

        if (increment > 0)
        {
            int temp = count;
            count = index;
            index = temp;
        }

        List<Transform> result = m_ChildTransforms.GetRange(index, count);
        result.AddRange(m_ChildTransforms.GetRange(0, index));
        m_ChildTransforms = result;

        UpdatePositions();
    }

    public void Add(Transform partyMember, int index)
    {
        string methodName = nameof(Add);

        if (CheckIndex(index, methodName))
        {
            if (CheckReference(partyMember, methodName))
            {
                if (m_ChildTransforms.Contains(partyMember))
                    m_ChildTransforms.Remove(partyMember);

                partyMember.transform.parent = transform;
                partyMember.transform.SetSiblingIndex(index);
            }

            m_ChildTransforms.Insert(index, partyMember);

            UpdatePositions();
        }
    }

    public void Add(Transform partyMember)
    {
        string methodName = nameof(Add);

        if (CheckReference(partyMember, methodName))
        {
            if (m_ChildTransforms.Contains(partyMember))
            {
                m_ChildTransforms.Remove(partyMember);
                partyMember.transform.parent = null;
            }

            partyMember.transform.parent = transform;
        }

        m_ChildTransforms.Add(partyMember);

        UpdatePositions();
    }

    public Transform Remove(int index)
    {
        string methodName = nameof(Remove);

        if (CheckIndex(index, methodName))
        {
            Transform obj = m_ChildTransforms[index];
            m_ChildTransforms.RemoveAt(index);

            if (CheckReference(obj, methodName))
                obj.transform.parent = null;

            UpdatePositions();

            return obj;
        }
        else
            return null;
    }

    public bool Remove(Transform partyMember)
    {
        string methodName = nameof(Remove);

        if (m_ChildTransforms.Remove(partyMember))
        {
            if (CheckReference(partyMember, methodName))
                partyMember.transform.parent = null;

            UpdatePositions();

            return true;
        }
        else
            return false;
    }

    public abstract void UpdatePositions();

    #endregion

    #region Private Methods

    protected void SetChildLocalPosition(Transform memberTransform, Vector3 displacement)
    {
        if (memberTransform != null)
            memberTransform.localPosition = displacement;
    }

    protected void SetChildLocalRotation(Transform memberTransform, Quaternion rotation)
    {
        if (memberTransform != null)
            memberTransform.localRotation = rotation;
    }

    protected void SetChildLocalRotation(Transform memberTransform, Vector3 rotationEuler)
    {
        SetChildLocalRotation(memberTransform, Quaternion.Euler(rotationEuler));
    }

    protected void SetChildLocalScale(Transform memberTransform, Vector3 scale)
    {
        if (memberTransform != null)
            memberTransform.localScale = scale;
    }

    private bool CheckIndex(int index, string methodName)
    {
        if (index < 0 || index >= ChildListSize)
        {
            Debug.LogWarning($"{methodName} for object {gameObject} was given an out-of-bounds index of {index}.");
            return false;
        }

        return true;
    }

    private bool CheckReference(Object obj, string methodName)
    {
        if (obj == null)
        {
            Debug.LogWarning($"{methodName} for object {gameObject} was accessing a NULL object.");
            return false;
        }

        return true;
    }

    #endregion
}

#if UNITY_EDITOR
[CustomEditor(typeof(ObjectContainer))]
[CanEditMultipleObjects]
public class ObjectContainerEditor : Editor
{
    private int m_GetIndex;

    private Transform m_GetTransform;

    private int m_FirstIndex;
    private int m_SecondIndex;

    private Transform m_FirstTransform;
    private Transform m_SecondTransform;

    private int m_ShiftIncrementation;

    private int m_InsertIndex;
    private Transform m_InsertTransform;

    private Transform m_AddTransform;

    private int m_RemoveIndex;

    private Transform m_RemoveTransform;

    // Displays the button that generates a string of the list of random drops
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        ObjectContainer tool = target as ObjectContainer;

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Get GameObject Method", EditorStyles.boldLabel);
        m_GetIndex = EditorGUILayout.IntField("Get Index: ", m_GetIndex);

        if (GUILayout.Button("Get GameObject"))
            Debug.Log(tool.GetChild(m_GetIndex));

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Get Index Method", EditorStyles.boldLabel);
        m_GetTransform = (Transform)EditorGUILayout.ObjectField("Insert GameObject: ", m_GetTransform, typeof(Transform), true);

        if (GUILayout.Button("Get Index"))
            Debug.Log(tool.IndexOf(m_GetTransform));

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Switch Indices Method", EditorStyles.boldLabel);
        m_FirstIndex = EditorGUILayout.IntField("First Index: ", m_FirstIndex);
        m_SecondIndex = EditorGUILayout.IntField("Second Index: ", m_SecondIndex);

        if (GUILayout.Button("Switch Indices"))
            tool.SwitchIndices(m_FirstIndex, m_SecondIndex);

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Switch GameObject Placements Method", EditorStyles.boldLabel);
        m_FirstTransform = (Transform)EditorGUILayout.ObjectField("First GameObject: ", m_FirstTransform, typeof(Transform), true);
        m_SecondTransform = (Transform)EditorGUILayout.ObjectField("Second GameObject: ", m_SecondTransform, typeof(Transform), true);

        if (GUILayout.Button("Switch Indices"))
            tool.SwitchChildren(m_FirstTransform, m_SecondTransform);

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Shift Party List Method", EditorStyles.boldLabel);
        m_ShiftIncrementation = EditorGUILayout.IntField("Shift Incrementation: ", m_ShiftIncrementation);

        if (GUILayout.Button("Shift Party List"))
            tool.ShiftList(m_ShiftIncrementation);

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Insert GameObject Method", EditorStyles.boldLabel);
        m_InsertIndex = EditorGUILayout.IntField("Insert Index: ", m_InsertIndex);
        m_InsertTransform = (Transform)EditorGUILayout.ObjectField("Insert GameObject: ", m_InsertTransform, typeof(Transform), true);

        if (GUILayout.Button("Insert GameObject"))
            tool.Add(m_InsertTransform, m_InsertIndex);

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Add GameObject Method", EditorStyles.boldLabel);
        m_AddTransform = (Transform)EditorGUILayout.ObjectField("Add GameObject: ", m_AddTransform, typeof(Transform), true);

        if (GUILayout.Button("Add GameObject"))
            tool.Add(m_AddTransform);

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Remove At Method", EditorStyles.boldLabel);
        m_RemoveIndex = EditorGUILayout.IntField("Remove Index: ", m_RemoveIndex);

        if (GUILayout.Button("Remove At Index"))
            tool.Remove(m_RemoveIndex);

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Remove GameObject Method", EditorStyles.boldLabel);
        m_RemoveTransform = (Transform)EditorGUILayout.ObjectField("Add GameObject: ", m_RemoveTransform, typeof(Transform), true);

        if (GUILayout.Button("Remove GameObject"))
            tool.Remove(m_RemoveTransform);

        EditorGUILayout.Space();
        if (GUILayout.Button("Update List"))
            tool.UpdateList();
    }
}
#endif