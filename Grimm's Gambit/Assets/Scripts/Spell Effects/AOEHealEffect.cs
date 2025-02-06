using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AOEHealEffect : SpellEffect
{
    [SerializeField] int _amount = 3;

    public AOEHealEffect()
    {
        _spellName = "Heal";
        _spellDescription = "Heal an amount for the party.";
    }

    public override void DoSpellEffect(Minion caster, Minion target)
    {
        //The following assumes that only two parties exist and characters have properly labeled owners.
        //This is used to ensure that the friendly party is healed
        List<Transform> friendlyParty;
        UnitParty[] parties = GameObject.FindObjectsOfType<UnitParty>();
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
            target.DamageTaken(-_amount);
        }
    }
}
