using UnityEngine;

[System.Serializable]
public class AddResourcesEffect : SpellEffect
{
    public AddResourcesEffect()
    {
        _spellName = "Gain Spirit.";
        _spellDescription = "Adds one spirit.";
    }

    public override void DoSpellEffect(Minion caster = null, Minion target = null)
    {
        EncounterController encounterController = GameObject.FindObjectOfType<EncounterController>();

        // Spending negative resources adds additional resources 
        encounterController.SpendResources(-1);
    }
}
