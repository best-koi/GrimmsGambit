using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

[Serializable]
internal class TextValue
{
    [SerializeField] internal TMP_Text txt;

    internal void SetText(string val)
    {
        txt.text = val;
    }
}

public class ShopDisplay : MonoBehaviour
{
    private ShopItem[][] displayedItems; //The items in the shop

    [SerializeField] private Sprite[] pickImages;

    [SerializeField] private Image defaultImage;

    [SerializeField] private TextValue[] pickNames, pickDescriptions;

    [SerializeField] private Button rerollButton;

    [SerializeField] private string defaultText;//Dummy Text for Empty Items 

   [SerializeField]
   private int cycleIndex;//Current index 

   [SerializeField]
   private int numberOfPages;//Used to determine when to stop cycling 
   
    [SerializeField] private int rerolls;


    // Start is called before the first frame update
    void Start()
    {
        displayedItems = new ShopItem[3][3];

        DisplayAllCycledItems(); 
    }

    // Update is called once per frame
    void Update()
    {

    }

//Displays items by a given index. If the index exceeds the provided list, then blank text is used 
//Takes the following parameters
//int index - represents the current index to display by
//Image image - the image slot of the item
//TMP_Text name - the name text of the item to set
//TMP_Text desc - the description text of the item to set
//List<ShopItem> items - a list of ShopItems to use 
    private void DisplayItem(int i){
        pickImages[i] = displayedItems[cycleIndex][i].GetIcon();
        pickNames[i].SetText(pickImages[cycleIndex][i].GetName());  
        pickDescriptions[i].SetText(pickDescriptions[cycleIndex][i].GetDescription());
    }

    private void DisplayShopItems()
    {
        for (int i = 0; i < displayedItems[cycleIndex].Length; i++)
        {
            DisplayItem(i);
        }
    }



//Updates all Displays 
    private void DisplayAllCycledItems(){
        /**
        DisplayShopItems(cycleIndex, cardImage, cardName, cardDesc, commonCards);
        DisplayShopItems(cycleIndex, heirloomImage, heirloomName, heirloomDesc, heirlooms);
        DisplayShopItems(cycleIndex, arcanaImage, arcanaName, arcanaDesc, arcana);
        */
    }

//Cycles the menu forward 1 
    public void CycleMenu(){
        if(cycleIndex < numberOfPages)
            cycleIndex++;
        else 
            cycleIndex = 0;
        Debug.Log(cycleIndex);
        DisplayAllCycledItems();
    }
    

//Produces a random lineup of 3 Items in the Shop
    public void Reroll(){

        rerolls--;

        if (rerolls <= 0)
        {
            rerollButton.interactable = false;
            return;
        } 

        /**
        int randomCommon = Random.Range(0, commonCards.Length);
        int randomHeirloom = Random.Range(0, heirlooms.Length);
        int randomArcana = Random.Range(0, heirlooms.Length);

        DisplayShopItems(0, randomCommon, cardImage, cardName, cardDesc, displayedItems);
        DisplayShopItems(1, randomHeirloom, heirloomImage, heirloomName, heirloomDesc, displayedItems);
        DisplayShopItems(2, randomArcana, arcanaImage, arcanaName, arcanaDesc, displayedItems); 

        */
    }
}
