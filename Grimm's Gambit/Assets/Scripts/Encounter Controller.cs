using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    [SerializeField] private int m_MaxResources, m_CurrentResources;

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    private void StartEncounter()
    {
        m_IsPlayerTurn = false;
        m_TurnCounter = 0;

        onEncounterStarted?.Invoke();

        m_PlayerDeck.StartDeck();
        EndTurn();
    }

    private void EndTurn()
    {
        m_IsPlayerTurn = !m_IsPlayerTurn;
        m_TurnCounter++;

        onTurnChanged?.Invoke(m_IsPlayerTurn);

        if (m_IsPlayerTurn) {
            m_PlayerDeck.Draw();
            m_CurrentResources = m_MaxResources;

            // Display the number of cards in the player's deck
            Debug.Log("Deck Size: " + m_PlayerDeck.GetGameDeckSize());
        }
        // Unfinished
        else Debug.Log("Enemy minion attacks."); 
    }

    private void EndEncounter(bool playerWin) {
    
        onEncounterEnded?.Invoke(playerWin);

        if (playerWin) Debug.Log("Player won in " + m_TurnCounter + " turns.");
        else Debug.Log("Player lost.");
    }
}
