using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tired : SpellComponent
{
    public Tired()
    {
        spellName = "Tired";
        spellDescription = "When this card is played, player becomes tired.";
    }
    public override void DoSpellEffect() //Causes the player to become tired (function currently only functional for player being tired since ai attacks are controlled without spirit)
    {
        EncounterController encounterController = FindObjectOfType<EncounterController>();
        encounterController.BecomeTired();
    }
}
