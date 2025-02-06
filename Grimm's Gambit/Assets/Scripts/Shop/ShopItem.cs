using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI; 

//Could possibly be transitioned into a Scriptable Object? 
public class ShopItem : MonoBehaviour
{
    [SerializeField]
    private Image icon;

    [SerializeField]
    private string name, description;

    const int cost = 1; 


    //Loot Type? 
    // Start is called before the first frame update
    
    public Image GetIcon(){
        return icon;
    }

    public string GetName(){
        return name;
    }

    public string GetDescription(){
        return description;
    }

}
