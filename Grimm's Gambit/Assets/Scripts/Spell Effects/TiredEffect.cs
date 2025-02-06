using UnityEngine;

[System.Serializable]
public class TiredEffect : SpellEffect
{
    public TiredEffect()
    {
        _spellName = "Tired";
        _spellDescription = "When this card is played, player becomes tired.";
    }
    public override void DoSpellEffect(Minion caster, Minion target) //Causes the player to become tired (function currently only functional for player being tired since ai attacks are controlled without spirit)
    {
        EncounterController encounterController = GameObject.FindObjectOfType<EncounterController>();
        encounterController.BecomeTired();
    }
}
