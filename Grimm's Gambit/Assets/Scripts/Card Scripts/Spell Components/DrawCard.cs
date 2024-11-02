using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCard : SpellComponent
{
    [SerializeField] int m_Amount = 1;

    public DrawCard()
    {
        spellName = "Draw";
        spellDescription = "Draw an amount of cards from the deck.";
    }

    public override void DoSpellEffect()
    {
        Deck deck = FindObjectOfType<Deck>();
        deck.DrawAmount(false, m_Amount);
    }
}
