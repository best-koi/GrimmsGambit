using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopDisplay : MonoBehaviour
{
   [SerializeField]
   private List<ShopItem> commonCards, heirlooms, arcana;//The items in the shop

   [SerializeField]
   private Image cardImage, heirloomImage, arcanaImage, defaultImage;//Images for each object (likely to be replaced with sprites)

   [SerializeField]
   private TMP_Text cardName, cardDesc, heirloomName, heirloomDesc, arcanaName, arcanaDesc;//Strings for names and descriptions 
   
   [SerializeField]
   private string defaultText;//Dummy Text for Empty Items 

   [SerializeField]
   private int cycleIndex;//Current index 

   [SerializeField]
   private int numberOfPages;//Used to determine when to stop cycling 
   
    [SerializeField] private int rerolls;

    // Start is called before the first frame update
    void Start()
    {
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
    private void DisplayShopItems(int index, Image image, TMP_Text name, TMP_Text desc, List<ShopItem> items){

        if(index < items.Count){
            image.sprite = items[index].GetIcon();
            name.text = items[index].GetName();
            desc.text = items[index].GetDescription();
        }else{
            image = defaultImage;
            name.text = defaultText;
            desc.text = defaultText; 

        }
  
    }



//Updates all Displays 
    private void DisplayAllCycledItems(){
        DisplayShopItems(cycleIndex, cardImage, cardName, cardDesc, commonCards);
        DisplayShopItems(cycleIndex, heirloomImage, heirloomName, heirloomDesc, heirlooms);
        DisplayShopItems(cycleIndex, arcanaImage, arcanaName, arcanaDesc, arcana);
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

        if (rerolls == 0) return;

        rerolls--;

        int randomCommon = Random.Range(0, commonCards.Count);
        int randomHeirloom = Random.Range(0, heirlooms.Count);
        int randomArcana = Random.Range(0, heirlooms.Count);

        DisplayShopItems(randomCommon, cardImage, cardName, cardDesc, commonCards);
        DisplayShopItems(randomHeirloom, heirloomImage, heirloomName, heirloomDesc, heirlooms);
        DisplayShopItems(randomArcana, arcanaImage, arcanaName, arcanaDesc, arcana); 

    }
}
