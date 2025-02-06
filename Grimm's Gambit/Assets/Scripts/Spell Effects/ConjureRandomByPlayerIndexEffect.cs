using UnityEngine;

[System.Serializable]
public class ConjureRandomKatzeCardEffect : SpellEffect
{
    [SerializeField] int m_Amount;

    public ConjureRandomKatzeCardEffect()
    {
        _spellName = "ConjureRandomKatzeCard";
        _spellDescription = "Conjures an amount of cards from the database into the hand, randomized from katze's database.";
    }

    public override void DoSpellEffect(Minion caster = null, Minion target = null)
    {
        Deck deck = GameObject.FindObjectOfType<Deck>();
        CardDatabase database = GameObject.FindObjectOfType<CardDatabase>();
        System.Random random = new System.Random();
        int randomValue = random.Next(0, 18);
        CardTemplate m_Conjured = database.GetTemplate(2, randomValue);

        for (int i = 0; i < m_Amount; i++)
            deck.Conjure(m_Conjured.Data);
    }
}
