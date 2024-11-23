using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowThrough : CheckHealth
{
    [SerializeField] private int m_BuffedDamage, m_BaseDamage;

    public override void DoTrueEffect()
    {
        caster.MinionUsed(target, m_BuffedDamage);
    }

    public override void DoFalseEffect()
    {
        caster.MinionUsed(target, m_BaseDamage);
    }
}
