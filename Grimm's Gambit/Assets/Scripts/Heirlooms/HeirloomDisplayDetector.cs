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
    public Transform imageContainer;

    private GameObject currentTooltip;

    private void OnMouseEnter()
    {
        ShowTooltip(parentObject);
    }

    private void OnMouseExit()
    {
        RemoveTooltip();
    }

    //Functionality for tooltips themselves:
    private void ShowTooltip(GameObject target)
    {

        // Create a new GameObject for the tooltip
        GameObject tooltipObject = new GameObject("Tooltip");

        // Add it as a child to the parent canvas
        tooltipObject.transform.SetParent(imageContainer, false); //Uses parent of all heirloom display objects as the parent for this display

        // Add a RectTransform component for UI positioning
        RectTransform tooltipRect = tooltipObject.AddComponent<RectTransform>();
        tooltipRect.sizeDelta = new Vector2(14, 10); // Set size of the tooltip ~ possibly change this depending on how clear the text is

        TextMeshProUGUI tooltipText = tooltipObject.AddComponent<TextMeshProUGUI>();
        tooltipText.text = Description; //Creates text - REPLACE THIS WITH A SPECIFIED VALUE BASED ON HEIRLOOM
        tooltipText.fontSize = 2f;
        tooltipText.color = Color.red;
        tooltipText.font = PlaytestCheats.GetAllFont();

        // Store reference to the tooltip
        currentTooltip = tooltipObject;
    }

    private void RemoveTooltip()
    {
        if (currentTooltip != null)
        {
            Destroy(currentTooltip);
            currentTooltip = null;
        }
    }
}
