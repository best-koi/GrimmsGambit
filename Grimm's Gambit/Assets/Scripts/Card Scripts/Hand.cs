using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    [SerializeField] private Deck m_DeckController;

    // All of the cards in the player's hand 
    [SerializeField] private List<int> m_Cards;

    // Indice of the most recently drawn card
    [SerializeField] private int m_TopCardID;

    private void Start()
    {
        Deck.onDraw += UpdateHand;
    }

    private void Update()
    {

    }

    private void UpdateHand(int drawID)
    {
        m_TopCardID = drawID;
        m_Cards = m_DeckController.GetHand();
    }
}
