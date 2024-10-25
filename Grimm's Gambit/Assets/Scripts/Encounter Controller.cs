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

    [SerializeField] private Deck m_PlayerDeck, m_EnemyDeck;

    [SerializeField] private bool m_IsPlayerTurn;
    [SerializeField] private int m_TurnCounter;

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    private void StartEncounter()
    {
        m_IsPlayerTurn = true;
        m_TurnCounter = 0;

        onEncounterStarted?.Invoke();

        m_PlayerDeck.StartDeck();
        m_EnemyDeck.StartDeck();
    }

    private void EndTurn()
    {
        m_IsPlayerTurn = !m_IsPlayerTurn;
        m_TurnCounter++;

        onTurnChanged?.Invoke(m_IsPlayerTurn);

        if (m_IsPlayerTurn) m_PlayerDeck.Draw();
        else m_EnemyDeck.Draw();
    }

    private void EndEncounter(bool playerWin) {
    
        onEncounterEnded?.Invoke(playerWin);

        if (playerWin) Debug.Log("Player won in " + m_TurnCounter + " turns.");
        else Debug.Log("Player lost.");
    }
}
