using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEHeal : SpellComponent
{
    [SerializeField] int m_Amount = 3;

    public AOEHeal()
    {
        spellName = "Heal";
        spellDescription = "Heal an amount for the party.";
    }
    public override void DoSpellEffect()
    {
        //The following assumes that only two parties exist and characters have properly labeled owners.
        //This is used to ensure that the friendly party is healed
        List<Transform> friendlyParty;
        UnitParty[] parties = FindObjectsOfType<UnitParty>();
        List<Transform> currentParty = parties[0].GetAll();   
        if (currentParty[0].GetComponent<Minion>().ownerPlayer == true)
        {
            friendlyParty = currentParty;
        }
        else
        {
            friendlyParty = parties[1].GetAll();
        }
        foreach (Transform member in friendlyParty)
        {
            target = member.GetComponent<Minion>();
            target.DamageTaken(-m_Amount);
        }
    }
}
