using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; //Used for random

public class ConjureRandomKatzeCard : SpellComponent
{
    [SerializeField] int m_Amount;

    public ConjureRandomKatzeCard()
    {
        spellName = "ConjureRandomKatzeCard";
        spellDescription = "Conjures an amount of cards from the database into the hand, randomized from katze's database.";
    }

    public override void DoSpellEffect()
    {
        Deck deck = FindObjectOfType<Deck>();
        CardDatabase database = FindObjectOfType<CardDatabase>();
        System.Random random = new System.Random();
        int randomValue = random.Next(0, 18);
        Card m_Conjured = database.GetCard(2, randomValue);
        for (int i = 0; i < m_Amount; i++)
        {
            deck.Conjure(m_Conjured.GetData());
        }
    }
}
