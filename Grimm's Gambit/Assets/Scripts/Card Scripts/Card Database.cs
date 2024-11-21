using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDatabase : MonoBehaviour
{
    // Only instantiated if the player has one or more copies
    // Evens represent face up cards, odds are reversed
    [SerializeField] private GameObject[] m_Prefabs;

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }


    public List<int> PopulateDeck(bool instantiate = true)
    {
        List<int> m_PlayerDeck = new List<int>();

        for (int i = 0; i < m_Prefabs.Length; i++)
        {
            Card card = GetCard(i);
            card.SetIndex(i);

            // Skip reversed cards
            if (card.GetIndex() % 2 == 1 || card == null) continue;

            for (int j = 0, copies = card.NumCopies; j < copies; j++)
            {
                m_PlayerDeck.Add(card.GetIndex());

                if (instantiate)
                {
                    Debug.Log($"Spawning Card: {card.GetName()}");
                    Instantiate(card.gameObject);
                }
            }
        }

        return m_PlayerDeck;
    }

    public Card GetCard(int index)
    {
        return GetPrefab(index).GetComponent<Card>();
    }

    public GameObject GetPrefab(int index) 
    {
        return m_Prefabs[index];
    }

    public void AddCard(int index, int numTimes = 1)
    {
        GetPrefab(index).GetComponent<Card>().NumCopies += numTimes;
    }
}
