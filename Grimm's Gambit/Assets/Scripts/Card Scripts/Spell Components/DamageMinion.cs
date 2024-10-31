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
        target.DamageTaken(damage);
    }
}
