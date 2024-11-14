using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class UnitParty : ObjectContainer
{
    #region Private Fields

    [SerializeField]
    private float m_DistanceBetweenMembers;

    #endregion

    #region Object Container Callbacks

    public override void UpdatePositions()
    {
        int partyCount = ChildListSize;
        int halfCount = partyCount / 2;
        float distanceFromCenter = 0;
        bool isEven = partyCount % 2 == 0;

        if (isEven)
            distanceFromCenter += m_DistanceBetweenMembers / 2;
        else
        {
            distanceFromCenter += m_DistanceBetweenMembers;
            SetChildLocalPosition(m_ChildTransforms[halfCount], Vector3.zero);
        }

        int leftIndex = halfCount - 1;
        int rightIndex = halfCount;
        if (!isEven)
            rightIndex++;

        for (int i = 0; i < halfCount; i++)
        {
            SetChildLocalPosition(m_ChildTransforms[leftIndex - i], Vector3.left * distanceFromCenter);
            SetChildLocalPosition(m_ChildTransforms[rightIndex + i], Vector3.right * distanceFromCenter);
            distanceFromCenter += m_DistanceBetweenMembers;
        }
    }

    #endregion
}

#if UNITY_EDITOR
[CustomEditor(typeof(UnitParty))]
[CanEditMultipleObjects]
public class UnitPartyEditor : ObjectContainerEditor
{
    
}
#endif