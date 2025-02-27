using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopDeckDisplay : MonoBehaviour
{
    [SerializeField] private CardDatabase cardDatabase;
    [SerializeField] private Deck shopDeck;

    [SerializeField] private Card[] displayedCards;
    [SerializeField] private Card selectedCard;

    [SerializeField] private Button nextButton, removeButton;

    private int pageNumber, totalPages;

    private void Start()
    {
        int deckSize = shopDeck.m_GameDeck.Count;
        totalPages = deckSize / 9;
        
        if (deckSize % 9 > 0)
        {
            totalPages++;
        }

        foreach (var card in displayedCards)
        {
            card.onCardHover += SelectCard;
        }
    }

    private void OnEnable()
    {
        pageNumber = 0;

        LoadCards();
    }

    private void LoadCards()
    {
        if(shopDeck.m_GameDeck.Count == 0)
        {
            Debug.LogError("Shop deck is empty. Cannot load cards.");
            return;
        }

        List<CardTemplate> cardTemplates = new List<CardTemplate>();

        for (int i = pageNumber * totalPages; i < (pageNumber + 1) * totalPages; i++)
        {
            cardTemplates.Add(shopDeck.GetCard(i));
        }

        for (int i = 0; i < displayedCards.Length; i++)
        {
            if (cardTemplates[i] == null)
            {
                displayedCards[i].gameObject.SetActive(false);
            }
            else
            {
                displayedCards[i].SetCardTemplate(cardTemplates[i]);
            }
        }
    }

    public void CyclePage()
    {
        if (pageNumber + 1 > totalPages) pageNumber = 0;
        else pageNumber++;

        LoadCards();
    }

    public void RemoveCard()
    {
        removeButton.interactable = false;

        shopDeck.RemoveCard(selectedCard.Data);
    }

    public void SelectCard(CardTemplate cardVals)
    {
        selectedCard.gameObject.SetActive(true);
        removeButton.gameObject.SetActive(true);
        selectedCard.SetCardTemplate(cardVals);
    }
}
