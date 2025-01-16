[System.Serializable]
public abstract class SpellEffectWithCheck : SpellEffect
{
    public abstract void DoSuccessEffect(Minion caster, Minion target);
    public abstract void DoFailedEffect(Minion caster, Minion target);
}
