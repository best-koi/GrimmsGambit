using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Rudimentary test of a card performing spell actions

[RequireComponent(typeof(HealSpell))]
public class HighPriestess : Card
{
    private HealSpell healspell;
    void Start()
    {
        healspell = GetComponent<HealSpell>();
    }
    public HighPriestess()
    {
        this.cardName = "High Priestess";
        this.cardCost = 3;
    }
}
