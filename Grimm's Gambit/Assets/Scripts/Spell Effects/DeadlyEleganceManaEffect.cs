using UnityEngine;

[System.Serializable]
public class DeadlyEleganceManaEffect : SpellEffect
{
    //This workaround refunds mana when cost should be lowered so I can avoid having to adjust the original card object - there is likely a better way to do this
    public override void DoSpellEffect(Minion caster = null, Minion target = null)
    {
        Deck deck = GameObject.FindObjectOfType<Deck>();
        int cardsUsed = deck.m_MaxCountThisTurn - deck.CurrentCardCount; //Calculates cards used this round
        EncounterController encounterController = GameObject.FindObjectOfType<EncounterController>();
        if (cardsUsed < 5 && cardsUsed > 0)
        {
            encounterController.SpendResources(-cardsUsed); //Refunds mana for cards spent, up to the maximum cost of the card (5)
        }
        else if (cardsUsed >= 5)
        {
            encounterController.SpendResources(-5); //Refunds maximum cost after using card
        }
    }
}
