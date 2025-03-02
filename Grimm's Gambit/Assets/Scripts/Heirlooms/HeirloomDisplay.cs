//Ryan Lockie - 11/16/2024
//The thought process behind this script is that it will recieve signals whenever something is added or removed from the heirloom list, so that constant dictionary checks are not necessary
//The expectation is also that this script is generalized for anything to use it, but functions have been added to the minion script so that it will call the add heirloom and remove heirloom functions here
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;


//using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.EventSystems;

public class HeirloomDisplay : MonoBehaviour
{
    public Dictionary<Heirloom, Sprite> imageDictionary = new Dictionary<Heirloom, Sprite>();
    public Dictionary<Heirloom, string> stringDictionary = new Dictionary<Heirloom, string>();
    private HeirloomImageLibrary heirloomImageLibrary;

    public Transform imageContainer; //Container for images to be stored within
    public int pixelWidth = 5; //Assumes default pixel with of visuals to be 50

    //Prefab references for mouseover window - must contain:
    public GameObject tooltipPrefab;
    public Canvas parentCanvas;
    //Identical to display code for affixes:
    public UnityEngine.UI.Image hoverOverImage;
    public TextMeshProUGUI hoverOverText;
    
    public void Awake()
    {
        UnityEngine.UI.Image[] images = FindObjectsOfType<UnityEngine.UI.Image>();
        foreach (UnityEngine.UI.Image image in images)
        {
            if (image.gameObject.name == "AffixImage")
            {
                hoverOverImage = image;
                break;
            }
        }
        TextMeshProUGUI[] texts = FindObjectsOfType<TextMeshProUGUI>();
        foreach (TextMeshProUGUI text in texts)
        {
            if (text.gameObject.name == "AffixDescription")
            {
                hoverOverText = text;
                break;
            }
        }
    }
    
    //Display object is not disabled since the affix display already accomplishes this

    public void AddHeirloom(Heirloom newHeirloom) //Adds an heirloom to display
    {
        if (heirloomImageLibrary == null)
        {
            heirloomImageLibrary = FindAnyObjectByType<HeirloomImageLibrary>();
        }
        switch (newHeirloom) //Adds an image depending on the added heirloom to display
        {
            case Heirloom.Hamelin:
                imageDictionary.Add(newHeirloom, heirloomImageLibrary.spriteLibrary[0]);
                stringDictionary.Add(newHeirloom, "Flute of Hamelin - Gain one more maximum spirit.");
                break;
            case Heirloom.Carnation:
                imageDictionary.Add(newHeirloom, heirloomImageLibrary.spriteLibrary[1]);
                stringDictionary.Add(newHeirloom, "Pink Carnation - Applies one more debuff stack when applying debuffs.");
                break;
            case Heirloom.Jar:
                imageDictionary.Add(newHeirloom, heirloomImageLibrary.spriteLibrary[2]);
                stringDictionary.Add(newHeirloom, "Greedy Jar - At the start of combat, draw 2 more cards.");
                break;
            case Heirloom.Silk:
                imageDictionary.Add(newHeirloom, heirloomImageLibrary.spriteLibrary[3]);
                stringDictionary.Add(newHeirloom, "Faded Gold Silk - The first card played for a party member costs 0 in combat.");
                break;
            case Heirloom.Blindfold:
                imageDictionary.Add(newHeirloom, heirloomImageLibrary.spriteLibrary[4]);
                stringDictionary.Add(newHeirloom, "Bloody Blindfold - Enemy intents will be hidden.");
                break;
            case Heirloom.Miracle:
                imageDictionary.Add(newHeirloom, heirloomImageLibrary.spriteLibrary[5]);
                stringDictionary.Add(newHeirloom, "Miracle Water - If a party member were to take fatal damage, use this water to nullify that hit.");
                break;
            case Heirloom.Lycan:
                imageDictionary.Add(newHeirloom, heirloomImageLibrary.spriteLibrary[6]);
                stringDictionary.Add(newHeirloom, "Lycan's Fang - Scratch cards now deal 2 more damage and apply 1 more Bleed.");
                break;
            case Heirloom.Serpent:
                imageDictionary.Add(newHeirloom, heirloomImageLibrary.spriteLibrary[7]);
                stringDictionary.Add(newHeirloom, "Serpent Ring - Gain 1 Strength this combat if the Hound has given at least 12 Block in a turn.");
                break;
            case Heirloom.Sweater:
                imageDictionary.Add(newHeirloom, heirloomImageLibrary.spriteLibrary[8]);
                stringDictionary.Add(newHeirloom, "Durable Sweater - The Seamstress gains 4 Block at the end of a turn.");
                break;
            default:
                break;
        }
        UpdateVisuals();
    }

    public void RemoveHeirloom(Heirloom removedHeirloom) //Removes an heirloom from display
    {
        imageDictionary.Remove(removedHeirloom); //Removes visual from dictionary and updates visual display
        stringDictionary.Remove(removedHeirloom); //Need to do this to prevent null error
        UpdateVisuals(); //Still called when character is destroyed, so null check is needed at the start of it
    }

    private void UpdateVisuals() //This function updates visuals every time an heirloom is added or removed
    {
        if(imageContainer == null)
        {
            return; //Skips function if character has been destroyed
        }
        //UI code for displaying the imageDictionary should go here:
        foreach (Transform child in imageContainer)
        {
            Destroy(child.gameObject); //Destroys current instances of images before creating new ones and reformatting
        }
        int index = 0; //Used for visual formatting
        int imagesPerRow = 4;
        foreach (var heirloomImage in imageDictionary)
        {
            GameObject newSpriteObject = new GameObject(heirloomImage.Key.ToString()); //Labels object for image with the name of its' heirloom

            newSpriteObject.transform.SetParent(imageContainer, false); //Uses parent of all heirloom display objects as the parent for this display

            RectTransform rectTransform = newSpriteObject.AddComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(pixelWidth, pixelWidth); //Creates transform to store image in

            int row = index/imagesPerRow; //Calculates row number
            int column = index % imagesPerRow; //Calculates column number
            rectTransform.anchoredPosition = new Vector2(column * .33f, -row * .33f); //Shifts by .33, adjust value if needed
            index++; //increments index for next entry
            
            SpriteRenderer imageComponent = newSpriteObject.AddComponent<SpriteRenderer>();
            if (heirloomImage.Value != null)
            {
                imageComponent.sprite = heirloomImage.Value; //sets sprite in sprite renderer
                //Possibly change color of spriteRenderer here
            }

            //Adds a collider for mouseover detection
            BoxCollider2D boxCollider = newSpriteObject.AddComponent<BoxCollider2D>();
            boxCollider.isTrigger = true; //Sets collider as a trigger so on mouse enter/exit can function properly

            //Adds script that manages mouseover information
            HeirloomDisplayDetector detector = newSpriteObject.AddComponent<HeirloomDisplayDetector>();
            detector.parentObject = newSpriteObject;
            detector.imageContainer = imageContainer;
            detector.Description = stringDictionary[heirloomImage.Key]; //Uses specific heirloom description
            detector.displayImageLocation = hoverOverImage;
            detector.displayTextLocation = hoverOverText;
            detector.Image = heirloomImage.Value;
        }
    }

    
}
