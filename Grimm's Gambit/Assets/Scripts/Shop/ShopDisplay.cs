using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;


public class ShopDisplay : MonoBehaviour
{
    private ShopItem[,] displayedItems; //The items in the shop

    [SerializeField] private HeirloomManager heirloomManager; 

    [SerializeField] private Image[] pickImages;
    [SerializeField] private TMP_Text[] pickNames, pickDescriptions;

    [SerializeField] private Image defaultImage;
    [SerializeField] private string defaultText; //Dummy Text for Empty Items 

    [SerializeField] private Button rerollButton;
    [SerializeField] private int rerolls;

    [SerializeField] private int cycleIndex, numberOfPages; //Current page and total pages in shop

    [SerializeField] private int numberOfItems = 3; // Number of items to be displayed on each page

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
        
        LoadShopItems();
        DisplayShopItems(); 
    }

    private void LoadShopItems()
    {
        // Load in cards and arcana from the deck
        List<Heirloom> currentHeirlooms = heirloomManager.GetCurrentHeirlooms();
        int numHeirlooms = heirloomManager.GetNumHeirlooms();

        List<Heirloom> heirloomPool = new List<Heirloom>(), chosen = new List<Heirloom>();

        for (int i = 0; i < numHeirlooms; i++)
        {
            if (currentHeirlooms.Contains((Heirloom)i)) continue;

            heirloomPool.Add((Heirloom)i);
        }

        System.Random rand = new System.Random();

        for (int i = 0; 0 < numberOfItems; i++)
        {
            int randIndex = rand.Next(0, heirloomPool.Count);
            chosen.Add(heirloomPool[randIndex]);
            heirloomPool.RemoveAt(randIndex);
        }
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
