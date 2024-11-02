using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conjure : SpellComponent
{
    [SerializeField] Card m_Conjured;

    public Conjure()
    {
        spellName = "Conjure";
        spellDescription = "Conjures a card from the database into the hand.";
    }

    public override void DoSpellEffect()
    {
        Deck deck = FindObjectOfType<Deck>();
        deck.Conjure(m_Conjured.GetIndex());
    }
}
