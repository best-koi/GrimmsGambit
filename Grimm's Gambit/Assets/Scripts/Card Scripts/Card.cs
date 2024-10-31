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

    [SerializeField] private protected Minion target = null;
    [SerializeField] private protected bool awaitingTarget;

    // Index in the database
    // Number that player has in their deck
    [SerializeField] private protected int m_Index, m_PlayerCopies;

    private void OnMouseDown()
    {
        Debug.Log(cardName + " selected...");
        
        Component[] spells = gameObject.GetComponents(typeof(SpellComponent));
        foreach(SpellComponent spell in spells) {
            if (spell.GetRequiresTarget() && (target == null)) {
                awaitingTarget = true;
            }
        }

        if ((awaitingTarget == false) && (target == null)) {
            DoSpells();
        }
    }

    private void Update()
    {
        if (awaitingTarget) {
            if(Input.GetMouseButtonDown(0)) {
                Vector3 mousePos = Input.mousePosition;
                Ray ray = Camera.main.ScreenPointToRay(mousePos);

                if (Physics.Raycast(ray, out RaycastHit hit)) {
                    if(hit.collider.GetComponent<Minion>()) {

                        target = hit.collider.GetComponent<Minion>();
                        Debug.Log("Hit Minion, Max Health " + target.maxHealth);

                        awaitingTarget = false;
                        DoSpells();
                    }
                }
            }

        }
    }

    private void DoSpells()
    {
        Component[] spells = gameObject.GetComponents(typeof(SpellComponent));
        foreach(SpellComponent spell in spells) {
            spell.DoSpellEffect();
        }

        m_Hand.Discard(0, m_Index);
        
        Destroy(gameObject);
    }

    // Getters

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
