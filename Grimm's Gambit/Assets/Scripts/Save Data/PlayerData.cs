using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Transactions;
using Unity.Collections;
//using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

[Serializable]
public class HPChange {
    public int currentHP;
    public int maxHP;
    public PlayerData.PartyMember partyMember;

    public HPChange(int cur, int max, PlayerData.PartyMember mem) {
        currentHP = cur;
        maxHP = max;
        partyMember = mem;
    }

    // Sets current and max health to amount
    public void SetHealth(int amount) {
        currentHP = amount;
        maxHP = amount;
    }

    public void RestoreHealth() {
        currentHP = maxHP;
    }
}

public class PlayerData : MonoBehaviour
{
    [SerializeField] public float volume = 0.5f;
    [SerializeField] public List<CardData> deck;
    [SerializeField] public List<Heirloom> heirlooms;
    [SerializeField] public HPChange seamstressStats, katzeStats, houndStats;
    [SerializeField] public UnityEngine.Vector3 position;

    // Dialogue/Canon choices will be stored by a dictionary -- int ChoiceID, bool Choice
    // ID Pattern: 1st digit - Campfire/Canon Number, 2nd Digit - Character (0: Canon Event, 1: Seamstress, 2: Katze, 3: Hound)
    // Choice: true - good, false - bad
    public Dictionary<int, bool> dialogueChoices;
    [SerializeField] public List<int> dialogueChoiceKeys;
    [SerializeField] public List<bool> dialogueChoiceValues;

    public enum PartyMember {
        Party,
        Seamstress,
        Katze,
        Hound
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);

        restoreAll();

        dialogueChoices = dialogueChoiceKeys.Zip(dialogueChoiceValues, (k, v) => new {k, v}).ToDictionary(x => x.k, x => x.v);
    }

    public void SetPlayerData(float volume) {
        this.volume = volume;
    }

    // Volume Functitons 
    public float getVolume() {
        return volume;
    }
  
    public void setVolume(float volume) {
        this.volume = volume;
    }

    // Deck Functions
    public void SetPlayerDeck(List<CardData> deck) {
        this.deck = deck;
    }

    public List<CardData> GetPlayerDeck() {
        return new List<CardData>(deck);
    }

    public void addCard(CardData card) {
        deck.Add(card);
    }

    public void randomDiscard(int quantity) {
        for (int i = 0; i < quantity; i++) {
            System.Random rnd = new System.Random();
            int toRemove = rnd.Next(0, deck.Count);
            deck.RemoveAt(toRemove);
        }
    }

    public void SetDefaultCards() {
        deck.Clear();
        deck.Add(new CardData(2,3));
        deck.Add(new CardData(2,2));
        deck.Add(new CardData(2,8));
        deck.Add(new CardData(0,1));
        deck.Add(new CardData(0,8));
        deck.Add(new CardData(0,1));
        deck.Add(new CardData(1,0));
        deck.Add(new CardData(1,5));
        deck.Add(new CardData(1,8));
    }

    // Heirloom Functions
    public void SetPlayerHeirlooms(List<Heirloom> heirlooms) {
        this.heirlooms = heirlooms;
    }

    public List<Heirloom> GetPlayerHeirlooms() {
        return new List<Heirloom>(heirlooms);
    }

    public void addHeirloom(Heirloom heirloom) {
        heirlooms.Add(heirloom);
    }

    public void removeHeirloom(Heirloom heirloom) {
        heirlooms.Remove(heirloom);
    }

    public void ClearHeirlooms() {
        heirlooms.Clear();
    }

    // Health Functions
    private HPChange selectMember(PartyMember ms) {
        switch (ms) {
            case PartyMember.Seamstress:
                return seamstressStats;
            case PartyMember.Katze:
                return katzeStats;
            case PartyMember.Hound:
                return houndStats;
            default:
                return seamstressStats;
        }
    }

    public void changeCurrentHP(PartyMember ms, int amount) {
        if (ms == PartyMember.Party) {
            seamstressStats.currentHP += amount;
            katzeStats.currentHP += amount;
            houndStats.currentHP += amount;
        } else {
            HPChange toChange = selectMember(ms);
            toChange.currentHP += amount;
        }
    }

    public void changeMaxHP(PartyMember ms, int amount) {
        if (ms == PartyMember.Party) {
            seamstressStats.maxHP += amount;
            seamstressStats.currentHP += amount;
            katzeStats.maxHP += amount;
            katzeStats.currentHP += amount;
            houndStats.maxHP += amount;
            houndStats.currentHP += amount;
        } else {
            HPChange toChange = selectMember(ms);
            toChange.maxHP += amount;
            toChange.currentHP += amount;
        }
    }

    public ValueTuple<int,int> GetHP(PartyMember ms) {
        HPChange character = selectMember(ms);
        return (character.currentHP, character.maxHP);
    }

    public void SetHP(PartyMember ms, int current) {
        HPChange character = selectMember(ms);
        character.SetHealth(current);
    }

    public void SetDefaultHP() {
        seamstressStats.SetHealth(28);
        katzeStats.SetHealth(33);
        houndStats.SetHealth(38);
    }

    public void restoreAll() {
        seamstressStats.RestoreHealth();
        katzeStats.RestoreHealth();
        houndStats.RestoreHealth();
    }

    // Narrative Choice Functions
    public void addChoice(int choiceID, bool choice) {
        dialogueChoices.Add(choiceID, choice);
        dialogueChoiceKeys.Add(choiceID);
        dialogueChoiceValues.Add(choice);
    }

    public bool getChoice(int choiceID) {
        return dialogueChoices[choiceID];
    }

    // Map Functions
    public UnityEngine.Vector3 GetPosition() {
        return position;
    }
    
    public void SetPosition(UnityEngine.Vector3 pos) {
        position = pos;
    }
}
