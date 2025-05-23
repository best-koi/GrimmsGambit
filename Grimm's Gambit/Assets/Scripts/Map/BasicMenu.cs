using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BasicMenu : MonoBehaviour
{

    [SerializeField]
    private string startScene;

    public void LoadStartScene()
    {
        SceneManager.LoadScene(startScene);
    }

    public void UnloadCampfire()
    {
        SceneManager.UnloadSceneAsync("Campfire");
        MapPlayer.sceneToToggle.SetActive(true);
    }

    public void UnloadTutorialScene()
    {
        SceneManager.UnloadSceneAsync("TutorialScene");
        MapPlayer.sceneToToggle.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
