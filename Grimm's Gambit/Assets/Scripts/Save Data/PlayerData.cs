using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField] public float volume = 0.5f;
    [SerializeField] public int currency;
    [SerializeField] public List<CardData> deck;
    [SerializeField] public List<Heirloom> heirlooms;
    [SerializeField] public int currentHPChange;
    [SerializeField] public int maxHPChange;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
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

    public void changeCurrentHP(int amount) {
        currentHPChange += amount;
    }

    public void changeMaxHP(int amount) {
        maxHPChange += amount;
    }
}
