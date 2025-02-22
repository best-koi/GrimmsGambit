using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

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

    [SerializeField] private GameObject sceneParent;

    [SerializeField] private EnemyTemplate[] enemies; 

    private bool Tired = false; //Variable to control whether the player is tired
    private bool Greeding = false; //Variable to control whether the greedy jar effect is active
    private bool FirstCardFree = false; //Variable to control whether the buff for Faded Gold Silk is active
    private HeirloomManager heirloomManager; //Reference to the heirloom manager for heirloom checks
    private PlayerData playerData;

    private bool encounterEnded = false; 


    [SerializeField] 
    private string enemySceneName, encounterWinScene, encounterLossScene; 

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
        heirloomManager = FindObjectOfType<HeirloomManager>(); //This is done here so it is only done once per scene runtime iteration
        playerData = FindObjectOfType<PlayerData>();
        List<Heirloom> heirlooms = playerData.GetPlayerHeirlooms();
        List<CardData> playerDeck = playerData.GetPlayerDeck();
        if (playerDeck.Count > 0) {
            m_PlayerDeck.m_GameDeck = playerDeck;
        }
        if (heirlooms.Count > 0) {
            for (int i = 0; i < heirlooms.Count; i++) {
                heirloomManager.AddHeirloom(heirlooms[i]);
            }
        }
        StartEncounter();
    }

    private void Update()
    {
        if(m_TurnCounter <= 1) return; //edge case

        if(m_PlayerInventory.ChildListSize == 0 && encounterEnded == false){
            encounterEnded = true;
            EndEncounter(false);

        }else if ((enemies.Length == 0 || (enemies.Length == 1 && enemies[0] == null)) && encounterEnded == false){
            encounterEnded = true;
            EndEncounter(true);
        
        }
        
    }

    private void StartEncounter()
    {
        m_IsPlayerTurn = false;
        m_TurnCounter = 0;

        onTurnChanged = null;

        if (heirloomManager.ContainsHeirloom(Heirloom.Jar)) //Activates the jar of greed if owned
        {
            Greeding = true;
        }
        if (heirloomManager.ContainsHeirloom(Heirloom.Silk)) //ACtivates the Faded Gold Silk
        {
            FirstCardFree = true;
        }

        SetHealthValues();

        onEncounterStarted?.Invoke();

        m_PlayerDeck.ShuffleDeck();
        //Once there is a setup for general cards - Add in the blindfold generation of Devastation here - Noted by Ryan on 2/2/2025
        /*if (heirloomManager.ContainsHeirloom(Heirloom.Blindfold)) //Conjures Devastation at the start of a encounter if blindfold is active
        {
            m_PlayerDeck.Conjure()
        }*/
        EndTurn();
    }

    private void EndTurn()
    {
        m_IsPlayerTurn = !m_IsPlayerTurn;
        m_TurnCounter++;

        onTurnChanged?.Invoke(m_IsPlayerTurn);

        List<Transform> party = m_PlayerInventory.GetAll();

        //Implementation of the Flute of Hamelin: 2/2/25
        //Updates the value of max resources depending on whether the flute of hamelin is active at the end of the current turn
        if (heirloomManager.ContainsHeirloom(Heirloom.Hamelin))
        {
            m_MaxResources = 5;
        }
        else
        {
            m_MaxResources = 4;
        }
        
        
        m_CurrentResources = m_MaxResources;
        // m_ResourceText.text = $"Spirit: {m_CurrentResources} / {m_MaxResources}";
        m_ResourceText.text = $"{m_CurrentResources}"; //changed by Danielle

        if (m_IsPlayerTurn) {
            if (Tired) //Implemented by Ryan on 11/9/2024 to allow cards to let player become tired
            {
                m_CurrentResources--; //Reduces spirit by one on turn after tired is applied
                Tired = false; //Removes tired status
                m_ResourceText.text = $"{m_CurrentResources}"; //changed by Danielle
            }
            
            if (Greeding)
            {
                m_PlayerDeck.DrawAmount(false, 8); //Draws 8 cards instead of 6 if the greed turn is active
            }
            else
            {
                m_PlayerDeck.DrawAmount(true);
            }
            
            m_TurnText.text = "Player Turn";
        }
        else 
        {
            m_PlayerDeck.DiscardHand();
            
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
        SaveHealthValues();
        onEncounterEnded?.Invoke(playerWin);
        SceneManager.UnloadSceneAsync(enemySceneName);
        if(playerWin) SceneManager.LoadScene(encounterWinScene, LoadSceneMode.Additive);
        else SceneManager.LoadScene(encounterLossScene, LoadSceneMode.Additive);
    }

    // Spend an amount of resources
    // True if the amount is successfully spent
    // False if current resources are insufficient 
    public bool SpendResources(int amount)
    {
        if (FirstCardFree && amount != 0) //This logic assumes that first card free is only consumed when spirit would be spent
        {
            FirstCardFree = false; //Consumes the first card free buff for this combat
            return true;
        }
        else
        {
            if (m_CurrentResources - amount < 0)
            {
                Debug.Log("Insufficient resources");
                return false;
            }

            m_CurrentResources -= amount;
            m_ResourceText.text = $"{m_CurrentResources}"; //changed by Danielle

            return true;
        }
    }

    public bool EnoughResources(int amount)
    {
        return FirstCardFree || m_CurrentResources - amount >= 0;
    }


    public void BecomeTired() //Function to make player tired
    {
        Tired = true;
    }

    private void SetHealthValues() {
        Minion seamstress = GameObject.Find("Seamstress").GetComponent<Minion>();
        Minion katze = GameObject.Find("Katze").GetComponent<Minion>();
        Minion hound = GameObject.Find("Hound").GetComponent<Minion>();

        (seamstress.currentHealth, seamstress.maxHealth) = playerData.GetHP(PlayerData.PartyMember.Seamstress);
        (katze.currentHealth, katze.maxHealth) = playerData.GetHP(PlayerData.PartyMember.Katze);
        (hound.currentHealth, hound.maxHealth) = playerData.GetHP(PlayerData.PartyMember.Hound);
    }

    private void SaveHealthValues() {
        GameObject seamstress = GameObject.Find("Seamstress");
        GameObject katze = GameObject.Find("Katze");
        GameObject hound = GameObject.Find("Hound");

        if (seamstress == null) {
            int max = playerData.GetHP(PlayerData.PartyMember.Seamstress).Item2;
            playerData.SetHP(PlayerData.PartyMember.Seamstress, (int) Math.Ceiling(max*0.15));
        } else {
            playerData.SetHP(PlayerData.PartyMember.Seamstress, seamstress.GetComponent<Minion>().currentHealth);
        }

        if (katze == null) {
            int max = playerData.GetHP(PlayerData.PartyMember.Katze).Item2;
            playerData.SetHP(PlayerData.PartyMember.Katze, (int) Math.Ceiling(max*0.15));
        } else {
            playerData.SetHP(PlayerData.PartyMember.Katze, katze.GetComponent<Minion>().currentHealth);
        }

        if (hound == null) {
            int max = playerData.GetHP(PlayerData.PartyMember.Hound).Item2;
            playerData.SetHP(PlayerData.PartyMember.Hound, (int) Math.Ceiling(max*0.15));
        } else {
            playerData.SetHP(PlayerData.PartyMember.Hound, hound.GetComponent<Minion>().currentHealth);
        }
    }
}
