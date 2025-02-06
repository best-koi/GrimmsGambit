[System.Serializable]
public class InsecurityEffect : SpellEffect
{
    public override void DoSpellEffect(Minion caster, Minion target)
    {
        DamageSelfEffect.ApplyEffect();
    }
}
