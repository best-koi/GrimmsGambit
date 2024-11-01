using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealSpell : SpellComponent
{
    public HealSpell()
    {
        spellName = "Heal";
        spellDescription = "Heal 3 health for the heir.";
    }
    public override void DoSpellEffect()
    {
        //Debug.Log("Heal 3 Health");
        //Updated Version Added by Ryan - 11/1/2024:
        target.DamageTaken(-3); //heals character for 3 health
    }
}
