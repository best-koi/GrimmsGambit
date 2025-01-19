using UnityEngine;

[System.Serializable]
public class DiscardCardEffect : SpellEffect
{
    public DiscardCardEffect()
    {
        _spellName = "Discard";
        _spellDescription = "Discard's the leftmost card from the player's hand.";
    }

    public override void DoSpellEffect(Minion caster = null, Minion target = null)
    {
        Deck deck = GameObject.FindObjectOfType<Deck>();
        deck.DiscardRandomInHand(); //Discards a random card from the hand
    }
}
