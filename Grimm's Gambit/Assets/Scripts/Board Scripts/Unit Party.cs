using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class UnitParty : MonoBehaviour
{
    [SerializeField]
    private float m_DistanceBetweenMembers;

    private List<GameObject> m_PartyMembers;

    private void Start()
    {
        m_PartyMembers = new List<GameObject>();

        UpdatePartyList();
    }

    private void FixedUpdate()
    {
        UpdatePartyPositions();

        /*
        int i = 0;

        while (i < m_PartyMembers.Count)
        {
            GameObject obj = m_PartyMembers[i];

            if (obj == null)
                m_PartyMembers.RemoveAt(i);
            else
                i++;
        }

        PartyListSetup();
        */
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

            if (CheckReference(first, methodName) && CheckReference(second, methodName))
            {
                first.transform.SetSiblingIndex(secondIndex);
                second.transform.SetSiblingIndex(firstIndex);

                m_PartyMembers.RemoveAt(firstIndex);
                m_PartyMembers.Insert(firstIndex, second);
                m_PartyMembers.RemoveAt(secondIndex);
                m_PartyMembers.Insert(secondIndex, first);
            }
        }
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
    }

    public GameObject RemoveMemberAt(int index)
    {
        string methodName = nameof(RemoveMemberAt);

        if (CheckIndex(index, methodName))
        {
            GameObject obj = m_PartyMembers[index];
            m_PartyMembers.RemoveAt(index);
            obj.transform.parent = null;
            return obj;
        }
        else
            return null;
    }

    public bool RemoveMember(GameObject partyMember)
    {
        string methodName = nameof(RemoveMember);

        if (CheckReference(partyMember, methodName) && m_PartyMembers.Remove(partyMember))
        {
            partyMember.transform.parent = null;
            return true;
        }
        else
            return false;
    }

    private void UpdatePartyPositions()
    {
        int partyCount = m_PartyMembers.Count;
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
        if (index < 0 || index >= m_PartyMembers.Count)
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

    /*
    public void SetParentToMember(int index, GameObject partyMember)
    {
        string methodName = nameof(SetParentToMember);
        if (CheckIndex(index, methodName))
        {
            Transform parent = m_PartyMemberPositions[index];

            if (CheckReference(parent, methodName))
                partyMember.transform.parent = parent;
        }
    }

    public void SwitchPositions(int firstIndex, int secondIndex)
    {
        string methodName = nameof(SwitchPositions);

        if (CheckIndex(firstIndex, methodName) && CheckIndex(secondIndex, methodName))
        {
            GameObject first = GetPartyMemberAtIndex(firstIndex);
            GameObject second = GetPartyMemberAtIndex(secondIndex);

            SetParentToMember(firstIndex, second);
            SetParentToMember(secondIndex, first);
        }
    }

    public GameObject GetPartyMemberAtIndex(int index)
    {
        return m_PartyMemberPositions[index].GetChild(0).gameObject;
    }

    public void AddPartyMember(int index, GameObject partyMember)
    {
        Transform partyPosition = new GameObject().transform;
        partyPosition.parent = transform;
        partyPosition.SetSiblingIndex(index);
        m_PartyMemberPositions.Insert(index, partyPosition);

        if (partyMember != null)
            SetParentToMember(index, partyMember);
    }

    public void AddPartyMember(GameObject partyMember)
    {
        Transform partyPosition = new GameObject().transform;
        partyPosition.parent = transform;

        int latestIndex = m_PartyMemberPositions.Count;
        m_PartyMemberPositions.Add(partyPosition);

        if (partyMember != null)
            SetParentToMember(latestIndex, partyMember);
    }

    public GameObject RemovePartyMember(int index)
    {
        string methodName = nameof(RemovePartyMember);

        if (CheckIndex(index, methodName))
        {
            Transform parent = m_PartyMemberPositions[index];

            if (CheckReference(parent, methodName))
            {
                GameObject obj = parent.GetChild(0).gameObject;
                obj.transform.parent = null;
                m_PartyMemberPositions.RemoveAt(index);
                Destroy(parent);
                return obj;
            }
        }

        return null;
    }

    public GameObject RemovePartyMember()
    {
        return RemovePartyMember(m_PartyMemberPositions.Count - 1);
    }

    public void UpdatePartyPositions()
    {

    }

    private bool CheckIndex(int index, string methodName)
    {
        if (index < 0 || index >= m_PartyMemberPositions.Count)
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
            Debug.LogWarning($"{methodName} for object {gameObject} was accessing a NULL {obj.GetType()} object.");
            return false;
        }

        return true;
    }
    */

    /*
    [SerializeField]
    private int m_InitialPartyMemberCount;
    [SerializeField]
    private float m_DistanceBetweenMembers;

    private List<Transform> m_PartyMemberPositions;

    private void Start()
    {
        m_PartyMemberPositions = new List<Transform>();

        for (int i = 0; i < m_InitialPartyMemberCount; i++)
            AddPartyMember(null);
    }

    private void FixedUpdate()
    {
        
    }

    public void SetParentToMember(int index, GameObject partyMember)
    {
        string methodName = nameof(SetParentToMember);
        if (CheckIndex(index, methodName))
        {
            Transform parent = m_PartyMemberPositions[index];

            if (CheckReference(parent, methodName))
                partyMember.transform.parent = parent;
        }
    }

    public void SwitchPositions(int firstIndex, int secondIndex)
    {
        string methodName = nameof(SwitchPositions);

        if (CheckIndex(firstIndex, methodName) && CheckIndex(secondIndex, methodName))
        {
            GameObject first = GetPartyMemberAtIndex(firstIndex);
            GameObject second = GetPartyMemberAtIndex(secondIndex);
            
            SetParentToMember(firstIndex, second);
            SetParentToMember(secondIndex, first);
        }
    }

    public GameObject GetPartyMemberAtIndex(int index)
    {
        return m_PartyMemberPositions[index].GetChild(0).gameObject;
    }

    public void AddPartyMember(int index, GameObject partyMember)
    {
        Transform partyPosition = new GameObject().transform;
        partyPosition.parent = transform;
        partyPosition.SetSiblingIndex(index);
        m_PartyMemberPositions.Insert(index, partyPosition);

        if (partyMember != null)
            SetParentToMember(index, partyMember);
    }

    public void AddPartyMember(GameObject partyMember)
    {
        Transform partyPosition = new GameObject().transform;
        partyPosition.parent = transform;

        int latestIndex = m_PartyMemberPositions.Count;
        m_PartyMemberPositions.Add(partyPosition);

        if (partyMember != null)
            SetParentToMember(latestIndex, partyMember);
    }

    public GameObject RemovePartyMember(int index)
    {
        string methodName = nameof(RemovePartyMember);

        if (CheckIndex(index, methodName))
        {
            Transform parent = m_PartyMemberPositions[index];

            if (CheckReference(parent, methodName))
            {
                GameObject obj = parent.GetChild(0).gameObject;
                obj.transform.parent = null;
                m_PartyMemberPositions.RemoveAt(index);
                Destroy(parent);
                return obj;
            }
        }

        return null;
    }

    public GameObject RemovePartyMember()
    {
        return RemovePartyMember(m_PartyMemberPositions.Count - 1);
    }

    public void UpdatePartyPositions()
    {
        
    }

    private bool CheckIndex(int index, string methodName)
    {
        if (index < 0 || index >= m_PartyMemberPositions.Count)
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
            Debug.LogWarning($"{methodName} for object {gameObject} was accessing a NULL {obj.GetType()} object.");
            return false;
        }

        return true;
    }
    */
}

#if UNITY_EDITOR
[CustomEditor(typeof(UnitParty))]
[CanEditMultipleObjects]
public class UnitPartyEditor : Editor
{
    private int m_GetIndex;

    private int m_FirstIndex;
    private int m_SecondIndex;

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