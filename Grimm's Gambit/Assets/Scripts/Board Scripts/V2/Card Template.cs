using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card Template", menuName = "Spawners/Card Template", order = 0)]
public class CardTemplate : ScriptableObject
{
    #region Serialized Fields

    [Tooltip("The card prefab used to spawn card objects from template")]
    [SerializeField] private GameObject _cardPrefab;

    [Header("Card Stats")]
    [SerializeField] private string _cardName;
    [SerializeField] private string _cardDescription;
    [SerializeField] private int _cardCost;
    [SerializeField] private CardData _data;
    [SerializeField] private int _playerCopyCount = 1;
    [SerializeField] private CardTemplate _reverseTemplate;
    [SerializeReference, SubclassSelector] private List<SpellEffect> _spells;

    #endregion

    #region Properties

    public string CardName { get => _cardName; }
    public string CardDescription { get => _cardDescription; }
    public int CardCost { get => _cardCost; }
    public CardData Data { get => _data; }
    public int PlayerCopyCount { get => _playerCopyCount; set => _playerCopyCount = value; }
    public CardTemplate ReverseTemplate { get => _reverseTemplate; }
    public List<SpellEffect> Spells { get => _spells; }

    #endregion

    #region Public Methods

    public GameObject SpawnCardObject(Minion caster = null)
    {
        GameObject cardObject = Instantiate(_cardPrefab);

        if (cardObject.TryGetComponent<CardV2>(out CardV2 card))
        {
            card.SetCardTemplate(this);

            if (caster != null)
                card.Caster = caster;
        }

        return cardObject;
    }

    public void DoSpells(Minion caster, Minion target)
    {
        foreach (SpellEffect spell in _spells)
            spell.DoSpellEffect(caster, target);
    }

    #endregion
}



