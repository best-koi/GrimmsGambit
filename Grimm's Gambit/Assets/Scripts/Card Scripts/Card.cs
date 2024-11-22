using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] private protected string cardName;

    [SerializeField] private protected int cardCost;

    [SerializeField] private protected Minion target = null;

    [SerializeField] private protected Minion caster = null; //Added by Ryan - 11/1/2024
    [SerializeField] private protected bool awaitingTarget;

    // Index in the database
    // Number that player has in their deck
    [SerializeField] private protected int m_Index = -1;
    [SerializeField ] private protected int m_PlayerCopies = 0;

    [SerializeField] private protected Card m_ReverseVersion;

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

    public bool CheckEphemeral()
    {
        return m_IsEphemeral;
    }

    public Minion GetCaster()
    {
        return caster;
    }

    public Minion GetTarget()
    {
        return target;
    }

    public Card GetReverse()
    {
        return m_ReverseVersion;
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

    // Assign index an index value to the card
    // Returns false if the card already has an index value
    // Returns true if successfully assigned
    public bool SetIndex(int i)
    {
        if (m_Index != -1)
        {
            Debug.Log("Card already has assigned index value.");
            return false;
        }
        m_Index = i;
        return true;
    }
}
