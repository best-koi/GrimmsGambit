using UnityEngine;

[System.Serializable]
public class ApplyAffixByFactorEffect : SpellEffect
{
    [SerializeField] GenericDict<Affix, int> m_Affixes;

    [SerializeField] private bool m_TargetingSelf;

    public ApplyAffixByFactorEffect()
    {
        _spellName = "AOE Attack";
        _spellDescription = "Deal an amount of damage to the enemy party.";

        if (!m_TargetingSelf) _requiresTarget = true;
    }

    public override void DoSpellEffect(Minion caster, Minion target)
    {
        Minion minionToCheck;
        if (_requiresTarget) minionToCheck = caster;
        else minionToCheck = target;

        for (int i = 0; i < m_Affixes.GetLength(); i++)
        {
            Affix key = m_Affixes.GetKey(i);

            if (m_Affixes.GetValue(key) < 1) m_Affixes.SetValue(key, 1);

            minionToCheck.AddAffix(key, minionToCheck.currentAffixes[key] * (m_Affixes.GetValue(key) - 1));
        }
    }
}
