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
        //The following assumes that only two parties exist and characters have properly labeled owners.
        //This is used to ensure that the friendly party is healed
        List<Transform> enemyParty;
        UnitParty[] parties = FindObjectsOfType<UnitParty>();
        List<Transform> currentParty = parties[0].GetAll();
        if (!currentParty[0].GetComponent<Minion>().ownerPlayer)
        {
            enemyParty = currentParty;
        }
        else
        {
            enemyParty = parties[1].GetAll();
        }
        foreach (Transform member in enemyParty)
        {
            target = member.GetComponent<Minion>();
            target.DamageTaken(m_Amount);
        }
    }
}
