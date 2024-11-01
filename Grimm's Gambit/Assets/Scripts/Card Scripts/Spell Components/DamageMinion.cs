using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageMinion : SpellComponent
{
    [SerializeField] private int damage;
    public DamageMinion()
    {
        requiresTarget = true;
    }

    public void SetDamage(int dmgToSet)
    {
        damage = dmgToSet;
    }

    public override void DoSpellEffect()
    {
        //target.DamageTaken(damage);
        //Updated Version Added by Ryan - 11/1/2024:
        caster.MinionUsed(target, damage); //deals damage to target character from the caster character
    }
}
