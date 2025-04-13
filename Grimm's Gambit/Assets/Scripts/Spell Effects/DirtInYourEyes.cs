[System.Serializable]
public class DirtInYourEyes : SpellEffect
{

    public DirtInYourEyes()
    {
        _spellName = "DirtInYourEyes";
        _spellDescription = "The targeted enemy this turn will gain Damage Reduction everytime an attack card is played.";
    }

    public override void DoSpellEffect(Minion caster, Minion target)
    {
        target.DirtInEyes = true; //applies dirt in eyes effect to target for the duration of this turn
    }
}
