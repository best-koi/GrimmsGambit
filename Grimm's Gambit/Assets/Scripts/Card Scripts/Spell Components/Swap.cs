using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swap : SpellComponent
{
    public Swap()
    {
        spellName = "Swap";
        spellDescription = "When this card is played, position is swapped with the target character.";
    }
    public override void DoSpellEffect()
    {
        //Insert function from unit party here to swap position with between "caster" and "target"
    }
}
