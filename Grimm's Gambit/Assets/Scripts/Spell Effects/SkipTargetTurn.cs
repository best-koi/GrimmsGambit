using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkipTargetTurn : SpellEffect
{

    public SkipTargetTurn()
    {
        _spellName = "SkipTargetTurn";
        _spellDescription = "Skips the target's turn";
    }

    public override void DoSpellEffect(Minion caster, Minion target)
    {
        //The following assumes that only two parties exist and characters have properly labeled owners.
        //This is used to register both the friendly and enemy parties for aoe usage
        List<Transform> friendlyParty, hostileParty;
        UnitParty[] parties = GameObject.FindObjectsOfType<UnitParty>();
        List<Transform> currentParty = parties[0].GetAll();
        if (currentParty[0].GetComponent<Minion>().ownerPlayer == true)
        {
            friendlyParty = currentParty;
            hostileParty = parties[1].GetAll();
        }
        else
        {
            hostileParty = currentParty;
            friendlyParty = parties[1].GetAll();
        }

        foreach (Transform member in hostileParty) //Sets new targets when necessary
        {
            Minion currentMinion = member.GetComponent<Minion>();
            if (currentMinion == target)
            {
                EnemyTemplate currentEnemy = member.GetComponent<EnemyTemplate>();
                currentEnemy.SkipTurn(); //Skips the turn of using the enemy template of the enemy who matches the target of this spell
            }
        }
    }
}
