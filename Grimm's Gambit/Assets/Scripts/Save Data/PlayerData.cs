using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField] public float volume = 0.5f;
    [SerializeField] public int currency;
    [SerializeField] public List<ShopItem> items;
    //[SerializeField] public List<CardV2> cards;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // private PlayerData(float volume = 0.5f) {
    //     this.volume = volume;
    // }

    public void SetPlayerData(float volume) {
        this.volume = volume;
    }

    public float getVolume() {
        return volume;
    }
    
    public void setVolume(float volume) {
        this.volume = volume;
    }

    public void addItem(ShopItem i) {
        items.Add(i);
    }
}
