using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoolishProvocation : SpellComponent
{
    public FoolishProvocation()
    {
        spellName = "Foolish Provocation";
        spellDescription = "When this card is played, player heals 7 HP if enemy has any debuffs.";
    }
    public override void DoSpellEffect() //Causes the player to become tired (function currently only functional for player being tired since ai attacks are controlled without spirit)
    {
        if (target.HasADebuff())
        {
            caster.DamageTaken(-7); //Heals caster for 7 if the target has a debuff
        }
    }
}
