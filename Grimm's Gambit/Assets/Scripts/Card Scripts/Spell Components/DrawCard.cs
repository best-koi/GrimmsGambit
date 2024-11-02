using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCard : SpellComponent
{
    public DrawCard()
    {
        spellName = "Draw";
        spellDescription = "Draw a random card from the deck.";
    }

    public override void DoSpellEffect()
    {
        Deck deck = FindObjectOfType<Deck>();
        deck.Draw();
    }
}
