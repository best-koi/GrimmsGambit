using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHand : MonoBehaviour
{
    [SerializeField]
    private Transform m_CardHolder;

    [SerializeField]
    private Vector2 m_CardAngleBounds;

    [SerializeField]
    private Vector3 m_DisplacementFromHolderCenter;

    private void Update()
    {
        Arrangecards();
    }

    public void AddCard()
    {

    }

    public void RemoveCard()
    {

    }

    private void Arrangecards()
    {
        if (m_CardHolder == null)
            return;
        
        List<Transform> cardTransforms = new List<Transform>();
        cardTransforms.AddRange(GetComponentsInChildren<Transform>());
        cardTransforms.Remove(m_CardHolder);

        int cardNum = cardTransforms.Count;
        float firstAngle = m_CardAngleBounds.x;
        float secondAngle = m_CardAngleBounds.y;
        float totalAngle = Mathf.Abs(firstAngle - secondAngle);

        int angleSpaceCount = (cardNum % 2 == 0 ? cardNum : cardNum - 1);

        float angleBetweenCards = totalAngle;
        if (angleSpaceCount > 0)
            angleBetweenCards /= angleSpaceCount;

        if (firstAngle > secondAngle)
            angleBetweenCards *= -1;

        float currentAngle = firstAngle;
        if (cardNum == 1 || cardNum % 2 == 0)
            currentAngle += angleBetweenCards / 2;

        foreach (Transform t in cardTransforms)
        {
            if (t != m_CardHolder)
            {
                Quaternion rotation = Quaternion.Euler(0f, 0f, currentAngle);
                Vector3 displacement = rotation * m_DisplacementFromHolderCenter;

                t.localRotation = rotation;
                t.localPosition = displacement;
                currentAngle += angleBetweenCards;
            }
        }
    }
}
