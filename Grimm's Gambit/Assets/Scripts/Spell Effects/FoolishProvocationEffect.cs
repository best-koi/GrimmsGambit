[System.Serializable]
public class FoolishProvocationEffect : SpellEffect
{
    public FoolishProvocationEffect()
    {
        _spellName = "Foolish Provocation";
        _spellDescription = "When this card is played, player heals 7 HP if enemy has any debuffs.";
    }

    public override void DoSpellEffect(Minion caster, Minion target) //Causes the player to become tired (function currently only functional for player being tired since ai attacks are controlled without spirit)
    {
        if (target.HasDebuff())
        {
            caster.DamageTaken(-7); //Heals caster for 7 if the target has a debuff
        }
    }
}
