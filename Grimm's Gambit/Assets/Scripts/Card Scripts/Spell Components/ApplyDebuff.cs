using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyDebuff : SpellComponent
{
    public Affix[] debuffs;
    public int[] value;
    public ApplyDebuff()
    {
        requiresTarget = true;
    }

    public override void DoSpellEffect()
    {
        // Unsure of what function is called to add affixes to Minions, here is filler code
        //Updated Version Added by Ryan - 11/1/2024:
        for(int i = 0; i < debuffs.Length; i++) 
        {
            target.AddAffix(debuffs[i], value[i]);
        }
    }
}
