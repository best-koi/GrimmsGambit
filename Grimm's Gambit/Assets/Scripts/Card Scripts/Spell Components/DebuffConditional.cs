using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuffConditional : SpellComponent
{
    public Affix conditionDebuff;
    public int spiritGain;
    public int cardDraw = 0;
    public Affix[] resultantDebuffs;
    public int[] resultantValues;
    public DebuffConditional()
    {
        requiresTarget = true;
        spellName = "ConditionalDebuff";
        spellDescription = "Does desired actions if a certain affix is present on the target.";
    }

    public override void DoSpellEffect() //Does spell effects if conditionDebuff is present on the target
    {
        if (target.currentAffixes.ContainsKey(conditionDebuff))
        {
            if (spiritGain > 0)
            {
                EncounterController encounterController = FindObjectOfType<EncounterController>();
                // Spending negative resources adds additional resources 
                encounterController.SpendResources(-1 * spiritGain);
            }
            if (cardDraw > 0)
            {
                Deck deck = FindObjectOfType<Deck>();
                deck.DrawAmount(false, cardDraw);
            }
            for(int i = 0; i < resultantDebuffs.Length; i++) 
            {
                target.AddAffix(resultantDebuffs[i], resultantValues[i]);
            }
        }
    }
}
