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
    [SerializeField] public int m_TurnCounter; //Made public so katze can determine when to conjure coordinate strikes - Ryan 11/16/24

    // Max resource value to be implemented by design
    [SerializeField] private int m_MaxResources, m_CurrentResources;

    [SerializeField] private Button m_EndButton;
    [SerializeField] private TMP_Text m_TurnText, m_ResourceText;

    [SerializeField] private UnitParty m_PlayerInventory, m_EnemyInventory;

    [SerializeField] private CardHand m_CardHand;

    [SerializeField] private GameObject endScreenCanvas;

    [SerializeField] private string winText, loseText; 
    [SerializeField] private TMP_Text endScreenText; 

[SerializeField]
private EnemyTemplate[] enemies; 

    private bool Tired = false; //Variable to control whether the player is tired

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
        //endScreenCanvas.SetActive(false);
        m_EndButton.onClick.AddListener(EndTurn);
        StartEncounter();
    }

    private void Update()
    {
        if(m_PlayerInventory.ChildListSize == 0){
            EndEncounter(false);
        }else if (m_EnemyInventory.ChildListSize == 0){
            EndEncounter(true);
        }
        
    }

    private void StartEncounter()
    {
        m_IsPlayerTurn = false;
        m_TurnCounter = 0;

        onEncounterStarted?.Invoke();

        m_PlayerDeck.ShuffleDeck();
        EndTurn();
    }

    private void EndTurn()
    {
        m_IsPlayerTurn = !m_IsPlayerTurn;
        m_TurnCounter++;

        onTurnChanged?.Invoke(m_IsPlayerTurn);

        List<Transform> party = m_PlayerInventory.GetAll();

        m_CurrentResources = m_MaxResources;
        m_ResourceText.text = $"Spirit: {m_CurrentResources} / {m_MaxResources}";

        if (m_IsPlayerTurn) {
            if (Tired) //Implemented by Ryan on 11/9/2024 to allow cards to let player become tired
            {
                m_CurrentResources--; //Reduces spirit by one on turn after tired is applied
                Tired = false; //Removes tired status
            }
            
            m_PlayerDeck.DrawAmount(true);
            
            m_TurnText.text = "Player Turn";

            // Display the number of cards in the player's deck
            //Debug.Log("Deck Size: " + m_PlayerDeck.GetGameDeckSize());
        }
        else 
        {
            m_PlayerDeck.DiscardHand();
            ExecuteCards();

            m_TurnText.text = "Enemy Turn";

            StartCoroutine(EnemyTurn());
        } 
    }

//Used for slowing down enemy attacks. Each enemy performs their action. 
    IEnumerator EnemyTurn(){
        m_EndButton.gameObject.SetActive(false);

        enemies = m_EnemyInventory.gameObject.GetComponentsInChildren<EnemyTemplate>();
        
        List <EnemyTemplate> filteredEnemies = new List<EnemyTemplate>();
        foreach(EnemyTemplate e in enemies){
            if(e != null)
                filteredEnemies.Add(e);
                Debug.Log("Filtering");

        }
        
        Debug.Log(filteredEnemies.Count);
        enemies = filteredEnemies.ToArray(); 

        foreach (EnemyTemplate enemy in enemies)
            {
                Debug.Log("Attacking");
                if(enemy != null)
                    enemy.AttackPattern();
                yield return new WaitForSeconds(1.5f);
                //EnemyTemplate enemyController = enemy.GetComponent<EnemySpawner>().GetEnemy();
                
            }
        
        EndTurn();
        m_EndButton.gameObject.SetActive(true);

    }

    private void EndEncounter(bool playerWin) {
    
        onEncounterEnded?.Invoke(playerWin);

        if (playerWin){
            //endScreenCanvas.SetActive(true);
            //endScreenText.text = winText;

        }
        else{
            //endScreenCanvas.SetActive(true);
            //endScreenText.text = loseText; 

        }
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

    private void ExecuteCards()
    {
        foreach (Transform player in m_PlayerInventory.GetAll())
            if (player.TryGetComponent<Minion>(out Minion m))
                m.ConsumeCard();

        foreach (Transform enemy in m_EnemyInventory.GetAll())
            if (enemy.TryGetComponent<EnemySpawner>(out EnemySpawner es))
                es.GetSpawnedEnemy().GetComponent<Minion>().ConsumeCard();
    }

    public void BecomeTired() //Function to make player tired
    {
        Tired = true;
    }
}
