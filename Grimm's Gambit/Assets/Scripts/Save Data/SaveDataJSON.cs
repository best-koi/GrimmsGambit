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
    
    [SerializeField] string mapName = "Map";
    
    
    private string saveFolder = Application.dataPath + Path.AltDirectorySeparatorChar + "Saves";
    private string savePath = Application.dataPath + Path.AltDirectorySeparatorChar + "Saves" + Path.AltDirectorySeparatorChar + "SavaData.json";

    void Awake()
    {
        PlayerData[] pdList = FindObjectsOfType<PlayerData>();
        if (pdList.Length > 1) {
            for (int i = pdList.Length - 1; i > 0; i--) {
                Destroy(pdList[i].gameObject);
            }
        }
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
        string jsonData = JsonUtility.ToJson(playerData, true);

        if (!Directory.Exists(saveFolder)) {
            Directory.CreateDirectory(saveFolder);
            File.Create(savePath).Close();
        }
        
        using(StreamWriter writer = new StreamWriter(savePath)) {
            writer.Write(jsonData);
            writer.Close();
        }
    }

    // Sets default Save Values
    public void SetDefaultValues() {
        playerData.SetDefaultHP();
        playerData.SetDefaultCards();
        playerData.ClearHeirlooms();
        playerData.SetPosition(new Vector3(0, 0, 0));
    }

    // Upon opening the settings page, all relevant settings will reflect the values stored by playerData
    public void LoadSettingsPage() {
        Slider musicSlider = GameObject.FindGameObjectWithTag("MusicSlider").GetComponent<Slider>();
        musicSlider.value = playerData.getVolume();
    }

    // Loads data from JSON save file, stores in playerData
    public void LoadIntoPlayerData() {
        string jsonData = string.Empty;

        if (!Directory.Exists(saveFolder)) {
            Directory.CreateDirectory(saveFolder);
            File.Create(savePath).Close();
        } else if (!File.Exists(savePath)) {
            File.Create(savePath).Close();
        }

        using(StreamReader reader = new StreamReader(savePath)) {
            Debug.Log(savePath);
            jsonData = reader.ReadToEnd();
            reader.Close();
        }

        if (jsonData == null) return;

        JsonUtility.FromJsonOverwrite(jsonData, playerData);
    }

    // Sets all relevant internal values to those held by playerData
    public void LoadFromPlayerData() {
        AudioSource audioSettings = FindObjectOfType<AudioSource>();
        if (audioSettings != null) {
            audioSettings.volume = playerData.getVolume();
        }
    }

    // Used by music slider to set volume
    public void setVolume(float volume) {
        playerData.setVolume(volume);
    }

    // Called by Quit button, prevents multiple playerData copies spawning by returning to Start Menu
    public void DestroyPlayerData() {
        //Destroy(playerData.gameObject);
    }

    public void newGame() {
        SetDefaultValues();
        SceneManager.LoadScene(mapName);
    }

    public void loadGame() {
        LoadIntoPlayerData();
        SceneManager.LoadScene(mapName);
    }
}
