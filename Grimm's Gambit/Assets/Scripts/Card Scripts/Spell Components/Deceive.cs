using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class Deceive : SpellComponent
{
    public Deceive()
    {
        spellName = "Deceive";
        spellDescription = "When this card is played, caster steals a random buff from the target.";
    }
    public override void DoSpellEffect() //Causes the player to become tired (function currently only functional for player being tired since ai attacks are controlled without spirit)
    {
        List<Affix> enemyBuffs = target.RetrieveBuffs(); //Retrieves enemy buff list
        System.Random random = new System.Random();
        int randomIndex = random.Next(enemyBuffs.Count);
        int affixValue = target.currentAffixes[enemyBuffs[randomIndex]]; //Retrieves stat value of random affix
        caster.AddAffix(enemyBuffs[randomIndex], affixValue); //Gives buff to caster
        target.currentAffixes.Remove(enemyBuffs[randomIndex]); //Removes buff from target
    }
}
