using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conjure : SpellComponent
{
    [SerializeField] Card m_Conjured;
    [SerializeField] int m_Amount;

    public Conjure()
    {
        spellName = "Conjure";
        spellDescription = "Conjures an amount of cards from the database into the hand.";
    }

    public override void DoSpellEffect()
    {
        Deck deck = FindObjectOfType<Deck>();
        for (int i = 0; i < m_Amount; i++)
        {
            deck.Conjure(m_Conjured.GetData());
        }
    }
}
