using UnityEngine;

[System.Serializable]
public class MillCardEffect : SpellEffect
{
    [SerializeField] private int _amount = 1;

    public MillCardEffect()
    {
        _spellName = "Mill";
        _spellDescription = "Mills an amount of cards from the top of the deck.";
    }

    public override void DoSpellEffect(Minion caster = null, Minion target = null)
    {
        Deck deck = GameObject.FindObjectOfType<Deck>();
        deck.MillAmount(_amount);
    }
}
