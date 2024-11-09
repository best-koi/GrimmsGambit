using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.AI;
using System.Data;

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

    [SerializeField] private CardHand m_CardHand;

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
        // Can theoritcally be removed later if no bugs 
        m_ResourceText.text = $"Spirit: {m_CurrentResources} / {m_MaxResources}";
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
            m_ResourceText.text = $"Spirit: {m_CurrentResources} / {m_MaxResources}";

            m_PlayerDeck.DrawAmount(true);
            

            m_TurnText.text = "Player Turn";

            // Display the number of cards in the player's deck
            Debug.Log("Deck Size: " + m_PlayerDeck.GetGameDeckSize());
        }
        else 
        {
            m_PlayerDeck.DiscardHand();
            ExecuteCards();


            m_TurnText.text = "Enemy Turn";

            foreach (GameObject enemy in enemies)
            {
                EnemyTemplate enemyController = enemy.GetComponent<EnemySpawner>().GetEnemy();
                enemyController.AttackPattern();

            }
            m_TurnText.text = "Enemy Turn";

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
        return true;
    }

    private void ExecuteCards()
    {
        foreach (GameObject player in m_PlayerInventory.GetAllMembers())
            if (player.TryGetComponent<Minion>(out Minion m))
                m.ConsumeCard();

        foreach (GameObject enemy in m_EnemyInventory.GetAllMembers())
            if (enemy.TryGetComponent<EnemySpawner>(out EnemySpawner es))
                es.GetSpawnedEnemy().GetComponent<Minion>().ConsumeCard();
    }
}
