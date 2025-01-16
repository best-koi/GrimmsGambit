using UnityEngine;

[System.Serializable]
public abstract class CheckHealthEffect : SpellEffectWithCheck
{
    [SerializeField] protected float m_HealthGate;
    [SerializeField] protected bool m_CheckingSelf;

    public CheckHealthEffect()
    {
        _spellName = "Check Health";
        _spellDescription = "Check if the health of the target or the caster is below the health gate.";

        if (!m_CheckingSelf) _requiresTarget = true;
    }

    public override void DoSpellEffect(Minion caster, Minion target)
    {
        if (CheckGate(caster, target)) DoSuccessEffect(caster, target);
        else DoFailedEffect(caster, target);
    }

    public bool CheckGate(Minion caster, Minion target)
    {
        if (!m_CheckingSelf) return _checkMinionHealth(target);
        else return _checkMinionHealth(caster);
    }


    private bool _checkMinionHealth(Minion minion)
    {
        return minion.currentHealth / minion.maxHealth < m_HealthGate;
    }
}
