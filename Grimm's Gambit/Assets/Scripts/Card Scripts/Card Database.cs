using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDatabase : MonoBehaviour
{

    // Only instantiated if the player has one or more copies
    // Evens represent face up cards, odds are reversed
    [SerializeField] private Card[] m_Data;
    [SerializeField] private GameObject[] m_Prefabs;

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
            // Skip reversed cards
            if (card.GetIndex() % 2 == 1) continue;

            for (int i = 0, copies = card.NumCopies; i < copies; i++)
            {
                m_PlayerDeck.Add(card.GetIndex());

                Debug.Log("Spawning Card");
                Instantiate(card.gameObject);
            }
        }

        return m_PlayerDeck;
    }

    public Card GetCard(int index)
    {
        return m_Data[index];
    }

    public GameObject GetPrefab(int index) 
    {
        return m_Prefabs[index];
    }

    public void AddCard(int index, int numTimes = 1)
    {
        m_Data[index].NumCopies += numTimes;
    }
}
