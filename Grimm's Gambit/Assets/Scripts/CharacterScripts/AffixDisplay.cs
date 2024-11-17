//Ryan Lockie - 11/16/2024
//The thought process behind this script is that it will recieve signals whenever something is added or removed from the affix list, so that constant dictionary checks are not necessary
//The expectation is also that this script is generalized for anything to use it, but functions have been added to the minion script so that it will call the add affix and remove affix functions here
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class AffixDisplay : MonoBehaviour
{
    public Dictionary<Affix, Sprite> imageDictionary = new Dictionary<Affix, Sprite>();

    public Transform imageContainer; //Container for images to be stored within
    public int pixelWidth = 50; //Assumes default pixel with of visuals to be 50
    

    public void AddAffix(Affix newAffix) //Adds an affix to display
    {
        switch (newAffix) //Adds an image depending on the added affix to display
        {
            case Affix.Taunt:
                imageDictionary.Add(newAffix, Resources.Load<Sprite>("Art/AffixSprites/Taunt"));
                break;
            case Affix.Block:
                imageDictionary.Add(newAffix, Resources.Load<Sprite>("Art/AffixSprites/Block"));
                break;
            case Affix.Vulnerable:
                imageDictionary.Add(newAffix, Resources.Load<Sprite>("Art/AffixSprites/Vulnerable"));
                break;
            case Affix.DamageReduction:
                imageDictionary.Add(newAffix, Resources.Load<Sprite>("Art/AffixSprites/DamageReduction"));
                break;
            case Affix.Thorns:
                imageDictionary.Add(newAffix, Resources.Load<Sprite>("Art/AffixSprites/Thorns"));
                break;
            case Affix.Regen:
                imageDictionary.Add(newAffix, Resources.Load<Sprite>("Art/AffixSprites/Regen"));
                break;
            case Affix.Parasite:
                imageDictionary.Add(newAffix, Resources.Load<Sprite>("Art/AffixSprites/Parasite"));
                break;
            case Affix.Strength:
                imageDictionary.Add(newAffix, Resources.Load<Sprite>("Art/AffixSprites/Strength"));
                break;
            case Affix.Bleed:
                imageDictionary.Add(newAffix, Resources.Load<Sprite>("Art/AffixSprites/Bleed"));
                break;
            case Affix.Mark:
                imageDictionary.Add(newAffix, Resources.Load<Sprite>("Art/AffixSprites/Mark"));
                break;
            case Affix.HoundCounter:
                imageDictionary.Add(newAffix, Resources.Load<Sprite>("Art/AffixSprites/HoundCounter"));
                break;
            case Affix.Threaded:
                imageDictionary.Add(newAffix, Resources.Load<Sprite>("Art/AffixSprites/Threaded"));
                break;
            case Affix.Naturopath:
                imageDictionary.Add(newAffix, Resources.Load<Sprite>("Art/AffixSprites/Naturopath"));
                break;
            case Affix.Exploit:
                imageDictionary.Add(newAffix, Resources.Load<Sprite>("Art/AffixSprites/Exploit"));
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

            newSpriteObject.transform.SetParent(imageContainer, false); //Sets parent and moves world position of image to match parent

            RectTransform rectTransform = newSpriteObject.AddComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(pixelWidth, pixelWidth); //Creates transform to store image in

            SpriteRenderer imageComponent = newSpriteObject.AddComponent<SpriteRenderer>();
            if (affixImage.Value != null)
            {
                imageComponent.sprite = affixImage.Value; //sets sprite in sprite renderer
                //Possibly change color of spriteRenderer here
            }
        }
    }
}
