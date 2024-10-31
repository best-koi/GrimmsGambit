using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellComponent : MonoBehaviour
{
    private protected string spellName;   
    private protected string spellDescription;
    private protected bool requiresTarget = false;
    private protected Minion target = null;

    // Generic function for a spell's effect
    // Meant to be overridden by inherited SpellComponents, will print an error if no override takes place
    // Card calls this function when used
    public virtual void DoSpellEffect() 
    {
        Debug.Log("Generic Spell: No Effect Taken");
    }

    public string GetSpellName()
    {
        return spellName;
    }

    public string GetSpellDescription()
    {
        return spellDescription;
    }

    public bool GetRequiresTarget()
    {
        return requiresTarget;
    }

    public void SetTarget(Minion newTarget)
    {
        target = newTarget;
    }
}
