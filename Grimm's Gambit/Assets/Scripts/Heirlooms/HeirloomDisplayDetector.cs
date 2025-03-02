//Ryan Lockie - 11/27/2024 - This file is used for mouseover display
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HeirloomDisplayDetector : MonoBehaviour
{
    //Both of these values are to be set when the detector is created
    public GameObject parentObject;
    public string Description; 
    public Sprite Image;
    public Transform imageContainer;

    private GameObject currentTooltip;
    public UnityEngine.UI.Image displayImageLocation;
    public TextMeshProUGUI displayTextLocation;

    private GameObject affixDisplay;
    
    private void Start()
    {
        GameObject[] objects = FindObjectsOfType<GameObject>(true);
        foreach (GameObject obj in objects)
        {
            if (obj.gameObject.name == "AffixDescriptionBox")
            {
                affixDisplay = obj;
                Debug.Log("found");
                break;
            }
        }
    }

    private void OnMouseEnter()
    {
        ShowTooltip(parentObject);
        affixDisplay.SetActive(true); //Enables object used to display both heirloom and affix text
    }

    private void OnMouseExit()
    {
        RemoveTooltip();
        affixDisplay.SetActive(false);
    }

    //Functionality for tooltips themselves:
    private void ShowTooltip(GameObject target)
    {
        displayTextLocation.text = Description; //Creates text - REPLACE THIS WITH A SPECIFIED VALUE BASED ON AFFIX
        displayImageLocation.sprite = Image;
    }

    private void RemoveTooltip()
    {
        displayImageLocation.sprite = null;
        displayTextLocation.text = "";
    }
}
