using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveAffixAndDoEffect : SpellComponent
{
    public Affix debuff;
    [SerializeField] private bool m_TargetingSelf = false;
    [SerializeField] private float m_AmountMultiplier = 1.0f;
    // Can add more bools and do different effects based on chosen one
    [SerializeField] private bool m_HealTarget;

    public RemoveAffixAndDoEffect()
    {
        spellName = "Remove Affix + Do Effect";
        spellDescription = "Removes an Affix and does an effect based on the amount of that affix removed";

        if (!m_TargetingSelf) requiresTarget = true;
    }

    public override void DoSpellEffect()
    {
        Minion minionToEffect;
        if (m_TargetingSelf) minionToEffect = caster;
        else minionToEffect = target;

        int amount = minionToEffect.RemoveAffixAndCount(debuff);

        if (amount > 0)
        {
            Debug.Log("Minion does not have selected affix applied.");
            return;
        }

        if (m_HealTarget) minionToEffect.DamageTaken((int)((-amount * m_AmountMultiplier) + 0.5));
    }
}
