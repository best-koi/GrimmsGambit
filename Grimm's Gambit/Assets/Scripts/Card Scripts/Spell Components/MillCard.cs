using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MillCard : SpellComponent
{
    [SerializeField] int m_Amount = 1;

    public MillCard()
    {
        spellName = "Mill";
        spellDescription = "Mills an amount of cards from the top of the deck.";
    }

    public override void DoSpellEffect()
    {
        Deck deck = FindObjectOfType<Deck>();
        deck.MillAmount(m_Amount);
    }
}
