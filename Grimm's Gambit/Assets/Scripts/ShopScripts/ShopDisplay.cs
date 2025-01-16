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
   private Image cardImage, heirloomImage, arcanaImage;

   [SerializeField]
   private TMP_Text cardText, heirloomText, arcanaText;

   [SerializeField]
   private int cycleIndex; 

    // Start is called before the first frame update
    void Start()
    {

        cardImage = commonCards[cycleIndex].GetIcon();
        cardText.text = commonCards[cycleIndex].GetName();

        heirloomImage = heirlooms[cycleIndex].GetIcon();
        heirloomText.text = heirlooms[cycleIndex].GetName();

        arcanaImage = arcana[cycleIndex].GetIcon();
        arcanaText.text = arcana[cycleIndex].GetName();

        
    }

    // Update is called once per frame
    void Update()
    {
        eyeText.text = $"{eyeCount}";
        hairText.text = $"{hairCount}";
        tongueText.text = $"{tongueCount}";
    }
}
