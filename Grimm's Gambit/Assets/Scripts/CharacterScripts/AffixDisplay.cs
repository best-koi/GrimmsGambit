//Ryan Lockie - 11/16/2024
//The thought process behind this script is that it will recieve signals whenever something is added or removed from the affix list, so that constant dictionary checks are not necessary
//The expectation is also that this script is generalized for anything to use it, but functions have been added to the minion script so that it will call the add affix and remove affix functions here
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;

//using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.EventSystems;

public class AffixDisplay : MonoBehaviour
{
    public Dictionary<Affix, Sprite> imageDictionary = new Dictionary<Affix, Sprite>();
    private AffixImageLibrary affixImageLibrary;

    public Transform imageContainer; //Container for images to be stored within
    public int pixelWidth = 5; //Assumes default pixel with of visuals to be 50

    //Prefab references for mouseover window - must contain:
    public GameObject tooltipPrefab;
    public Canvas parentCanvas;
    private GameObject currentTooltip;
    
    
    public void AddAffix(Affix newAffix) //Adds an affix to display
    {
        if (affixImageLibrary == null)
        {
            affixImageLibrary = FindAnyObjectByType<AffixImageLibrary>();
        }
        switch (newAffix) //Adds an image depending on the added affix to display
        {
            case Affix.Taunt:
                imageDictionary.Add(newAffix, affixImageLibrary.spriteLibrary[0]);
                break;
            case Affix.Block:
                imageDictionary.Add(newAffix, affixImageLibrary.spriteLibrary[1]);
                break;
            case Affix.Vulnerable:
                imageDictionary.Add(newAffix, affixImageLibrary.spriteLibrary[2]);
                break;
            case Affix.DamageReduction:
                imageDictionary.Add(newAffix, affixImageLibrary.spriteLibrary[3]);
                break;
            case Affix.Thorns:
                imageDictionary.Add(newAffix, affixImageLibrary.spriteLibrary[4]);
                break;
            case Affix.Regen:
                imageDictionary.Add(newAffix, affixImageLibrary.spriteLibrary[5]);
                break;
            case Affix.Parasite:
                imageDictionary.Add(newAffix, affixImageLibrary.spriteLibrary[6]);
                break;
            case Affix.Strength:
                imageDictionary.Add(newAffix, affixImageLibrary.spriteLibrary[7]);
                break;
            case Affix.Bleed:
                imageDictionary.Add(newAffix, affixImageLibrary.spriteLibrary[8]);
                break;
            case Affix.Mark:
                imageDictionary.Add(newAffix, affixImageLibrary.spriteLibrary[9]);
                break;
            case Affix.HoundCounter:
                imageDictionary.Add(newAffix, affixImageLibrary.spriteLibrary[10]);
                break;
            case Affix.Threaded:
                imageDictionary.Add(newAffix, affixImageLibrary.spriteLibrary[11]);
                break;
            case Affix.Naturopath:
                imageDictionary.Add(newAffix, affixImageLibrary.spriteLibrary[12]);
                break;
            case Affix.Exploit:
                imageDictionary.Add(newAffix, affixImageLibrary.spriteLibrary[13]);
                break;
            default:
                break;
        }
        UpdateVisuals();
    }

    public void RemoveAffix(Affix removedAffix) //Removes an affix from display
    {
        imageDictionary.Remove(removedAffix); //Removes visual from dictionary and updates visual display
        UpdateVisuals();
    }

    private void UpdateVisuals() //This function updates visuals every time an affix is added or removed
    {
        //UI code for displaying the imageDictionary should go here:
        foreach (Transform child in imageContainer)
        {
            Destroy(child.gameObject); //Destroys current instances of images before creating new ones and reformatting
        }

        foreach (var affixImage in imageDictionary)
        {
            GameObject newSpriteObject = new GameObject(affixImage.Key.ToString()); //Labels object for image with the name of its' affix

            newSpriteObject.transform.SetParent(imageContainer, false); //Uses parent of all affix display objects as the parent for this display

            RectTransform rectTransform = newSpriteObject.AddComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(pixelWidth, pixelWidth); //Creates transform to store image in

            SpriteRenderer imageComponent = newSpriteObject.AddComponent<SpriteRenderer>();
            if (affixImage.Value != null)
            {
                imageComponent.sprite = affixImage.Value; //sets sprite in sprite renderer
                //Possibly change color of spriteRenderer here
            }

            //Code for mouseover text descriptions: - test and debug the following
            EventTrigger hoverEvent = newSpriteObject.AddComponent<EventTrigger>(); //Creates event detector

            //THE FOLLOWING PORTION DOES NOT SEEM TO WORK - NEED TO LOOK INTO OTHER WAYS TO USE EVENT TRIGGER FOR UNITY
            //IDEA FOR LATER: CREATE A SCRIPT THAT CAN BE ADDED TO EVERY newSpriteObject which has OnMouseEnter() and OnMouseExit() and also add a collider to the ui object so this works properly
            EventTrigger.Entry hoverEnter = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerEnter
            };
            hoverEnter.callback.AddListener((data) => ShowTooltip(newSpriteObject));
            hoverEvent.triggers.Add(hoverEnter); //Adds listener to showtooltip function for hovering over the button

            EventTrigger.Entry hoverExit = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerExit
            };
            hoverExit.callback.AddListener((data) => RemoveTooltip());
            hoverEvent.triggers.Add(hoverExit); //Adds listener to removetooltip function for when mouseover ends
        }
    }

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
        tooltipRect.sizeDelta = new Vector2(200, 50); // Set size of the tooltip ~ possibly change this depending on how clear the text is

        // Add a Text component for the tooltip text
        GameObject textObject = new GameObject("TooltipText");
        textObject.transform.SetParent(tooltipObject.transform, false);

        TextMeshProUGUI tooltipText = textObject.AddComponent<TextMeshProUGUI>();
        tooltipText.text = "chungus"; //Creates text - REPLACE THIS WITH A SPECIFIED VALUE BASED ON AFFIX
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
