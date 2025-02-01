using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyAffixToLowestHPCard : SpellComponent
{
    [SerializeField] GenericDict<Affix, int> m_Affixes; 

    public ApplyAffixToLowestHPCard()
    {
        spellName = "Apply Affix to Low HP";
        spellDescription = "Apply an amount of an affix of your choice to the lowest HP party member.";
    }


    public override void DoSpellEffect()
    {
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

        target = friendlyParty[0].GetComponent<Minion>();

        foreach (Transform member in friendlyParty)
        {
            if (member.GetComponent<Minion>().currentHealth < target.currentHealth) target = member.GetComponent<Minion>();
        }

        for (int i = 0; i < m_Affixes.GetLength(); i++)
        {
            Affix key = m_Affixes.GetKey(i);
            target.AddAffix(key, m_Affixes.GetValue(key));
        }
    }
}
