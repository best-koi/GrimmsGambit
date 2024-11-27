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
        //Debugging:
        print("Mouseover Active");

        // Create a new GameObject for the tooltip
        GameObject tooltipObject = new GameObject("Tooltip");

        // Add it as a child to the parent canvas
        tooltipObject.transform.SetParent(imageContainer, false); //Uses parent of all affix display objects as the parent for this display

        // Add a RectTransform component for UI positioning
        RectTransform tooltipRect = tooltipObject.AddComponent<RectTransform>();
        tooltipRect.sizeDelta = new Vector2(10, 10); // Set size of the tooltip ~ possibly change this depending on how clear the text is

        // Add a Text component for the tooltip text
        GameObject textObject = new GameObject("TooltipText");
        textObject.transform.SetParent(tooltipRect, false);

        TextMeshProUGUI tooltipText = textObject.AddComponent<TextMeshProUGUI>();
        tooltipText.text = Description; //Creates text - REPLACE THIS WITH A SPECIFIED VALUE BASED ON AFFIX
        tooltipText.fontSize = 14;
        tooltipText.color = Color.white;

        // Store reference to the tooltip
        currentTooltip = tooltipObject;
    }

    private void RemoveTooltip()
    {
        //Debugging:
        print("Mouseover Inactive");
        if (currentTooltip != null)
        {
            Destroy(currentTooltip);
            currentTooltip = null;
        }
    }
}
