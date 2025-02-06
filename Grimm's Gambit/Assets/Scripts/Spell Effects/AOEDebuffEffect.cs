using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AOEDebuffEffect : SpellEffect
{
    public bool TargetingFriendlies;
    public Affix debuff;
    public int value;

    public AOEDebuffEffect()
    {
        _spellName = "Heal";
        _spellDescription = "Heal an amount for the party.";
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


        if (TargetingFriendlies) //Version for when targetting friendly party
        {
            foreach (Transform member in friendlyParty)
            {
                target = member.GetComponent<Minion>();
                target.AddAffix(debuff, value);
            }
        }
        else //Condition for targetting enemy party
        {
            foreach (Transform member in hostileParty)
            {
                target = member.GetComponent<Minion>();
                target.AddAffix(debuff, value);
            }
        }
    }
}
