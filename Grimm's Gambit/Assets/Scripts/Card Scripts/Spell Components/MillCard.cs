using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MillCard : SpellComponent
{
    public MillCard()
    {
        spellName = "Mill";
        spellDescription = "Mills a card from the top of the deck.";
    }

    public override void DoSpellEffect()
    {
        Deck deck = FindObjectOfType<Deck>();
        deck.Mill();
    }
}
