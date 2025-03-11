using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OddsDialogueHandler : PostDialogueHandler
{

      [Space] [SerializeField] protected string playerEnding;

    protected override void _endPostDialogue()
    {
        RenderSettings.fog = true;
        
        SceneManager.UnloadSceneAsync(endingScene);
        SceneManager.LoadScene(playerEnding);
        
        //MapPlayer.sceneToToggle.SetActive(true);
        SaveDataJSON save = FindObjectOfType<SaveDataJSON>();
        save.LoadFromPlayerData();
        

    }
}
