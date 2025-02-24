using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopDeckDisplay : MonoBehaviour
{
    [SerializeField] private CardDatabase cardDatabase;
    [SerializeField] private Deck shopDeck;

    [SerializeField] private Card[] displayedCards;
    [SerializeField] private Card selectedCard;

    private int pageNumber;

    private void Awake()
    {

    }

    private void Start()
    {

    }

    private void LoadCards()
    {
        List<CardTemplate> cardTemplates = new List<CardTemplate>();

        for (int i = 0; i < displayedCards.Length; i++)
        {
            //displayedCards.SetCardTemplate(cardTemplates[i]);
        }
    }
}
