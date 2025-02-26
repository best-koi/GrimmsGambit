//Ryan Lockie - 11/16/2024
//The thought process behind this script is that it will recieve signals whenever something is added or removed from the affix list, so that constant dictionary checks are not necessary
//The expectation is also that this script is generalized for anything to use it, but functions have been added to the minion script so that it will call the add affix and remove affix functions here
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
//using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;


//using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AffixDisplay : MonoBehaviour
{
    public Dictionary<Affix, Sprite> imageDictionary = new Dictionary<Affix, Sprite>();
    public Dictionary<Affix, string> stringDictionary = new Dictionary<Affix, string>();
    public Dictionary<Affix, int> stackDictionary = new Dictionary<Affix, int>(); //Used to store stack quantities of each affix
    private AffixImageLibrary affixImageLibrary;

    public Transform imageContainer; //Container for images to be stored within
    public int pixelWidth = 5; //Assumes default pixel with of visuals to be 50

    //Prefab references for mouseover window - must contain:
    public GameObject tooltipPrefab;
    public Canvas parentCanvas;
    public UnityEngine.UI.Image hoverOverImage;
    public TextMeshProUGUI hoverOverText;
    public GameObject affixDescriptionBox; // added by danielle
    
    public void Start()
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
        // added by danielle; to find the affix UI box and show/hide on hover
        GameObject[] objects = FindObjectsOfType<GameObject>();
        foreach (GameObject obj in objects)
        {
            if (obj.gameObject.name == "AffixDescriptionBox")
            {
                affixDescriptionBox = obj;
                Debug.Log("found");
                affixDescriptionBox.SetActive(false);
                break;
            }
        }

    }

    public void AddAffix(Affix newAffix, int currentStacks) //Adds an affix to display
    {
        if (affixImageLibrary == null)
        {
            affixImageLibrary = FindAnyObjectByType<AffixImageLibrary>();
        }
        stackDictionary.Add(newAffix, currentStacks);
        switch (newAffix) //Adds an image depending on the added affix to display
        {
            case Affix.Taunt:
                imageDictionary.Add(newAffix, affixImageLibrary.spriteLibrary[0]);
                stringDictionary.Add(newAffix, "Taunt - Attracts enemy aggression.");
                break;
            case Affix.Block:
                imageDictionary.Add(newAffix, affixImageLibrary.spriteLibrary[1]);
                stringDictionary.Add(newAffix, "Block - Blocks a set amount of damage based on the amount of stacks. Removed at the end of turn.");
                break;
            case Affix.Vulnerable:
                imageDictionary.Add(newAffix, affixImageLibrary.spriteLibrary[2]);
                stringDictionary.Add(newAffix, "Vulnerable - Increases damage taken.");
                break;
            case Affix.DamageReduction:
                imageDictionary.Add(newAffix, affixImageLibrary.spriteLibrary[3]);
                stringDictionary.Add(newAffix, "DamageReduction - Reduces damage dealt.");
                break;
            case Affix.Thorns:
                imageDictionary.Add(newAffix, affixImageLibrary.spriteLibrary[4]);
                stringDictionary.Add(newAffix, "Thorns - deals knockback damage.");
                break;
            case Affix.Regen:
                imageDictionary.Add(newAffix, affixImageLibrary.spriteLibrary[5]);
                stringDictionary.Add(newAffix, "Regen - heals at the end of each turn.");
                break;
            case Affix.Parasite:
                imageDictionary.Add(newAffix, affixImageLibrary.spriteLibrary[6]);
                stringDictionary.Add(newAffix, "Parasite - when attacking this creature, health will be gained.");
                break;
            case Affix.Strength:
                imageDictionary.Add(newAffix, affixImageLibrary.spriteLibrary[7]);
                stringDictionary.Add(newAffix, "Strength - increases damage dealt on a stacking basis.");
                break;
            case Affix.Bleed:
                imageDictionary.Add(newAffix, affixImageLibrary.spriteLibrary[8]);
                stringDictionary.Add(newAffix, "Bleed - On unit activation, quantity of bleed stacks taken as damage.");
                break;
            case Affix.Mark:
                imageDictionary.Add(newAffix, affixImageLibrary.spriteLibrary[9]);
                stringDictionary.Add(newAffix, "Mark - increased damage per attack dealt to this character.");
                break;
            case Affix.HoundCounter:
                imageDictionary.Add(newAffix, affixImageLibrary.spriteLibrary[10]);
                stringDictionary.Add(newAffix, "HoundCounter - when this character is attacked, the attacker will take half damage in return.");
                break;
            case Affix.Threaded:
                imageDictionary.Add(newAffix, affixImageLibrary.spriteLibrary[11]);
                stringDictionary.Add(newAffix, "Threaded - character's turn will be skipped.");
                break;
            case Affix.Naturopath:
                imageDictionary.Add(newAffix, affixImageLibrary.spriteLibrary[12]);
                stringDictionary.Add(newAffix, "Naturopath - Holder adds the number of stacks to their heal.");
                break;
            case Affix.Exploit:
                imageDictionary.Add(newAffix, affixImageLibrary.spriteLibrary[13]);
                stringDictionary.Add(newAffix, "Exploit - Adds number of stacks to the damage of the next hit. Half of stacks rounded down removed on hit.");
                break;
            case Affix.Curse:
                imageDictionary.Add(newAffix, affixImageLibrary.spriteLibrary[6]); //Using parasite image
                stringDictionary.Add(newAffix, "Curse - Takes damage equal to number of stacks, incrementing by one each turn.");
                break;
            case Affix.PowerBurst:
                imageDictionary.Add(newAffix, affixImageLibrary.spriteLibrary[7]); //Using strength image
                stringDictionary.Add(newAffix, "Power Burst - Increases damage by the number of stacks, decreasing by one each turn.");
                break;
            default:
                break;
        }
        UpdateVisuals();
    }

    public void UpdateStacks(Affix updatedAffix, int currentStacks)
    {
        stackDictionary.Remove(updatedAffix);
        stackDictionary.Add(updatedAffix, currentStacks); //Replaces affix with new value of stacks
        UpdateVisuals(); //Not optional if you want to display stacks
    }

    public void RemoveAffix(Affix removedAffix) //Removes an affix from display
    {
        imageDictionary.Remove(removedAffix); //Removes visual from dictionary and updates visual display
        stackDictionary.Remove(removedAffix); //Removes all stacks of a removed affix
        stringDictionary.Remove(removedAffix); //Need to do this to prevent null error
        UpdateVisuals(); //Still called when character is destroyed, so null check is needed at the start of it
    }

    private void UpdateVisuals() //This function updates visuals every time an affix is added or removed
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
        foreach (var affixImage in imageDictionary)
        {
            GameObject newSpriteObject = new GameObject(affixImage.Key.ToString()); //Labels object for image with the name of its' affix

            newSpriteObject.transform.SetParent(imageContainer, false); //Uses parent of all affix display objects as the parent for this display

            RectTransform rectTransform = newSpriteObject.AddComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(pixelWidth, pixelWidth); //Creates transform to store image in

            int row = index/imagesPerRow; //Calculates row number
            int column = index % imagesPerRow; //Calculates column number
            rectTransform.anchoredPosition = new Vector2(column * .33f, -row * .33f); //Shifts by .33, adjust value if needed
            index++; //increments index for next entry
            
            SpriteRenderer imageComponent = newSpriteObject.AddComponent<SpriteRenderer>();
            if (affixImage.Value != null)
            {
                imageComponent.sprite = affixImage.Value; //sets sprite in sprite renderer
                //Possibly change color of spriteRenderer here
            }

            //Adds a collider for mouseover detection
            BoxCollider2D boxCollider = newSpriteObject.AddComponent<BoxCollider2D>();
            boxCollider.isTrigger = true; //Sets collider as a trigger so on mouse enter/exit can function properly

            //Adds script that manages mouseover information
            AffixDisplayDetector detector = newSpriteObject.AddComponent<AffixDisplayDetector>();
            detector.parentObject = newSpriteObject;
            detector.Image = affixImage.Value;
            detector.displayImageLocation = hoverOverImage;
            detector.displayTextLocation = hoverOverText;
            detector.Description = stringDictionary[affixImage.Key]; //Uses specific affix description
            detector.Stacks = stackDictionary[affixImage.Key]; //Uses specific stack count
            detector.affixDisplay = affixDescriptionBox; // added by danielle

        }
    }

    
}
