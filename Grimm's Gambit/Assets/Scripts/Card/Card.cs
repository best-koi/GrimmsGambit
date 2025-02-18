using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    [Tooltip("Card Template that contains card stats")]
    [SerializeField] private CardTemplate _cardTemplate;

    [Header("Card Instance Variables")]
    [SerializeField] private Minion _caster = null; //Added by Ryan - 11/1/2024
    [SerializeField] private bool _awaitingTarget;

    private protected bool m_IsEphemeral = false;
    private int _currentCardCost;

    #region Properties

    public string CardName { get => _cardTemplate.CardName; }
    public string CardDescription { get => _cardTemplate.CardDescription; }
    public int CardCost { get => _currentCardCost; set => _currentCardCost = value; }
    public int BaseCardCost { get => _cardTemplate.CardCost; }
    public Sprite CardSprite { get => _cardTemplate.CardSprite; }
    public CardData Data { get => _cardTemplate.Data; }
    public int PlayerCopyCount { get => _cardTemplate.PlayerCopyCount; set => _cardTemplate.PlayerCopyCount = value; }
    public bool TargetsEnemies { get => _cardTemplate.TargetsEnemies; }
    public CardTemplate ReverseTemplate { get => _cardTemplate.ReverseTemplate; }
    public List<SpellEffect> Spells { get => _cardTemplate.Spells; }
    public Minion Caster { get => _caster; set => _caster = value; }
    public bool IsEphemeral { get => m_IsEphemeral; }

    #endregion

    #region MonoBehavior Callbacks

    private void Start()
    {
        _currentCardCost = BaseCardCost;
    }

    #endregion

    #region Public Methods

    public void DoSpells(Minion target)
    {
        _cardTemplate.DoSpells(_caster, target);
    }

    public void SetCardTemplate(CardTemplate newTemplate)
    {
        _cardTemplate = newTemplate;
    }

    #endregion
}



