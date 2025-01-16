[System.Serializable]
public class GougeEffect : SpellEffect
{
    public int _multiplier;

    public GougeEffect()
    {
        _spellName = "Gouge";
        _spellDescription = "When this card is played, multiplies bleed by multiplier.";
    }

    public override void DoSpellEffect(Minion caster, Minion target)
    {
        target.Gouge(_multiplier); //gouges target
    }
}
