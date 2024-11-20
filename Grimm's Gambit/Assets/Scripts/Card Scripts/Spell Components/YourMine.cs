using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YourMine : CheckTargetDeath
{
    [SerializeField] private int m_Amount;

    [SerializeField] private Affix debuff;
    [SerializeField] private int value;

    public virtual void DoTrueEffect()
    {
        caster.DamageTaken(-m_Amount);
        caster.AddAffix(debuff, value);
    }
}
