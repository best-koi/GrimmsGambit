using UnityEngine;

[System.Serializable]
public class YourMineEffect : CheckTargetDeathEffect
{
    [SerializeField] private int m_Amount;

    [SerializeField] private Affix debuff;
    [SerializeField] private int value;

    public override void DoSuccessEffect(Minion caster, Minion target)
    {
        caster.DamageTaken(-m_Amount);
        caster.AddAffix(debuff, value);
    }

    public override void DoFailedEffect(Minion caster, Minion target)
    {
    }
}
