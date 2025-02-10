using UnityEngine;

[System.Serializable]
public abstract class CheckHealthEffect : SpellEffectWithCheck
{
    [SerializeField] protected float m_HealthGate;
    [SerializeField] protected ValueComparison _valueComparisonMode;
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
        float healthRatio = minion.currentHealth / minion.maxHealth;

        switch (_valueComparisonMode)
        {
            case ValueComparison.EQUAL:
                return healthRatio == m_HealthGate;

            case ValueComparison.NOT_EQUAL:
                return healthRatio != m_HealthGate;

            case ValueComparison.LESS:
                return healthRatio < m_HealthGate;

            case ValueComparison.GREATER:
                return healthRatio > m_HealthGate;

            case ValueComparison.LESS_OR_EQUAL:
                return healthRatio <= m_HealthGate;

            case ValueComparison.GREATER_OR_EQUAL:
                return healthRatio >= m_HealthGate;

            default:
                return false;
        }
    }
}

public enum ValueComparison
{
    EQUAL,
    NOT_EQUAL,
    LESS,
    GREATER,
    LESS_OR_EQUAL,
    GREATER_OR_EQUAL,
}
