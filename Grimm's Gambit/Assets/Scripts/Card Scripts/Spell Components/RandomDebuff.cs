using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RandomDebuff : SpellComponent
{
    public int quantity;
    public RandomDebuff()
    {
        requiresTarget = true;
    }

    public override void DoSpellEffect() //Function to apply a random debuff effect to the target character
    {
        System.Random random = new System.Random();
        int debuffOption = random.Next(0, 6);
        switch(debuffOption)
        {
            case 0:
                target.AddAffix(Affix.DamageReduction, quantity);
                break;
            case 1:
                target.AddAffix(Affix.Vulnerable, quantity);
                break;
            case 2:
                target.AddAffix(Affix.Bleed, quantity);
                break;
            case 3:
                target.AddAffix(Affix.Mark, quantity);
                break;
            case 4:
                target.AddAffix(Affix.Threaded, quantity);
                break;
            case 5:
                target.AddAffix(Affix.Exploit, quantity);
                break;
            default: //This should never occur
                break;
        }
    }
}
