using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscardCard : SpellComponent
{
    public DiscardCard()
    {
        spellName = "Discard";
        spellDescription = "Discard's the leftmost card from the player's hand.";
    }

    public override void DoSpellEffect()
    {
        Deck deck = FindObjectOfType<Deck>();
        // To fix
        //deck.Discard();
    }
}
