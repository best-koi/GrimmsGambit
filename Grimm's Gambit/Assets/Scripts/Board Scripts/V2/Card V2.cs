using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CardV2 : MonoBehaviour
{
    [Tooltip("Card Template that contains card stats")]
    [SerializeField] private CardTemplate _cardTemplate;

    [Header("Card Instance Variables")]
    [SerializeField] private Minion _caster = null; //Added by Ryan - 11/1/2024
    [SerializeField] private bool _awaitingTarget;

    #region Properties

    public string CardName { get => _cardTemplate.CardName; }
    public string CardDescription { get => _cardTemplate.CardDescription; }
    public int CardCost { get => _cardTemplate.CardCost; }
    public CardData Data { get => _cardTemplate.Data; }
    public int PlayerCopyCount { get => _cardTemplate.PlayerCopyCount; }
    public CardTemplate ReverseTemplate { get => _cardTemplate.ReverseTemplate; }
    public SpellEffect[] Spells { get => _cardTemplate.Spells; }
    public Minion Caster { get => _caster; set => _caster = value; }

    #endregion

    public void SelectCard(Minion target)
    {
        Debug.Log(CardName + " selected...");

        Component[] spells = gameObject.GetComponents(typeof(SpellComponent));
        foreach (SpellComponent spell in spells)
        {
            if (spell.GetRequiresTarget() && (target == null))
            {
                _awaitingTarget = true;
            }
        }

        if ((_awaitingTarget == false) && (target == null))
        {

            // Cancel the method call if the player doesn't have enough resources
            //if (!m_EncounterController.SpendResources(cardCost)) return;

            DoSpells(target);
        }
    }

    public void DoSpells(Minion target)
    {
        foreach (SpellEffect spell in Spells)
        {
            spell.DoSpellEffect(_caster, target);
        }
    }

    public void SetCardTemplate(CardTemplate newTemplate)
    {
        _cardTemplate = newTemplate;
    }
}



