using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyAffixToLowestHPCard : SpellComponent
{
    public Affix[] debuffs;
    public int[] values;

    public ApplyAffixToLowestHPCard()
    {
        spellName = "Apply Affix to Low HP";
        spellDescription = "Apply an amount of an affix of your choice to the lowest HP party member.";
    }


    public override void DoSpellEffect()
    {
        List<GameObject> friendlyParty;
        UnitParty[] parties = FindObjectsOfType<UnitParty>();
        List<GameObject> currentParty = parties[0].GetAllMembers();
        if (currentParty[0].GetComponent<Minion>().ownerPlayer == true)
        {
            friendlyParty = currentParty;
        }
        else
        {
            friendlyParty = parties[1].GetAllMembers();
        }

        target = friendlyParty[0].GetComponent<Minion>();

        foreach (GameObject member in friendlyParty)
        {
            if (member.GetComponent<Minion>().currentHealth < target.currentHealth) target = member.GetComponent<Minion>();
        }

        for (int i = 0; i < debuffs.Length; i++)
        {
            target.AddAffix(debuffs[i], values[i]);
        }
    }
}
