using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Shop Item", menuName = "Shop/Shop Item")]
public class ShopItem : ScriptableObject
{
    [SerializeField]
    private Sprite icon;

    [SerializeField]
    private string itemName, description;

    [SerializeField] private CardTemplate cardRef;

    [SerializeField] private Heirloom heirloomRef;
    
    public Sprite GetIcon(){
        return icon;
    }

    public string GetName(){
        return itemName;
    }

    public string GetDescription(){
        return description;
    }

    public void StoreHeirloom(Heirloom heirloom, HeirloomImageLibrary library)
    {
        heirloomRef = heirloom;

        itemName = heirloom.ToString();

        description = library.descriptionLibrary[(int)heirloom];
        icon = library.spriteLibrary[(int)heirloom];
    }

    public void StoreCard(CardTemplate card)
    {
        cardRef = card;

        itemName = card.CardName;
        description = card.CardDescription;
        icon = card.CardSprite;
    }

    public CardTemplate GetCard()
    {
        return cardRef;
    }

    public Heirloom GetHeirloom()
    {
        return heirloomRef;
    }
}
