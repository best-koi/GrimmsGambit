[System.Serializable]
public class CleanseEffect : SpellEffect
{
    public CleanseEffect()
    {
        _spellName = "Cleanse";
        _spellDescription = "When this card is played, target loses all debuffs.";
    }

    public override void DoSpellEffect(Minion caster, Minion target)
    {
        target.Cleanse(); //cleanses target
    }
}
