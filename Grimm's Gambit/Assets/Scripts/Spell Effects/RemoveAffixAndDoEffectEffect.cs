using UnityEngine;

[System.Serializable]
public class RemoveAffixAndDoEffectEffect : SpellEffect
{
    public Affix debuff;
    [SerializeField] private bool m_TargetingSelf = false;
    [SerializeField] private float m_AmountMultiplier = 1.0f;
    // Can add more bools and do different effects based on chosen one
    [SerializeField] private bool m_HealTarget;

    public RemoveAffixAndDoEffectEffect()
    {
        _spellName = "Remove Affix + Do Effect";
        _spellDescription = "Removes an Affix and does an effect based on the amount of that affix removed";

        if (!m_TargetingSelf) _requiresTarget = true;
    }

    public override void DoSpellEffect(Minion caster, Minion target)
    {
        Minion minionToEffect;
        if (m_TargetingSelf) minionToEffect = caster;
        else minionToEffect = target;

        int amount = minionToEffect.GetAffixCount(debuff);

        if (amount > 0)
        {
            Debug.Log("Minion does not have selected affix applied.");
            return;
        }

        if (m_HealTarget) minionToEffect.DamageTaken((int)((-amount * m_AmountMultiplier) + 0.5));
    }
}
