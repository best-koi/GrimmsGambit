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
    private protected Minion caster = null; //Added by Ryan - 11/1/2024

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

    public void SetCaster(Minion newCaster) //Function to determine caster Added by Ryan - 11/1/2024
    {
        caster = newCaster;
    }
}
