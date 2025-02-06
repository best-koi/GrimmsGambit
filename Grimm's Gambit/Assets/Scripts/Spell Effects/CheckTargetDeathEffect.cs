[System.Serializable]
public abstract class CheckTargetDeathEffect : SpellEffectWithCheck
{
    private Minion _target;

    public CheckTargetDeathEffect()
    {
        _spellName = "Check Target Death";
        _spellDescription = "Listen for the death of a target, then do an effect when that target dies.";

        _requiresTarget = true;
    }

    public override void DoSpellEffect(Minion caster, Minion target)
    {
        _target = target;
        Minion.onDeath += CheckEffect;
        DoFailedEffect(caster, target);
    }

    private void CheckEffect(Minion minion)
    {
        if (minion == _target)
        {
            Minion.onDeath -= CheckEffect;
            DoSuccessEffect(minion, null);
        }
        else
        {
            Minion.onDeath += CheckEffect;
            DoFailedEffect(minion, null);
        }
    }
}