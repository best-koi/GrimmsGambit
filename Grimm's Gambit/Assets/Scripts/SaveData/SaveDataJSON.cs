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
    private AudioSource audioSettings;
    private Slider musicSlider;
    private string savePath = Application.dataPath + Path.AltDirectorySeparatorChar + "Saves" + Path.AltDirectorySeparatorChar + "SavaData.json";

    // Start is called before the first frame update
    void Awake()
    {
        playerData = FindObjectOfType<PlayerData>();

        //DontDestroyOnLoad(this.gameObject);
        // audioSettings = FindObjectOfType<AudioSource>();
        // LoadData();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        audioSettings = FindObjectOfType<AudioSource>();
        LoadData();
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void SaveData() {
        string jsonData = JsonUtility.ToJson(playerData);
        //Debug.Log(jsonData);
        using(StreamWriter writer = new StreamWriter(savePath)) {
            writer.Write(jsonData);
        }
        //Debug.Log("Saved!!!");
    }

    public void LoadData() {
        string jsonData = string.Empty;

        using(StreamReader reader = new StreamReader(savePath)) {
            jsonData = reader.ReadToEnd();
        }

        if (jsonData == null) return;

        JsonUtility.FromJsonOverwrite(jsonData, playerData);
        
        audioSettings.volume = playerData.volume;

        Debug.Log("Settings Loaded!");
    }

    public void LoadSettingsPage() {
        musicSlider = GameObject.FindGameObjectWithTag("MusicSlider").GetComponent<Slider>();
        musicSlider.value = playerData.volume;
    }
}
