using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealSpell : SpellComponent
{
    [SerializeField] private int m_Amount = 3;
    [SerializeField] private bool m_TargetingSelf = false;

    public HealSpell()
    {
        spellName = "Heal";
        spellDescription = "Heal an amount for a target.";

        if(!m_TargetingSelf) requiresTarget = true;
    }
    public override void DoSpellEffect()
    {
        if (m_TargetingSelf) caster.DamageTaken(-m_Amount);
        else target.DamageTaken(-m_Amount); 
    }
}
