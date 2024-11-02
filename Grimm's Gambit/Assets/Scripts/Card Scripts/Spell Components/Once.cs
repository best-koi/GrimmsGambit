using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Once : SpellComponent
{
    public Once()
    {
        spellName = "Once";
        spellDescription = "When this card is played, it is removed from the game.";
    }
    public override void DoSpellEffect()
    {
        Card card = GetComponent<Card>();
        card.MakeEphemeral(); 
    }
}
