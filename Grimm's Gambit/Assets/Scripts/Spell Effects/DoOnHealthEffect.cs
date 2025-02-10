using UnityEngine;

[System.Serializable]
public class DoOnHealthEffect : CheckHealthEffect
{
    [SerializeReference, SubclassSelector] private SpellEffect[] _successSpells;
    [SerializeReference, SubclassSelector] private SpellEffect[] _failureSpells;

    public DoOnHealthEffect()
    {
        _requiresTarget = true;
        _spellName = "DoOnHealth";
        _spellDescription = "Does desired actions if target hits certain health threshold.";
    }

    public override void DoSuccessEffect(Minion caster, Minion target)
    {
        _castEffects(_successSpells, caster, target);
    }

    public override void DoFailedEffect(Minion caster, Minion target)
    {
        _castEffects(_failureSpells, caster, target);
    }

    private void _castEffects(SpellEffect[] spells, Minion caster, Minion target)
    {
        foreach (SpellEffect spell in spells)
            spell.DoSpellEffect(caster, target);
    }
}
