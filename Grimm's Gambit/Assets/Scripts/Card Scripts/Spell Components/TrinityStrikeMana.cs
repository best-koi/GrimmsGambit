using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrinityStrikeMana : SpellComponent
{
    //This workaround refunds mana when cost should be lowered so I can avoid having to adjust the original card object - there is likely a better way to do this
    public override void DoSpellEffect() //Causes the player to become tired (function currently only functional for player being tired since ai attacks are controlled without spirit)
    {
        Deck deck = FindObjectOfType<Deck>();
        int cardsUsed = deck.m_MaxCountThisTurn - deck.CurrentCardCount(); //Calculates cards used this round
        EncounterController encounterController = FindObjectOfType<EncounterController>();
        if (cardsUsed == 2)
        {   
            encounterController.SpendResources(-3); //Refunds mana if exactly two cards have been used before this
        }
    }
}
