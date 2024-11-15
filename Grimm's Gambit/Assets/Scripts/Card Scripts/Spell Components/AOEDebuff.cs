using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEDebuff : SpellComponent
{
    public bool TargetingFriendlies;
    public Affix[] debuffs;
    public int[] values;

    public AOEDebuff()
    {
        spellName = "Heal";
        spellDescription = "Heal an amount for the party.";
    }
    public override void DoSpellEffect()
    {
        //The following assumes that only two parties exist and characters have properly labeled owners.
        //This is used to register both the friendly and enemy parties for aoe usage
        List<Transform> friendlyParty, hostileParty;
        UnitParty[] parties = FindObjectsOfType<UnitParty>();
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
                for(int i = 0; i < debuffs.Length; i++) 
                {
                    target.AddAffix(debuffs[i], values[i]);
                }
            }
        }
        else //Condition for targetting enemy party
        {
            foreach (Transform member in hostileParty)
            {
                target = member.GetComponent<Minion>();
                for(int i = 0; i < debuffs.Length; i++) 
                {
                    target.AddAffix(debuffs[i], values[i]);
                }
            }
        }
    }
}
