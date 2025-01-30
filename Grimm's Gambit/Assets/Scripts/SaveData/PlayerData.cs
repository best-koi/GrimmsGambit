using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public float volume;

    void Start()
    {
        //DontDestroyOnLoad(this.gameObject);
    }

    private PlayerData(float volume = 0.5f) {
        this.volume = volume;
    }

    public void SetPlayerData(float volume) {
        this.volume = volume;
    }
    
    public void setVolume(float v) {
        this.volume = v;
    }
}
