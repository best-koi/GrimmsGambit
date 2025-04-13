using UnityEngine;

[System.Serializable]
public class Reconstruct : SpellEffect
{
    public Reconstruct()
    {
        _spellName = "Reconstruct";
        _spellDescription = "Discard hand and draw the same number discarded.";
    }

    public override void DoSpellEffect(Minion caster = null, Minion target = null)
    {
        Deck deck = GameObject.FindObjectOfType<Deck>();
        int drawAmount = deck.DiscardHand(); //Discards hand and records amount discarded
        deck.DrawAmount(false, drawAmount); //Draws the amount of cards that were discarded
    }
}
