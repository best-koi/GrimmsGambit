using UnityEngine;

[System.Serializable]
public class AddSpiritEffect : SpellEffect
{
    [SerializeField] private int _spiritGained;

    public AddSpiritEffect()
    {
        _spellName = "Gain Spirit.";
        _spellDescription = "Adds X spirit.";
    }

    public override void DoSpellEffect(Minion caster = null, Minion target = null)
    {
        EncounterController encounterController = GameObject.FindObjectOfType<EncounterController>();

        // Spending negative resources adds additional resources 
        encounterController.SpendResources(-1 * _spiritGained);
    }
}
