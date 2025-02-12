using UnityEngine;

[System.Serializable]
public class DoOnDebuffEffect : SpellEffect
{
    public Affix conditionDebuff;
    [SerializeReference, SubclassSelector] private SpellEffect[] _spells;

    public DoOnDebuffEffect()
    {
        _requiresTarget = true;
        _spellName = "DoOnDebuff";
        _spellDescription = "Does desired actions if a certain affix is present on the target.";
    }

    public override void DoSpellEffect(Minion caster, Minion target)
    {
        if (target.currentAffixes.ContainsKey(conditionDebuff))
            foreach (SpellEffect spell in _spells)
                spell.DoSpellEffect(caster, target);
    }
}
