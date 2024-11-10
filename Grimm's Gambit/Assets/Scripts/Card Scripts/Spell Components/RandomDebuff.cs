using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDebuff : SpellComponent
{
    public Affix[] debuffs;
    public int[] values;
    public RandomDebuff()
    {
        requiresTarget = true;
    }

    public override void DoSpellEffect()
    {
        // Unsure of what function is called to add affixes to Minions, here is filler code
        //Updated Version Added by Ryan - 11/1/2024:
        for(int i = 0; i < debuffs.Length; i++) 
        {
            target.AddAffix(debuffs[i], values[i]);
        }
    }
}
