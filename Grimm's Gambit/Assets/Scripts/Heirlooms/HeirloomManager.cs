//Created by Ryan Lockie 2/2/2025
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum Heirloom
{
    Hamelin, //Flute of Hamelin - Plus 1 Maximum Spirit and Spirit gain on turn start
    Carnation, //Pink Carnation - Applies an additional debuff when applying debuffs - currently implemented in the minion.AddAffix 2/2/25
    Jar, //Greedy Jar - At the start of combat, draw 2 more cards
    Silk, //Faded Gold Silk - The first played card for a party member costs 0 in a combat
    Blindfold, //Bloody Blindfold - Enemy Intents are hidden
    Miracle, //Miracle Water - One time use that nullifies the hit which would deal fatal damage to a party member 
    Lycan, //Lycan's Fang - Scratch cards now deal 2 more damage and apply 1 more bleed - Implemented in ScratchEffect.cs 2/2/25
    Serpent, //Serpent Ring - Gain 1 Strength this combat if the Hound has given at least 12 Block in a turn
    Sweater //Durable Sweater - The Seamstress gains 4 Block at the end of a turn
}

public class HeirloomManager : MonoBehaviour
{
    private List<Heirloom> currentHeirlooms = new List<Heirloom>(); //List to control current heirlooms
    public HeirloomDisplay heirloomDisplay;
    
    private void Awake()
    {
        AddHeirloom(Heirloom.Lycan);
        AddHeirloom(Heirloom.Hamelin);
        heirloomDisplay = FindObjectOfType<HeirloomDisplay>();
    }
    
    public bool ContainsHeirloom(Heirloom heirloom)//Returns whether the list contains the heirloom
    {
        return currentHeirlooms.Contains(heirloom);
    }

    public void AddHeirloom(Heirloom heirloom) //Adds an heirloom
    {
        if (!currentHeirlooms.Contains(heirloom))
        {
            currentHeirlooms.Add(heirloom);
            if (heirloomDisplay)
            {
                heirloomDisplay.AddHeirloom(heirloom); //For visual aspect
            }
        }
    }

    public void RemoveHeirloom(Heirloom heirloom) //Removes an heirloom
    {
        if (currentHeirlooms.Contains(heirloom))
        {
            currentHeirlooms.Remove(heirloom);
            if (heirloomDisplay)
            {
                heirloomDisplay.RemoveHeirloom(heirloom); //For visual aspect
            }
        }
    }

    // Used by Shop
    public int GetNumHeirlooms()
    {
        return Enum.GetValues(typeof(Heirloom)).Length;
    }

    // Used by Shop Display 
    public List<Heirloom> GetHeirlooms()
    {
        return currentHeirlooms;
    }

    public void SetHeirlooms(List<Heirloom> heirlooms) //Adjusted by ryan to properly update heirloom display
    {
        currentHeirlooms = heirlooms;
        foreach (Heirloom heirloom in heirlooms)
        {
            AddHeirloom(heirloom); //Considers visual aspect as well
        }
    }
}
