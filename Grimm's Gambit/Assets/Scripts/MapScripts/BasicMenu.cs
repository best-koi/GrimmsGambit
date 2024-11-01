using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BasicMenu : MonoBehaviour
{

    [SerializeField]
    private string startScene;

    [SerializeField]
    private string mapScene;

    [SerializeField]
    private string enemyScene;

    public void LoadStartScene()
    {
        SceneManager.LoadScene(startScene);
    }

    public void EnemyScene()
    {
        SceneManager.LoadScene(enemyScene);
    }

    public void MapScene()
    {
        SceneManager.LoadScene(mapScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
