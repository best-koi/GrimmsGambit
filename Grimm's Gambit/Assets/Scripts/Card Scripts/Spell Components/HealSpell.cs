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
        caster.MinionUsed(target, -3); //deals damage to target character from the caster character
    }
}
