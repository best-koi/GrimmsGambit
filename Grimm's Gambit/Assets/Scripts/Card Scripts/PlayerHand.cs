using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    [SerializeField] private List<Card> hand;

    public void AddCard(Card card)
    {
        hand.Add(card);
    }

    public void RemoveCard(Card card)
    {
        hand[hand.IndexOf(card)] = null;
    }

    public bool Contains(Card card)
    {
        return hand.Contains(card);
    }
}
