using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Space : MonoBehaviour
{
    /// Space MonoBehaviour
    /// 
    /// Represents a dice roll
    /// 
    /// </summary>

    // Maximum roll number
    [SerializeField]
    protected SpacePath[] m_NextSpaces = new SpacePath[1];

    [SerializeField]
    protected bool m_WillConsumeMovement = true;

    public bool WillConsumeMovement
    {
        get
        {
            return m_WillConsumeMovement;
        }
        set
        {
            m_WillConsumeMovement = value;
        }
    }

    protected bool m_HasCompletedSpace = false;

    public bool HasCompletedSpace
    {
        get
        {
            return m_HasCompletedSpace;
        }
        set
        {
            m_HasCompletedSpace = value;
        }
    }

    protected bool m_HasCompletedStop;

    public bool HasCompletedStop
    {
        get
        {
            return m_HasCompletedStop;
        }
        set
        {
            m_HasCompletedStop = value;
        }
    }

    [SerializeField]
    protected int m_SpaceState;

    public int SpaceState
    {
        get
        {
            return m_SpaceState;
        }
        set
        {
            m_SpaceState = value;
        }
    }

    protected void Start()
    {
        
    }

    public virtual void ActivateSpace()
    {
        //Debug.Log("Activate Space");
        m_HasCompletedSpace = true;
    }

    public virtual void DoStop()
    {
        //Debug.Log("Do Stop");
        m_HasCompletedStop = true;
    }

    public List<Space> GetNextSpaces()
    {
        List<Space> result = new List<Space>();

        for (int i = 0; i < m_NextSpaces.Length; i++)
        {
            SpacePath temp = m_NextSpaces[i];

            if (temp.IsOpen)
                result.Add(temp.Space);
        }

        return result;
    }

    public void ChoosePath(int index)
    {
        for (int i = 0; i < m_NextSpaces.Length; i++)
        {
            SpacePath path = m_NextSpaces[i];

            if (path != null)
                path.IsOpen = i == index;
        }
    }
    
    protected virtual void OnDrawGizmos()
    {
        if (m_NextSpaces == null || m_NextSpaces.Length <= 0)
            return;

        Vector3 thisPos = transform.position;
        float arrowHeadLength = 0.2f;
        float arrowHeadAngle = 30.0f;

        foreach (SpacePath n in m_NextSpaces)
        {
            if (n.Space != null && n.IsOpen)
            {
                Vector3 nextPos = n.Space.transform.position;
                Vector3 distance = nextPos - thisPos;

                Gizmos.color = Color.green;
                Gizmos.DrawRay(thisPos, distance);

                Vector3 right = Quaternion.LookRotation(distance) * Quaternion.Euler(0, 180 + arrowHeadAngle, 0) * Vector3.forward;
                Vector3 left = Quaternion.LookRotation(distance) * Quaternion.Euler(0, 180 - arrowHeadAngle, 0) * Vector3.forward;

                Vector3 distance2 = distance / 2 + Quaternion.LookRotation(distance) * Vector3.forward * 0.1f;

                Gizmos.DrawRay(thisPos + distance2, right * arrowHeadLength);
                Gizmos.DrawRay(thisPos + distance2, left * arrowHeadLength);
            }
        }
    }
}

[System.Serializable]
public class SpacePath
{
    [SerializeField]
    private Space m_Space;

    public Space Space
    {
        get
        {
            return m_Space;
        }
        set
        {
            m_Space = value;
        }
    }

    [SerializeField]
    private bool m_IsOpen = true;

    public bool IsOpen
    {
        get
        {
            return m_IsOpen;
        }
        set
        {
            m_IsOpen = value;
        }
    }
}
