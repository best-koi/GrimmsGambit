using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDatabase : MonoBehaviour
{
    [SerializeField] private Card[] m_Data;
    
    // The physical representaion of the all of the cards
    // Only instantiated if the player has one or more copies
    [SerializeField] private GameObject[] m_Cards;

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    public List<int> PopulateDeck()
    {
        List<int> m_PlayerDeck = new List<int>();

        foreach (Card card in m_Data)
        {
            for (int i = 0, copies = card.NumCopies; i < copies; i++)
            {
                m_PlayerDeck.Add(card.GetIndex());
                
                // Unfinished
                // Instantiate the card GameObject into the scene
            }
        }

        return m_PlayerDeck;
    }

    public Card GetCard(int index)
    {
        return m_Data[index];
    }

    public void AddCard(int index, int numTimes = 1)
    {
        m_Data[index].NumCopies += numTimes;
    }
}