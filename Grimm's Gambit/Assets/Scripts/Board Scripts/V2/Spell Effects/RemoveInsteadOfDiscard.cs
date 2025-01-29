[System.Serializable]
public class RemoveInsteadOfDiscard : SpellEffect
{

    public RemoveInsteadOfDiscard()
    {
        _spellName = "RemoveInsteadOfDiscard";
        _spellDescription = "This spell is used as a flag to designate cards to be removed instead of discarded.";
    }

    public override void DoSpellEffect(Minion caster, Minion target)
    {
        //Nothing occurs
    }
}
