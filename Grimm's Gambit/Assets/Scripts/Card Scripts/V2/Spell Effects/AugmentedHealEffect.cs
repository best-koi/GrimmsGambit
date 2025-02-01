using UnityEngine;

[System.Serializable]
public class AugmentedHealEffect : CheckTargetEffect
{
    [SerializeField] private int m_BuffedHeal, m_BaseHeal;
    [SerializeField] private bool m_TargetingSelf;
    
    public override void DoSuccessEffect(Minion caster, Minion target)
    {
        if (m_TargetingSelf) target = caster;
        caster.MinionUsed(target, -m_BuffedHeal);
    }

    public override void DoFailedEffect(Minion caster, Minion target)
    {
        if (m_TargetingSelf) target = caster;
        caster.MinionUsed(target, -m_BaseHeal);
    }
}
