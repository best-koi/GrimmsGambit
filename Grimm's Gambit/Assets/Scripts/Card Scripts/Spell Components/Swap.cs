using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swap : SpellComponent
{
    public Swap()
    {
        spellName = "Swap";
        spellDescription = "When this card is played, position is swapped with the target character.";
    }
    public override void DoSpellEffect() //Swaps positions of caster and target
    {
        UnitParty friendlyUnitParty;
        UnitParty[] parties = FindObjectsOfType<UnitParty>();
        List<GameObject> currentParty = parties[0].GetAllMembers();
        if (currentParty[0].GetComponent<Minion>().ownerPlayer == true)
            friendlyUnitParty = parties[0];
        else
            friendlyUnitParty = parties[1];

        //Performs swap with determined index values
        friendlyUnitParty.SwitchMembers(target.gameObject, caster.gameObject);

        /*
        //Retrieves unit friendly unit party reference
        List<GameObject> friendlyParty;
        UnitParty friendlyUnitParty;
        UnitParty[] parties = FindObjectsOfType<UnitParty>();
        List<GameObject> currentParty = parties[0].GetAllMembers();   
        if (currentParty[0].GetComponent<Minion>().ownerPlayer == true)
        {
            friendlyParty = currentParty;
            friendlyUnitParty = parties[0];
        }
        else
        {
            friendlyParty = parties[1].GetAllMembers();
            friendlyUnitParty = parties[1];
        }
        //Determines indexes within the unit party for caster and target minions
        int casterIndex = 0;
        int targetIndex = 0;
        for(int i = 0; i < friendlyParty.Count; i++)
        {
            if (target == friendlyParty[i].GetComponent<Minion>()) //Checks if target is current minion
            {
                targetIndex = i;
            }
            else if (caster == friendlyParty[i].GetComponent<Minion>()) //Checks if caster is current minion
            {
                casterIndex = i;
            }
        }
        //Performs swap with determined index values
        friendlyUnitParty.SwitchMemberIndices(targetIndex, casterIndex);
        */
    }
}
