//Ryan Lockie - 11/27/2024 - This file is used for mouseover display
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AffixDisplayDetector : MonoBehaviour
{
    //Both of these values are to be set when the detector is created
    public GameObject parentObject;
    public string Description; 
    public Sprite Image;
    public UnityEngine.UI.Image displayImageLocation;
    public TextMeshProUGUI displayTextLocation;
    public int Stacks;
    public GameObject affixDisplay;
    
    private void OnMouseEnter()
    {
        ShowTooltip(parentObject);
        affixDisplay.SetActive(true);
    }

    private void OnMouseExit()
    {
        RemoveTooltip();
        affixDisplay.SetActive(false);
    }

    //Functionality for tooltips themselves:
    private void ShowTooltip(GameObject target)
    {
        displayTextLocation.text = Stacks.ToString() + " " + Description; //Creates text - REPLACE THIS WITH A SPECIFIED VALUE BASED ON AFFIX
        displayImageLocation.sprite = Image;
    }

    private void RemoveTooltip()
    {
        displayImageLocation.sprite = null;
        displayTextLocation.text = "";
    }
}
