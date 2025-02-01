using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AugmentedHeal : CheckTarget
{
    [SerializeField] private int m_BuffedHeal, m_BaseHeal;
    [SerializeField] private bool m_TargetingSelf;
    
    public override void DoTrueEffect()
    {
        if (m_TargetingSelf) target = caster;
        caster.MinionUsed(target, -m_BuffedHeal);
    }

    public override void DoFalseEffect()
    {
        if (m_TargetingSelf) target = caster;
        caster.MinionUsed(target, -m_BaseHeal);
    }
}
