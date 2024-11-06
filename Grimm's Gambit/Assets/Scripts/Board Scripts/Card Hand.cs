using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class CardHand : MonoBehaviour
{
    [SerializeField] private CardDatabase m_Database;
    [SerializeField]
    private Transform m_CardHolder;

    [SerializeField]
    private Vector2 m_CardAngleBounds;

    [SerializeField]
    private Vector3 m_DisplacementFromHolderCenter;

    [SerializeField]
    private List<CardDisplay> cardsToReturn;

    private void Update()
    {
        Arrangecards();
        
    }

    private void Start()
    {
        Deck.onDraw += AddCardFromIndex;
        Deck.onDiscard += RemoveCardFromIndex;
    }

    public void AddCardFromIndex(int cardIndex)
    {
        GameObject card = Instantiate(m_Database.GetPrefab(cardIndex));
        AddCard(card);
    }

    public void AddCard(GameObject card)
    {
        card.transform.parent = this.transform;
    }

    public void RemoveCardFromIndex(int cardIndex)
    {
        Debug.Log(cardIndex);
        RemoveCard(transform.GetChild(cardIndex).gameObject);
    }

    public void RemoveCard(GameObject card)
    {
        card.transform.parent = null;
        Destroy(card);
    }

    public void ResetCards()
    {
        
    }

    private void Arrangecards()
    {
        if (m_CardHolder == null)
            return;

        List<CardDisplay> cards = new List<CardDisplay>();
        cards.AddRange(GetComponentsInChildren<CardDisplay>());

        // Calculate angle between cards
        int cardNum = cards.Count;
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

        // Apply transformations to all cards
        foreach (CardDisplay c in cards)
        {
            Transform t = c.transform;

            if (t != m_CardHolder)
            {
                Quaternion rotation = Quaternion.Euler(0f, 0f, currentAngle);
                Vector3 displacement = rotation * m_DisplacementFromHolderCenter;

                t.localRotation = rotation;
                t.localPosition = displacement;

                // Increment angle for each card
                currentAngle += angleBetweenCards;
            }
        }
    }
}
