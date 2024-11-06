using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEHeal : SpellComponent
{
    [SerializeField] int m_Amount = -3;

    public AOEHeal()
    {
        spellName = "Heal";
        spellDescription = "Heal an amount for the party.";
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
