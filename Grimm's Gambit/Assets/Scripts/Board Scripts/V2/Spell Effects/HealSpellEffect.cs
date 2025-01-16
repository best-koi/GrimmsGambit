using UnityEngine;

[System.Serializable]
public class HealSpellEffect : SpellEffect
{
    [SerializeField] private int m_Amount = 3;
    [SerializeField] private bool m_TargetingSelf = false;

    public HealSpellEffect()
    {
        _spellName = "Heal";
        _spellDescription = "Heal an amount for a target.";

        if(!m_TargetingSelf) _requiresTarget = true;
    }
    public override void DoSpellEffect(Minion caster, Minion target)
    {
        if (m_TargetingSelf) caster.DamageTaken(-m_Amount);
        else target.DamageTaken(-m_Amount); 
    }
}
