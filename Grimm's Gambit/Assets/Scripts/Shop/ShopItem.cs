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

    [SerializeField] private Card cardRef;

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

    public void StoreHeirloom(Heirloom heirloom)
    {

    }

    public void StoreCard(CardTemplate card)
    {

    }
}
