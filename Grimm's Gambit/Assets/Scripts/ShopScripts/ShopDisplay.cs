using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopDisplay : MonoBehaviour
{
    [SerializeField]
    private TMP_Text eyeText, hairText, tongueText; //Text to display the loot drops 

    [SerializeField]
    private int eyeCount, hairCount, tongueCount;//Ints to test out shop values - to be taken from player loot script eventually 

   [SerializeField]
   private List<ShopItem> commonCards, heirlooms, arcana;

   [SerializeField]
   private Image cardImage, heirloomImage, arcanaImage, defaultImage;

   [SerializeField]
   private TMP_Text cardName, cardDesc, heirloomName, heirloomDesc, arcanaName, arcanaDesc;
   
   [SerializeField]
   private string defaultText; 

   [SerializeField]
   private int cycleIndex; 

   [SerializeField]
   private int numberOfPages; 

    // Start is called before the first frame update
    void Start()
    {

        DisplayShopItems();

        
    }

    // Update is called once per frame
    void Update()
    {
        eyeText.text = $"{eyeCount}";
        hairText.text = $"{hairCount}";
        tongueText.text = $"{tongueCount}";

        DisplayShopItems();
    }

    public void DisplayShopItems(){

        if(cycleIndex < commonCards.Count){
            cardImage = commonCards[cycleIndex].GetIcon();
            cardName.text = commonCards[cycleIndex].GetName();
            cardDesc.text = commonCards[cycleIndex].GetDescription();
        }else{
            cardImage = defaultImage;
            cardName.text = defaultText;
            cardDesc.text = defaultText; 

        }
  
         if(cycleIndex < heirlooms.Count){
            heirloomImage = heirlooms[cycleIndex].GetIcon();
            heirloomName.text = heirlooms[cycleIndex].GetName();
            heirloomDesc.text = heirlooms[cycleIndex].GetDescription();
         }else{
            heirloomImage = defaultImage;
            heirloomName.text = defaultText;
            heirloomDesc.text = defaultText; 

         }
         if(cycleIndex < arcana.Count){
            arcanaImage = arcana[cycleIndex].GetIcon();
            arcanaName.text = arcana[cycleIndex].GetName();
            arcanaDesc.text = arcana[cycleIndex].GetDescription(); 
         }else{
            arcanaImage = defaultImage;
            arcanaName.text = defaultText;
            arcanaDesc.text = defaultText; 

         }

    }

    public void CycleMenu(){
        if(cycleIndex < numberOfPages)
            cycleIndex++;
        else 
            cycleIndex = 0;
        Debug.Log(cycleIndex);
    }
}
