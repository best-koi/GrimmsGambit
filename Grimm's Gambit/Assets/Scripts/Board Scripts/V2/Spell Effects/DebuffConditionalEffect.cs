using UnityEngine;

[System.Serializable]
public class DebuffConditionalEffect : SpellEffect
{
    public Affix conditionDebuff;
    public int spiritGain;
    public int cardDraw = 0;
    public Affix resultantDebuff;
    public int resultantValue;

    public DebuffConditionalEffect()
    {
        _requiresTarget = true;
        _spellName = "ConditionalDebuff";
        _spellDescription = "Does desired actions if a certain affix is present on the target.";
    }

    public override void DoSpellEffect(Minion caster, Minion target)
    {
        if (target.currentAffixes.ContainsKey(conditionDebuff))
        {
            if (spiritGain > 0)
            {
                EncounterController encounterController = GameObject.FindObjectOfType<EncounterController>();
                // Spending negative resources adds additional resources 
                encounterController.SpendResources(-1 * spiritGain);
            }
            if (cardDraw > 0)
            {
                Deck deck = GameObject.FindObjectOfType<Deck>();
                deck.DrawAmount(false, cardDraw);
            }

            target.AddAffix(resultantDebuff, resultantValue);
        }
    }
}
