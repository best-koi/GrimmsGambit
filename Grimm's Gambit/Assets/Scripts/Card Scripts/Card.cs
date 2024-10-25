using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] private protected string cardName;

    [SerializeField] private protected int cardCost;

    [SerializeField] private protected bool isReversed = false;

    [SerializeField] private protected Deck m_Hand;

    // Index in the database
    // Number that player has in their deck
    [SerializeField] private protected int m_Index, m_PlayerCopies;

    private void OnMouseDown()
    {
        Component[] spells = gameObject.GetComponents(typeof(SpellComponent));
        foreach(SpellComponent spell in spells) {
            spell.DoSpellEffect();
        }

        m_Hand.Discard(0, m_Index);
        
        Destroy(gameObject);
    }

    public string GetName() 
    { 
        return cardName; 
    }

    public int GetCardCost()
    {
        return cardCost;
    }

    public int GetIndex()
    {
        return m_Index;
    }

    public int NumCopies
    {
        get
        {
            return m_PlayerCopies;
        }
        set
        {
            m_PlayerCopies = value;
        }
    }

    public void ReverseCard()
    {
        isReversed = !isReversed;
    }
}
