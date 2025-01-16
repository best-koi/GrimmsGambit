using UnityEngine;

[System.Serializable]
public class ConjureEffect : SpellEffect
{
    [SerializeField] private CardTemplate _conjuredTemplate;
    [SerializeField] private int _amount;

    public ConjureEffect()
    {
        _spellName = "Conjure";
        _spellDescription = "Conjures an amount of cards from the database into the hand.";
    }

    public override void DoSpellEffect(Minion caster = null, Minion target = null)
    {
        Deck deck = GameObject.FindObjectOfType<Deck>();
        for (int i = 0; i < _amount; i++)
            deck.Conjure(_conjuredTemplate.Data);
    }
}
