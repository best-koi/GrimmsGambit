using System.Collections.Generic;
using UnityEngine;

public class CardDatabaseV2 : MonoBehaviour
{
    // Array of party members
    // Can be changed to accodomadate Unit Party or other data types
    [SerializeField] private Minion[] _partyIndex;

    // New databse representing cards
    [SerializeField] private Array2D _cardPrefabs;

    private void Start()
    {
        if (_partyIndex.Length != _cardPrefabs._baseArray.Length) Debug.LogWarning("Party size match database.");
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

    public CardTemplate GetTemplate(int owner, int index) 
    {
        return (CardTemplate)_cardPrefabs.GetValue(owner, index);
    }

    public GameObject GetCardInstance(int owner, int index)
    {
        return GetTemplate(owner, index).SpawnCardObject(_partyIndex[owner]);
    }

    public void ReverseCard(int owner, int index)
    {
        Object reverse = GetTemplate(owner, index).ReverseTemplate;
        _cardPrefabs.SetValue(owner, index, reverse);
    }
}
