using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class CappedSteal : SpellComponent
{

    public Affix targetDebuff;
    public int limit;

    public CappedSteal()
    {
        spellName = "Capped Steal";
        spellDescription = "When this card is played, caster steals an amount of the given debuff less than or equal to the given limit.";
    }
    public override void DoSpellEffect() //Tries to steal "limit" amount of "targetDebuff"
    {
        if (target.currentAffixes.ContainsKey(targetDebuff))
        {
            int currentStacks = target.currentAffixes[targetDebuff];
            if (currentStacks > limit)
            {
                target.currentAffixes.Remove(targetDebuff); //Replaces affix with a lesser amount
                target.currentAffixes.Add(targetDebuff, currentStacks-limit);
                caster.AddAffix(targetDebuff, limit); //Grants caster limit
            }
            else //Grants caster current Stacks
            {
                target.currentAffixes.Remove(targetDebuff);
                caster.AddAffix(targetDebuff, currentStacks);
            }
        }
    }
}
