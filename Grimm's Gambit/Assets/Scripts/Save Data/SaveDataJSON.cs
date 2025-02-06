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

    // Start is called before the first frame update
    void Awake()
    {
        playerData = FindObjectOfType<PlayerData>();
        if (playerData == null) {
            playerData = new PlayerData();
            LoadIntoPlayerData();
        }

        if (gameStart) {
            LoadIntoPlayerData();
        }

        LoadFromPlayerData();
    }

    // void OnEnable()
    // {
    //     SceneManager.sceneLoaded += OnSceneLoaded;
    // }

    // void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    // {
    //     LoadFromPlayerData();
    // }

    // void OnDisable()
    // {
    //     SceneManager.sceneLoaded -= OnSceneLoaded;
    // }

    public void SaveData() {
        string jsonData = JsonUtility.ToJson(playerData);
        
        using(StreamWriter writer = new StreamWriter(savePath)) {
            writer.Write(jsonData);
        }
    }

    public void LoadSettingsPage() {
        Slider musicSlider = GameObject.FindGameObjectWithTag("MusicSlider").GetComponent<Slider>();
        musicSlider.value = playerData.getVolume();
    }

    public void LoadIntoPlayerData() {
        string jsonData = string.Empty;

        using(StreamReader reader = new StreamReader(savePath)) {
            jsonData = reader.ReadToEnd();
        }

        if (jsonData == null) return;

        JsonUtility.FromJsonOverwrite(jsonData, playerData);

        Debug.Log("Settings Loaded!");
    }

    public void LoadFromPlayerData() {
        AudioSource audioSettings = FindObjectOfType<AudioSource>();
        audioSettings.volume = playerData.getVolume();
    }

    public void setVolume(float volume) {
        playerData.setVolume(volume);
    }
}
