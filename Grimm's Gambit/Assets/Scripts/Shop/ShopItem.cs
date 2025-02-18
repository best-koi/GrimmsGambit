using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI; 

//Could possibly be transitioned into a Scriptable Object? 
public class ShopItem : MonoBehaviour
{
    [SerializeField]
    private Sprite icon;

    [SerializeField]
    private string itemName, description;

    
    public Sprite GetIcon(){
        return icon;
    }

    public string GetName(){
        return itemName;
    }

    public string GetDescription(){
        return description;
    }

}
