using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSelf : SpellComponent
{
    [SerializeField] private int damage;
    public DamageSelf()
    {
        spellName = "DamageSelf";
        spellDescription = "Deal an amount of damage to the caster of the spell.";
    }

    public void SetDamage(int dmgToSet)
    {
        damage = dmgToSet;
    }

    public override void DoSpellEffect()
    {
        caster.MinionUsed(caster, damage); 
    }
}
