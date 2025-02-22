using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.Collections;
using UnityEngine;
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
}

public class PlayerData : MonoBehaviour
{
    [SerializeField] public float volume = 0.5f;
    [SerializeField] public int currency;
    [SerializeField] public List<CardData> deck;
    [SerializeField] public List<Heirloom> heirlooms;
    [SerializeField] public HPChange seamstressStats, katzeStats, houndStats;

    public enum PartyMember {
        Party,
        Seamstress,
        Katze,
        Hound
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);

        seamstressStats.SetHealth(28);
        katzeStats.SetHealth(33);
        houndStats.SetHealth(38);
    }

    public void SetPlayerData(float volume) {
        this.volume = volume;
    }

    public float getVolume() {
        return volume;
    }
    
    public void setVolume(float volume) {
        this.volume = volume;
    }

    public void SetPlayerDeck(List<CardData> deck) {
        this.deck = deck;
    }

    public List<CardData> GetPlayerDeck() {
        return new List<CardData>(deck);
    }

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
            katzeStats.maxHP += amount;
            houndStats.maxHP += amount;
        } else {
            HPChange toChange = selectMember(ms);
            toChange.maxHP += amount;
        }
    }

    public ValueTuple<int,int> GetHP(PartyMember ms) {
        HPChange character = selectMember(ms);
        return (character.currentHP, character.maxHP);
    }

    public void SetHP(PartyMember ms, int current) {
        HPChange character = selectMember(ms);
        character.currentHP = current;
    }
}
