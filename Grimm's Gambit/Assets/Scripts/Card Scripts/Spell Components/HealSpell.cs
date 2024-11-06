using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealSpell : SpellComponent
{
    [SerializeField] int m_Amount = -3;

    public HealSpell()
    {
        spellName = "Heal";
        spellDescription = "Heal an amount for a target.";
    }
    public override void DoSpellEffect()
    {
        target.DamageTaken(m_Amount); 
    }
}
