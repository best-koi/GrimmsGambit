using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class UnitParty : ObjectContainer
{
    #region Private Fields

    [SerializeField]
    private float m_DistanceBetweenMembers;

    [SerializeField]
    private int m_RowArrangeThreshold = -1;

    [SerializeField]
    private float m_DistanceBetweenRows = 0;

    #endregion

    #region Object Container Callbacks

    public override void UpdatePositions()
    {
        int partyCount = ChildListSize;
        int halfCount = partyCount / 2;

        float distanceFromCenter = 0;
        float distanceBetweenMembers = m_DistanceBetweenMembers;
        float rowDistanceFromCenter = 0;

        bool pastThreshold = m_RowArrangeThreshold >= 0 && m_RowArrangeThreshold < partyCount;

        if (pastThreshold)
        {
            distanceBetweenMembers /= 2;
            rowDistanceFromCenter = m_DistanceBetweenRows / 2;
        }

        bool isEven = partyCount % 2 == 0;

        if (isEven)
            distanceFromCenter += distanceBetweenMembers / 2;
        else
        {
            distanceFromCenter += distanceBetweenMembers;
            SetChildLocalPosition(m_ChildTransforms[halfCount], Vector3.forward * -rowDistanceFromCenter);
        }   

        int leftIndex = halfCount - 1;
        int rightIndex = halfCount;
        if (!isEven)
            rightIndex++;

        for (int i = 0; i < halfCount; i++)
        {
            SetChildLocalPosition(m_ChildTransforms[leftIndex - i], new Vector3(-distanceFromCenter, 0f, isEven ? -rowDistanceFromCenter : rowDistanceFromCenter));
            SetChildLocalPosition(m_ChildTransforms[rightIndex + i], new Vector3(distanceFromCenter, 0f, rowDistanceFromCenter));

            distanceFromCenter += distanceBetweenMembers;
            rowDistanceFromCenter *= -1;
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