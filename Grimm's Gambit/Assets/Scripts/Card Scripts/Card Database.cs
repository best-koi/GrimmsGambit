using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDatabase : MonoBehaviour
{
    // Array of party members
    // Can be changed to accodomadate Unit Party or other data types
    [SerializeField] private GameObject[] m_PartyIndex;

    // New databse representing cards
    [SerializeField] private TwoDArray m_CardPrefabs;

    private void Start()
    {
        if (m_PartyIndex.Length != m_CardPrefabs.Arr.Length) Debug.LogWarning("Party size match database.");
    }

    private void Update()
    {
        
    }

    public List<CardData> PopulateDeck(bool instantiate = false)
    {
        List<CardData> m_PlayerDeck = new List<CardData>();

        /**
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
        */

        return m_PlayerDeck;
    }

    public Card GetCard(int owner, int index)
    {
        return GetPrefab(owner, index).GetComponent<Card>();
    }

    public GameObject GetPrefab(int owner, int index) 
    {
        return m_CardPrefabs.GetValue(owner, index);
    }

    public void AddCard(int owner, int index, int numTimes = 1)
    {
        GetPrefab(owner, index).GetComponent<Card>().NumCopies += numTimes;
    }

    public void ReverseCard(int owner, int index)
    {
        GameObject reverse = GetCard(owner, index).GetReverse().gameObject;
        m_CardPrefabs.SetValue(owner, index, reverse);
    }
}
