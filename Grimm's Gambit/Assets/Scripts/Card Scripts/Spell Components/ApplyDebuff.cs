using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyDebuff : SpellComponent
{
    private Affixes[] debuffs;
    public ApplyDebuff()
    {
        requiresTarget = true;
    }

    public override void DoSpellEffect()
    {
        // Unsure of what function is called to add affixes to Minions, here is filler code
        //Updated Version Added by Ryan - 11/1/2024:
        foreach(Affixes debuff in debuffs) 
        {
            target.AddAffix(debuff.Tag, debuff.Value);
        }
    }
}
