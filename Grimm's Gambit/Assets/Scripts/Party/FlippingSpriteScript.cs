using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// William Min

/*
 * Makes a sprite object in a 3D world face towards the camera in a similar vein as Paper Mario or Cult of the Lamb
 * 
 * Also considers if the sprite is facing to the right or left
 */

public class FlippingSpriteScript : SpriteBillboardScript
{
    // Main entity that holds the rotation
    [SerializeField]
    private GameObject m_RotationOwner;

    // Starting Vector for sprite rotation
    private Vector3 ZeroAxis = Vector3.right;

    // Makes sure that the sprite is generally facing towards the camera
    protected override void AdjustRotation()
    {
        float flippedY = m_Camera.transform.rotation.eulerAngles.y;
        float flippedX = m_Camera.transform.rotation.eulerAngles.x;

        if (m_RotationOwner != null)
        {
            Vector3 m_MyCenterVector = m_RotationOwner.transform.position;
            Vector3 m_MyFirstVector = m_RotationOwner.transform.forward + m_MyCenterVector;
            Vector3 m_MySecondVector = -m_Camera.transform.forward + m_MyCenterVector;

            renderLines(m_MyFirstVector, m_MySecondVector, m_MyCenterVector);

            if (!IsFacingForward(m_MyFirstVector, m_MySecondVector, m_MyCenterVector, ZeroAxis))
            {
                flippedY += 180;
                flippedX *= -1;
            }
        }

        SetRotation(flippedX, flippedY);
    }

    // Displays vector lines that are used for calculations
    private void renderLines(Vector3 vector1, Vector3 vector2, Vector3 vectorCenter)
    {
        vector1.y = vectorCenter.y;
        vector2.y = vectorCenter.y;

        Debug.DrawLine(vectorCenter, vector1, Color.magenta);
        Debug.DrawLine(vectorCenter, vector2, Color.blue);
    }

    // Checks if the sprite is facing along the game object's rotation
    private bool IsFacingForward(Vector3 vector1, Vector3 vector2, Vector3 vectorCenter, Vector3 zeroAxis)
    {
        return FacingAngle(vector1, vector2, vectorCenter, zeroAxis) > 0;
    }

    // Gets the degree of the sprite facing along the game object's rotation or not
    private float FacingAngle(Vector3 vector1, Vector3 vector2, Vector3 vectorCenter, Vector3 zeroAxis)
    {
        vector1.y = 0f;
        vector2.y = 0f;
        vectorCenter.y = 0f;

        Vector3 v1 = vector1 - vectorCenter;
        Vector3 v2 = vector2 - vectorCenter;

        float zDis1 = zeroAxis.z - v1.z - zeroAxis.z;
        float zDis2 = zeroAxis.z - v2.z;

        float sign1 = Mathf.Sign(zDis1);
        float sign2 = Mathf.Sign(zDis2);

        float angle1 = Vector3.Angle(zeroAxis, v1) * sign1;
        float angle2 = Vector3.Angle(zeroAxis, v2) * sign2;

        float finalAngle = angle2 - angle1;

        if (finalAngle < -180f)
            finalAngle += 360;
        else if (finalAngle > 180f)
            finalAngle -= 360;

        return finalAngle;
    }
}
