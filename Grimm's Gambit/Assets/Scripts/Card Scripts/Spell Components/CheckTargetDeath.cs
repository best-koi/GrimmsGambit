using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTargetDeath : SpellComponent
{
    public CheckTargetDeath()
    {
        spellName = "Check Target Death";
        spellDescription = "Listen for the death of a target, then do an effect when that target dies.";

        requiresTarget = true;
    }

    public override void DoSpellEffect()
    {
        Minion.onDeath += CheckEffect;
        DoTriggeredEffect();
    }

    private void CheckEffect(Minion minion)
    {
        if (minion == target)
        {
            Minion.onDeath -= CheckEffect;
            DoTrueEffect();
        }
    }

    public virtual void DoTriggeredEffect()
    {
        Debug.Log("Do an effect on cast.");
    }

    public virtual void DoTrueEffect()
    {
        Debug.Log("Do the effect when the target is destroyed.");
    }
}