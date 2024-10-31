using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageMinion : SpellComponent
{
    [SerializeField] private int damage;
    public DamageMinion()
    {
        spellName = "Damage";
        spellDescription = "Deal " + damage + " damage";
        requiresTarget = true;
    }

    

    public override void DoSpellEffect()
    {
        Debug.Log("Dealt " + damage + " damage");
    }
}
