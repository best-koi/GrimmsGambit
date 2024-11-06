using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class EncounterController : MonoBehaviour
{
    public static Action onEncounterStarted;

    // Player turn is true, enemy turn is false
    public static Action<bool> onTurnChanged;

    // True if player has won
    public static Action<bool> onEncounterEnded;

    [SerializeField] private Deck m_PlayerDeck;

    [SerializeField] private bool m_IsPlayerTurn;
    [SerializeField] private int m_TurnCounter;

    // Max resource value to be implemented by design
    [SerializeField] private int m_MaxResources, m_CurrentResources;

    [SerializeField] private Button m_EndButton;
    [SerializeField] private TMP_Text m_TurnText, m_ResourceText;

    [SerializeField] private UnitParty m_PlayerInventory, m_EnemyInventory;

    public UnitParty GetEnemyInventory()
    {
        return m_EnemyInventory;
    }

    public UnitParty GetPlayerInventory()
    {
        return m_PlayerInventory;
    }

    private void Start()
    {
        m_EndButton.onClick.AddListener(EndTurn);
    }

    private void Update()
    {
        
    }

    private void StartEncounter()
    {
        m_IsPlayerTurn = false;
        m_TurnCounter = 0;

        onEncounterStarted?.Invoke();

        m_EndButton.interactable = false;

        m_PlayerDeck.StartDeck();
        EndTurn();
    }

    private void EndTurn()
    {
        m_IsPlayerTurn = !m_IsPlayerTurn;
        m_TurnCounter++;

        onTurnChanged?.Invoke(m_IsPlayerTurn);

        List<GameObject> party = m_PlayerInventory.GetAllMembers(), enemies = m_EnemyInventory.GetAllMembers();

        //m_EndButton.interactable = m_IsPlayerTurn;

        if (m_IsPlayerTurn) {

            m_CurrentResources = m_MaxResources;

            m_PlayerDeck.DrawAmount(true);
            

            m_TurnText.text = "Player Turn";

            // Display the number of cards in the player's deck
            Debug.Log("Deck Size: " + m_PlayerDeck.GetGameDeckSize());
        }
        else 
        {
            m_PlayerDeck.DiscardHand();

            m_TurnText.text = "Enemy Turn";

            foreach (GameObject enemy in enemies)
            {
                EnemyTemplate enemyController = enemy.GetComponent<EnemySpawner>().GetEnemy();
                enemyController.AttackPattern();

            }
            //m_IsPlayerTurn = true;
            m_TurnText.text = "Enemy Turn";
            //m_EndButton.interactable = m_IsPlayerTurn;

        } 
    }

    private void EndEncounter(bool playerWin) {
    
        onEncounterEnded?.Invoke(playerWin);

        if (playerWin) Debug.Log("Player won in " + m_TurnCounter + " turns.");
        else Debug.Log("Player lost.");
    }


    // Spend an amount of resources
    // True if the amount is successfully spent
    // False if current resources are insufficient 
    public bool SpendResources(int amount)
    {
        if (m_CurrentResources - amount < 0)
        {
            Debug.Log("Insufficient resources");
            return false;
        }

        m_CurrentResources -= amount;
        m_ResourceText.text = $"Spirit: {m_CurrentResources} / {m_MaxResources}";
        return true;
    }
}
