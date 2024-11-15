using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gouge : SpellComponent
{
    public int Multiplier;
    public Gouge()
    {
        spellName = "Gouge";
        spellDescription = "When this card is played, multiplies bleed by multiplier.";
    }
    public override void DoSpellEffect()
    {
        target.Gouge(Multiplier); //gouges target
    }
}
