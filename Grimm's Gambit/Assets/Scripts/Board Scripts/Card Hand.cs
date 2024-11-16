using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class CardHand : ObjectContainer
{
    [SerializeField] private CardDatabase m_Database;

    [SerializeField]
    private Vector2 m_AngleBounds;

    [SerializeField]
    private Vector3 m_DisplacementFromContainerCenter;

    [SerializeField]
    private List<CardDisplay> cardsToReturn;

    protected override void Awake()
    {
        base.Awake();

        Deck.onDraw += AddCardFromIndex;
        Deck.onDiscard += RemoveCardFromIndex;
    }

    public void AddCardFromIndex(int cardIndex)
    {
        GameObject card = Instantiate(m_Database.GetPrefab(cardIndex));
        Add(card.transform);
    }

    public void RemoveCardFromIndex(int cardIndex)
    {
        Transform t = Remove(cardIndex);

        if (t != null)
            Destroy(t.gameObject);
    }

    #region Object Container Callbacks

    public override void UpdatePositions()
    {
        // Calculate angle between cards
        int cardNum = m_ChildTransforms.Count;
        float firstAngle = m_AngleBounds.x;
        float secondAngle = m_AngleBounds.y;
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

        // Apply transformations to all cards
        foreach (Transform t in m_ChildTransforms)
        {
            Quaternion rotation = Quaternion.Euler(0f, 0f, currentAngle);
            Vector3 displacement = rotation * m_DisplacementFromContainerCenter;

            SetChildLocalRotation(t, rotation);
            SetChildLocalPosition(t, displacement);

            // Increment angle for each card
            currentAngle += angleBetweenCards;
        }
    }

    #endregion
}

#if UNITY_EDITOR
[CustomEditor(typeof(CardHand))]
[CanEditMultipleObjects]
public class CardHandEditor : ObjectContainerEditor
{

}
#endif