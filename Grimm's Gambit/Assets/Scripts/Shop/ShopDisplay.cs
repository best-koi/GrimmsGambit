using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;


public class ShopDisplay : MonoBehaviour
{
    private ShopItem[,] displayedItems; //The items in the shop

    [SerializeField] private Deck shopDeck;
    private PlayerData playerData;
    private HeirloomManager heirloomManager; 

    [SerializeField] private Image[] pickImages;
    [SerializeField] private TMP_Text[] pickNames, pickDescriptions;

    [SerializeField] private Image defaultImage;
    [SerializeField] private string defaultText; //Dummy Text for Empty Items 

    [SerializeField] private Button rerollButton;
    [SerializeField] private int rerolls;

    [SerializeField] private int cycleIndex, numberOfPages; //Current page and total pages in shop

    [SerializeField] private int numberOfItems = 3; // Number of items to be displayed on each page

    [SerializeField] private int partySize = 3, maxCharacterCards = 15; // Party size and maximum number of cards a party member can have 

    // Start is called before the first frame update
    private void Start()
    {
        displayedItems = new ShopItem[numberOfItems, numberOfPages + 1];
        
        if (pickImages.Length != numberOfItems)
        {
            pickImages = new Image[numberOfItems];
        }
        
        if (pickNames.Length != numberOfItems) 
        { 
            pickNames = new TMP_Text[numberOfItems]; 
        }
        
        if (pickDescriptions.Length != numberOfItems)
        {
            pickDescriptions = new TMP_Text[numberOfItems];
        }
        
        LoadShopPool();
        DisplayShopItems(); 
    }

    private void LoadShopPool()
    {
        System.Random rand = new System.Random();

        // Load in cards and arcana from the deck
        playerData = FindObjectOfType<PlayerData>();
        shopDeck.m_GameDeck = playerData.GetPlayerDeck();

        List<CardData> cardPool = new List<CardData>(), temp = new List<CardData>();

        for (int i = 0; i < partySize; i++)
        {
            for (int j = 0; j < maxCharacterCards; j++)
            {
                CardData currentData = new CardData(i, j);

                if (shopDeck.m_GameDeck.Contains(currentData)) return;

                cardPool.Add(currentData);
            }
        }

        for(int i = 0; i < numberOfItems; i++)
        {
            int randIndex = rand.Next(0, cardPool.Count);
            temp.Add(cardPool[randIndex]);
            cardPool.RemoveAt(randIndex);
        }

        cardPool = temp;
        temp.Clear();

        // Load heirlooms from manager
        heirloomManager = FindObjectOfType<HeirloomManager>();
        List<Heirloom> currentHeirlooms = heirloomManager.GetCurrentHeirlooms(), heirloomPool = new List<Heirloom>();
        int numHeirlooms = heirloomManager.GetNumHeirlooms();

        for (int i = 0; i < numHeirlooms; i++)
        {
            if (currentHeirlooms.Contains((Heirloom)i)) continue;

            heirloomPool.Add((Heirloom)i);
        }

        currentHeirlooms = heirloomPool;
        heirloomPool.Clear();

        for (int i = 0; i < numberOfItems; i++)
        {
            int randIndex = rand.Next(0, currentHeirlooms.Count);
            heirloomPool.Add(heirloomPool[randIndex]);
            currentHeirlooms.RemoveAt(randIndex);
        }

        LoadShopItems(cardPool, heirloomPool);
    }

    // Helper method
    private void LoadShopItems(List<CardData> cards, List<Heirloom> heirlooms)
    {

    }

    private void DisplayItem(int i){
        pickImages[i].sprite = displayedItems[cycleIndex, i].GetIcon();

        if (pickImages[i] == null) pickImages[i].sprite = defaultImage.sprite;

        pickNames[i].text = displayedItems[cycleIndex, i].GetName();  
        pickDescriptions[i].text = displayedItems[cycleIndex, i].GetDescription();
    }

    private void DisplayShopItems()
    {
        for (int i = 0; i < displayedItems.GetLength(0); i++)
        {
            DisplayItem(i);
        }
    }


//Cycles the menu forward 1 
    public void CycleMenu(){
        if(cycleIndex < numberOfPages)
            cycleIndex++;
        else 
            cycleIndex = 0;
        Debug.Log(cycleIndex);
        DisplayShopItems();
    }
    

//Produces a random lineup of 3 Items in the Shop
    public void Reroll(){

        rerolls--;

        if (rerolls <= 0)
        {
            rerollButton.interactable = false;
            return;
        } 
        
        LoadShopItems();
    }
}
