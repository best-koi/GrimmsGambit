using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AOEDamageEffect : SpellEffect
{
    [SerializeField] int m_Amount;

    public AOEDamageEffect()
    {
        _spellName = "AOE Attack";
        _spellDescription = "Deal an amount of damage to the enemy party.";
    }

    public override void DoSpellEffect(Minion caster, Minion target)
    {
        //The following assumes that only two parties exist and characters have properly labeled owners.
        //This is used to ensure that the friendly party is healed

        /*
        List<Transform> enemyParty;
        UnitParty[] parties = GameObject.FindObjectsOfType<UnitParty>();
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
            Debug.Log(target);
            target.DamageTaken(m_Amount);
        }
        */

        UnitParty[] parties = GameObject.FindObjectsOfType<UnitParty>();
        Minion[] enemyParty = !target.ownerPlayer ? parties[1].GetComponentsInChildren<Minion>() : parties[0].GetComponentsInChildren<Minion>();

        foreach (Minion member in enemyParty)
        {
            Debug.Log(member);
            member.DamageTaken(m_Amount);
        }
    }
}
