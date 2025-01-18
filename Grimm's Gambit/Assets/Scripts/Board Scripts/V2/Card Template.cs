using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card Template", menuName = "Spawners/Card Template", order = 0)]
public class CardTemplate : ScriptableObject
{
    #region Serialized Fields

    [Header("Card Stats")]
    [SerializeField] private string _cardName;
    [SerializeField] private string _cardDescription;
    [SerializeField] private int _cardCost;
    [SerializeField] private int _playerCopyCount = 1;
    [SerializeField] private CardTemplate _reverseTemplate;
    [SerializeReference, SubclassSelector] private List<SpellEffect> _spells;

    #endregion

    #region Private Fields

    private CardData _data;

    #endregion

    #region Properties

    public string CardName { get => _cardName; }
    public string CardDescription { get => _cardDescription; }
    public int CardCost { get => _cardCost; }
    public CardData Data { get => _data; set => _data = value; }
    public int PlayerCopyCount { get => _playerCopyCount; set => _playerCopyCount = value; }
    public CardTemplate ReverseTemplate { get => _reverseTemplate; }
    public List<SpellEffect> Spells { get => _spells; }

    #endregion

    #region Public Methods

    public void DoSpells(Minion caster, Minion target)
    {
        foreach (SpellEffect spell in _spells)
            spell.DoSpellEffect(caster, target);
    }

    #endregion
}



