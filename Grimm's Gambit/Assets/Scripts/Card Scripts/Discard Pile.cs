using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscardPile : MonoBehaviour
{
    /**
     Like overlaps with UI scripts
     Can be removed later
     */

    [SerializeField] private Deck m_DeckController;

    // All of the cards in the player's discard pile
    // Can later be replaced with the actual card GameObjects
    // [SerializeField] private List<GameObject> m_Cards;
    [SerializeField] private List<int> m_Cards;

    // Indice of the most recently discarded card
    [SerializeField] private int m_TopCardID;

    private void Start()
    {
        Deck.onDiscard += UpdateDiscardPile;
    }

    private void Update()
    {
        
    }

    private void UpdateDiscardPile(int discardedID)
    {
        m_TopCardID = discardedID;
        m_Cards = m_DeckController.GetDiscardPile();
    }
}
