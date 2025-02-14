using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Text.Json;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SaveDataJSON : MonoBehaviour
{
    private PlayerData playerData;

    // Check on title screen to load save from JSON
    [SerializeField] public bool gameStart;
    
    
    private string savePath = Application.dataPath + Path.AltDirectorySeparatorChar + "Saves" + Path.AltDirectorySeparatorChar + "SavaData.json";

    void Awake()
    {
        playerData = FindObjectOfType<PlayerData>();

        // For debugging purposes only, prevents issues when starting from scenes other than title screen
        if (playerData == null) {
            playerData = new PlayerData();
            LoadIntoPlayerData();
        }

        // Only called on title screen
        if (gameStart) {
            LoadIntoPlayerData();
        }

        LoadFromPlayerData();
    }

    // Saves playerData to JSON save file
    public void SaveData() {
        string jsonData = JsonUtility.ToJson(playerData);
        
        using(StreamWriter writer = new StreamWriter(savePath)) {
            writer.Write(jsonData);
        }
    }

    // Upon opening the settings page, all relevant settings will reflect the values stored by playerData
    public void LoadSettingsPage() {
        Slider musicSlider = GameObject.FindGameObjectWithTag("MusicSlider").GetComponent<Slider>();
        musicSlider.value = playerData.getVolume();
    }

    // Loads data from JSON save file, stores in playerData
    public void LoadIntoPlayerData() {
        string jsonData = string.Empty;

        using(StreamReader reader = new StreamReader(savePath)) {
            jsonData = reader.ReadToEnd();
        }

        if (jsonData == null) return;

        JsonUtility.FromJsonOverwrite(jsonData, playerData);

        Debug.Log("Settings Loaded!");
    }

    // Sets all relevant internal values to those held by playerData
    public void LoadFromPlayerData() {
        AudioSource audioSettings = FindObjectOfType<AudioSource>();
        audioSettings.volume = playerData.getVolume();
    }

    // Used by music slider to set volume
    public void setVolume(float volume) {
        playerData.setVolume(volume);
    }
}
