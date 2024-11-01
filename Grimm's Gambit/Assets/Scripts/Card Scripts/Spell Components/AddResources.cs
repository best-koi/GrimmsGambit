using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddResources : SpellComponent
{
    public AddResources()
    {
        spellName = "Gain Spirit.";
        spellDescription = "Adds one spirit.";
    }

    public override void DoSpellEffect()
    {
        EncounterController encounterController = FindObjectOfType<EncounterController>();
        
        // Spending negative resources adds additional resources 
        encounterController.SpendResources(-1);
    }
}
