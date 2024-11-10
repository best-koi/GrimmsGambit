using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEDamage : SpellComponent
{
    [SerializeField] int m_Amount;

    public AOEDamage()
    {
        spellName = "AOE Attack";
        spellDescription = "Deal an amount of damage to the enemy party.";
    }
    public override void DoSpellEffect()
    {
        List<GameObject> party = FindObjectOfType<UnitParty>().GetAllMembers();

        foreach (GameObject member in party)
        {
            target = member.GetComponent<Minion>();
            target.DamageTaken(m_Amount);
        }
    }
}
