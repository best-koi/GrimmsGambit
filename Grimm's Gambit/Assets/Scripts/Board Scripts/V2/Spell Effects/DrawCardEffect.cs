using UnityEngine;

[System.Serializable]
public class DrawCardEffect : SpellEffect
{
    [SerializeField] int m_Amount = 1;

    public DrawCardEffect()
    {
        _spellName = "Draw";
        _spellDescription = "Draw an amount of cards from the deck.";
    }

    public override void DoSpellEffect(Minion caster, Minion target)
    {
        Deck deck = GameObject.FindObjectOfType<Deck>();
        deck.DrawAmount(false, m_Amount);
    }
}
