using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SwapEffect : SpellEffect
{
    public SwapEffect()
    {
        _spellName = "Swap Effect";
        _spellDescription = "When this card is played, position is swapped with the target character.";
    }
    public override void DoSpellEffect(Minion caster, Minion target) //Swaps positions of caster and target
    {
        UnitParty friendlyUnitParty;
        UnitParty[] parties = GameObject.FindObjectsOfType<UnitParty>();
        List<Transform> currentParty = parties[0].GetAll();
        if (currentParty[0].GetComponent<Minion>().ownerPlayer == true)
            friendlyUnitParty = parties[0];
        else
            friendlyUnitParty = parties[1];

        //Performs swap with determined index values
        friendlyUnitParty.SwitchChildren(target.transform, caster.transform);
    }
}
