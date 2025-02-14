using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField] public float volume = 0.5f;
    [SerializeField] public int currency;
    [SerializeField] public List<CardData> deck;
    [SerializeField] public List<Heirloom> heirlooms;

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
        return deck;
    }

    public void SetPlayerHeirlooms(List<Heirloom> heirlooms) {
        this.heirlooms = heirlooms;
    }

    public List<Heirloom> GetPlayerHeirlooms() {
        return heirlooms;
    }
}
