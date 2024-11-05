using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class UnitParty : MonoBehaviour
{
    #region Private Fields

    [SerializeField]
    private float m_DistanceBetweenMembers;

    [SerializeField]
    private List<GameObject> m_PartyMembers;

    public int PartySize
    {
        get
        {
            return m_PartyMembers.Count;
        }
    }

    #endregion

    #region MonoBehaviour Callbacks

    private void Start()
    {
        m_PartyMembers = new List<GameObject>();
    }

    private void FixedUpdate()
    {
        UpdatePartyList();
        UpdatePartyPositions();

        foreach (GameObject member in m_PartyMembers)
        {
            Debug.Log(member);
        }
    }

    #endregion

    #region Public Methods

    public List<GameObject> GetAllMembers() 
    { 
        return m_PartyMembers;
    }

    public GameObject GetPartyMember(int index)
    {
        string methodName = nameof(GetPartyMember);

        if (CheckIndex(index, methodName))
            return m_PartyMembers[index].gameObject;
        else
            return null;
    }

    public void UpdatePartyList()
    {
        m_PartyMembers.Clear();
        for (int i = 0; i < transform.childCount; i++)
            m_PartyMembers.Add(transform.GetChild(i).gameObject);
    }

    public void SwitchMemberIndices(int firstIndex, int secondIndex)
    {
        string methodName = nameof(SwitchMemberIndices);

        if (CheckIndex(firstIndex, methodName) && CheckIndex(secondIndex, methodName))
        {
            GameObject first = m_PartyMembers[firstIndex];
            GameObject second = m_PartyMembers[secondIndex];

            if (CheckReference(first, methodName))
                first.transform.SetSiblingIndex(secondIndex);
            if (CheckReference(second, methodName))
                second.transform.SetSiblingIndex(firstIndex);

            m_PartyMembers.RemoveAt(firstIndex);
            m_PartyMembers.Insert(firstIndex, second);
            m_PartyMembers.RemoveAt(secondIndex);
            m_PartyMembers.Insert(secondIndex, first);

            UpdatePartyPositions();
        }
    }

    public void ShiftPartyList(int increment)
    {
        int length = PartySize;
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

        List<GameObject> result = m_PartyMembers.GetRange(index, count);
        result.AddRange(m_PartyMembers.GetRange(0, index));
        m_PartyMembers = result;

        UpdatePartyPositions();
    }

    public void InsertMember(int index, GameObject partyMember)
    {
        string methodName = nameof(InsertMember);

        if (CheckIndex(index, methodName))
        {
            if (CheckReference(partyMember, methodName))
            {
                if (m_PartyMembers.Contains(partyMember))
                    m_PartyMembers.Remove(partyMember);

                partyMember.transform.parent = transform;
                partyMember.transform.SetSiblingIndex(index);
            }

            m_PartyMembers.Insert(index, partyMember);

            UpdatePartyPositions();
        }
    }

    public void AddMember(GameObject partyMember)
    {
        string methodName = nameof(AddMember);

        if (CheckReference(partyMember, methodName))
        {
            if (m_PartyMembers.Contains(partyMember))
            {
                m_PartyMembers.Remove(partyMember);
                partyMember.transform.parent = null;
            }

            partyMember.transform.parent = transform;
        }

        m_PartyMembers.Add(partyMember);

        UpdatePartyPositions();
    }

    public GameObject RemoveMemberAt(int index)
    {
        string methodName = nameof(RemoveMemberAt);

        if (CheckIndex(index, methodName))
        {
            GameObject obj = m_PartyMembers[index];
            m_PartyMembers.RemoveAt(index);

            if (CheckReference(obj, methodName))
                obj.transform.parent = null;

            UpdatePartyPositions();

            return obj;
        }
        else
            return null;
    }

    public bool RemoveMember(GameObject partyMember)
    {
        string methodName = nameof(RemoveMember);

        if (m_PartyMembers.Remove(partyMember))
        {
            if (CheckReference(partyMember, methodName))
                partyMember.transform.parent = null;

            UpdatePartyPositions();

            return true;
        }
        else
            return false;
    }

    #endregion

    #region Private Methods

    private void UpdatePartyPositions()
    {
        int partyCount = PartySize;
        int halfCount = partyCount / 2;
        float distanceFromCenter = 0;
        bool isEven = partyCount % 2 == 0;

        if (isEven)
            distanceFromCenter += m_DistanceBetweenMembers / 2;
        else
        {
            distanceFromCenter += m_DistanceBetweenMembers;
            SetMemberLocalPosition(m_PartyMembers[halfCount], Vector3.zero);
        }

        int leftIndex = halfCount - 1;
        int rightIndex = halfCount;
        if (!isEven)
            rightIndex++;

        for (int i = 0; i < halfCount; i++)
        {
            SetMemberLocalPosition(m_PartyMembers[leftIndex - i], Vector3.left * distanceFromCenter);
            SetMemberLocalPosition(m_PartyMembers[rightIndex + i], Vector3.right * distanceFromCenter);
            distanceFromCenter += m_DistanceBetweenMembers;
        }
    }

    private void SetMemberLocalPosition(GameObject member, Vector3 displacement)
    {
        if (member != null)
            member.transform.localPosition = displacement;
    }

    private bool CheckIndex(int index, string methodName)
    {
        if (index < 0 || index >= PartySize)
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
[CustomEditor(typeof(UnitParty))]
[CanEditMultipleObjects]
public class UnitPartyEditor : Editor
{
    private int m_GetIndex;

    private int m_FirstIndex;
    private int m_SecondIndex;

    private int m_ShiftIncrementation;

    private int m_InsertIndex;
    private GameObject m_InsertGameObject;

    private GameObject m_AddGameObject;

    private int m_RemoveIndex;

    private GameObject m_RemoveGameObject;

    // Displays the button that generates a string of the list of random drops
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        UnitParty tool = target as UnitParty;

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Get GameObject Method", EditorStyles.boldLabel);
        m_GetIndex = EditorGUILayout.IntField("Get Index: ", m_GetIndex);

        if (GUILayout.Button("Get GameObject"))
            Debug.Log(tool.GetPartyMember(m_GetIndex));

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Switch Indices Method", EditorStyles.boldLabel);
        m_FirstIndex = EditorGUILayout.IntField("First Index: ", m_FirstIndex);
        m_SecondIndex = EditorGUILayout.IntField("Second Index: ", m_SecondIndex);

        if (GUILayout.Button("Switch Indices"))
            tool.SwitchMemberIndices(m_FirstIndex, m_SecondIndex);

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Shift Party List Method", EditorStyles.boldLabel);
        m_ShiftIncrementation = EditorGUILayout.IntField("Shift Incrementation: ", m_ShiftIncrementation);

        if (GUILayout.Button("Shift Party List"))
            tool.ShiftPartyList(m_ShiftIncrementation);

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Insert GameObject Method", EditorStyles.boldLabel);
        m_InsertIndex = EditorGUILayout.IntField("Insert Index: ", m_InsertIndex);
        m_InsertGameObject = (GameObject)EditorGUILayout.ObjectField("Insert GameObject: ", m_InsertGameObject, typeof(GameObject), true);

        if (GUILayout.Button("Insert GameObject"))
            tool.InsertMember(m_InsertIndex, m_InsertGameObject);

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Add GameObject Method", EditorStyles.boldLabel);
        m_AddGameObject = (GameObject)EditorGUILayout.ObjectField("Add GameObject: ", m_AddGameObject, typeof(GameObject), true);

        if (GUILayout.Button("Add GameObject"))
            tool.AddMember(m_AddGameObject);

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Remove At Method", EditorStyles.boldLabel);
        m_RemoveIndex = EditorGUILayout.IntField("Remove Index: ", m_RemoveIndex);

        if (GUILayout.Button("Remove At Index"))
            tool.RemoveMemberAt(m_RemoveIndex);

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Remove GameObject Method", EditorStyles.boldLabel);
        m_RemoveGameObject = (GameObject)EditorGUILayout.ObjectField("Add GameObject: ", m_RemoveGameObject, typeof(GameObject), true);

        if (GUILayout.Button("Remove GameObject"))
            tool.RemoveMember(m_RemoveGameObject);

        EditorGUILayout.Space();
        if (GUILayout.Button("Update Party List"))
            tool.UpdatePartyList();
    }
}
#endif