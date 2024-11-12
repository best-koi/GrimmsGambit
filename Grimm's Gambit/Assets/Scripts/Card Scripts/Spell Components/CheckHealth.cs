using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckHealth : SpellComponent
{
    private protected float m_HealthGate;
    private protected bool m_CheckingSelf;

    public CheckHealth()
    {
        spellName = "Check Health";
        spellDescription = "Check if the health of the target or the caster is below the health gate.";

        if (!m_CheckingSelf) requiresTarget = true;
    }

    public bool CheckGate()
    {
        if (!m_CheckingSelf) return (target.currentHealth / target.maxHealth) <= m_HealthGate;
        return (caster.currentHealth / caster.maxHealth) <= m_HealthGate;
    }

    public override void DoSpellEffect()
    {
        if (CheckGate()) DoRealEffect();
    }

    public virtual void DoRealEffect() {
        Debug.Log("Augment another spell");
    }
}
