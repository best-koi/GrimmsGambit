using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealSpell : SpellComponent
{
    public HealSpell()
    {
        this.spellName = "Heal";
        this.spellDescription = "Heal 3 health for the heir.";
    }
    public override void DoSpellEffect()
    {
        Debug.Log("Heal 3 Health");
    }
}
