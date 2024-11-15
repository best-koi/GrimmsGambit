using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyAffixByFactor : SpellComponent
{
    [SerializeField] private Affix[] debuffs;

    // Must be greater that or equal to one
    [SerializeField] private int[] values;

    [SerializeField] private bool m_TargetingSelf;

    public ApplyAffixByFactor()
    {
        spellName = "AOE Attack";
        spellDescription = "Deal an amount of damage to the enemy party.";

        if (!m_TargetingSelf) requiresTarget = true;
    }

    public override void DoSpellEffect()
    {
        Minion minionToCheck;
        if (requiresTarget) minionToCheck = caster;
        else minionToCheck = target;

        for (int i = 0; i < debuffs.Length; i++)
        {
            if (values[i] < 1) values[i] = 1;

            minionToCheck.AddAffix(debuffs[i], minionToCheck.currentAffixes[debuffs[i]] * (values[i] - 1));
        }
    }
}
