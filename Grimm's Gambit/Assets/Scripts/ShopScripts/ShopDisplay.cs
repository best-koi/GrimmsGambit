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
   private TMP_Text cardText, heirloomText, arcanaText, defaultText;

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
  
        cardImage = commonCards[cycleIndex].GetIcon();
        cardText.text = commonCards[cycleIndex].GetName();

        heirloomImage = heirlooms[cycleIndex].GetIcon();
        heirloomText.text = heirlooms[cycleIndex].GetName();

        arcanaImage = arcana[cycleIndex].GetIcon();
        arcanaText.text = arcana[cycleIndex].GetName();

    }

    public void CycleMenu(){
        if(cycleIndex > numberOfPages)
            cycleIndex = 0;
        else 
            cycleIndex++;
    }
}
