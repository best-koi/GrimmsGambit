using System.Collections.Generic;
using UnityEngine;

public class CardDatabase : MonoBehaviour
{
    [Tooltip("The card prefab used to spawn card objects from template.")] [SerializeField] private GameObject _cardPrefab;
    [Tooltip(" Array of party members. Can be changed to accodomadate Unit Party or other data types.")] [SerializeField] private Minion[] _partyIndex;
    [Tooltip("New databse representing cards.")] [SerializeField] public Array2D _cardPrefabs; //Made public by Ryan 2/8/2025 for shop menu purposes

    private void Start()
    {
        if (_partyIndex.Length != _cardPrefabs._baseArray.Length) Debug.LogWarning("Party size match database.");

        for (int i = 0; i < _cardPrefabs.BaseArray.Length; i++)
            for (int j = 0; j < _cardPrefabs.BaseArray[i].Row.Length; j++)
                GetTemplate(i, j).Data = new CardData(i, j);
    }

    public CardTemplate GetTemplate(int owner, int index) 
    {
        return (CardTemplate)_cardPrefabs.GetValue(owner, index);
    }

    public GameObject SpawnCardObject(int owner, int index)
    {
        CardTemplate template = GetTemplate(owner, index);
        GameObject cardObject = Instantiate(_cardPrefab);

        if (cardObject.TryGetComponent<Card>(out Card card))
        {
            card.SetCardTemplate(template);

            Minion caster = _partyIndex[owner];
            if (caster != null)
                card.Caster = caster;
        }

        return cardObject;
    }

    public void ReverseCard(int owner, int index)
    {
        Object reverse = GetTemplate(owner, index).ReverseTemplate;
        _cardPrefabs.SetValue(owner, index, reverse);
    }

    public int RetrieveIndexFromMinion(Minion minion) //The purpose of this function is to determine the database index of a minion as it is destroyed
    {
        for (int i = 0; i < _partyIndex.Length; i++)
        {
            if (_partyIndex[i] == minion)
            {
                return i;
            }
        }
        return -1; //Should never occur
    }
}
