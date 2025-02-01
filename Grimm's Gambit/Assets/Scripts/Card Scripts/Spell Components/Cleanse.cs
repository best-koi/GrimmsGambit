using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleanse : SpellComponent
{
    public Cleanse()
    {
        spellName = "Cleanse";
        spellDescription = "When this card is played, target loses all debuffs.";
    }
    public override void DoSpellEffect()
    {
        target.Cleanse(); //cleanses target
    }
}
