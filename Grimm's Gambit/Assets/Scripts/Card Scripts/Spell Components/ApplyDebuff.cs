using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyDebuff : SpellComponent
{
    private Affix[] debuffs;
    public ApplyDebuff()
    {
        requiresTarget = true;
    }

    public override void DoSpellEffect()
    {
        // Unsure of what function is called to add affixes to Minions, here is filler code

        // foreach( Affix debuff in debuffs )
        //      target.ApplyDebuff(debuff);
    }
}
