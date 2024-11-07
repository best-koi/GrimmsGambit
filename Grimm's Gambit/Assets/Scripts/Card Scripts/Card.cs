using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] private protected string cardName;

    [SerializeField] private protected int cardCost;

    [SerializeField] private protected Deck m_Hand;

    [SerializeField] private protected Minion target = null;

    [SerializeField] private protected Minion caster = null; //Added by Ryan - 11/1/2024
    [SerializeField] private protected bool awaitingTarget;

    // Index in the database
    // Number that player has in their deck
    [SerializeField] private protected int m_Index, m_PlayerCopies;

    [SerializeField] private protected EncounterController m_EncounterController;

    // If the card is not intended to be part of the deck
    // Applicable to once keyword
    private protected bool m_IsEphemeral = false;

    public void SelectCard()
    {
        Debug.Log(cardName + " selected...");
        
        Component[] spells = gameObject.GetComponents(typeof(SpellComponent));
        foreach(SpellComponent spell in spells) {
            if (spell.GetRequiresTarget() && (target == null)) {
                awaitingTarget = true;
            }
        }

        if ( (awaitingTarget == false) && (target == null) ) {
            
            // Cancel the method call if the player doesn't have enough resources
            //if (!m_EncounterController.SpendResources(cardCost)) return;
         
            DoSpells();
        }
    }

    private void Update()
    {
        // Pretty sure this is all UI element stuff
        // Can be copied and added to a seperate class
        
        /**
        if (awaitingTarget) {
            if(Input.GetMouseButtonDown(0)) {
                Vector3 mousePos = Input.mousePosition;
                Ray ray = Camera.main.ScreenPointToRay(mousePos);

                if (Physics.Raycast(ray, out RaycastHit hit)) {
                    if (hit.collider.GetComponent<Minion>()) {

                        target = hit.collider.GetComponent<Minion>();
                        Debug.Log("Hit Minion, Max Health " + target.maxHealth);

                        awaitingTarget = false;
                        DoSpells();
                    }
                }
            }

        }
        */
    }

    public void DoSpells()
    {
        Component[] spells = gameObject.GetComponents(typeof(SpellComponent));
        foreach(SpellComponent spell in spells) {
            if (spell.GetRequiresTarget())
                spell.SetTarget(target);
            spell.SetCaster(caster);
            spell.DoSpellEffect();
        }

        if (m_IsEphemeral)
        {
            // Unfinished
            // Reference the deck object 
            // Remove from the game

            return;
        }

        //m_Hand.Discard(0, m_Index);
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

    public Minion GetCaster()
    {
        return caster;
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

    public void MakeEphemeral()
    {
        m_IsEphemeral = true;
    }

    public void SetTarget(Minion newTarget)
    {
        target = newTarget;
    }
}
