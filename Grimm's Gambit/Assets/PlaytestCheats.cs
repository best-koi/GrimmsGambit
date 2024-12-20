using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
using TMPro;
public class PlaytestCheats : MonoBehaviour
{
    [SerializeField]
    private TMP_FontAsset font;

    private static TMP_FontAsset allFont;

    [SerializeField]
    private GameObject enemySceneItems;

    static GameObject allEnemySceneItems;

    [SerializeField] 
    private string enemySceneName; 

    // Start is called before the first frame update
    void Start()
    {
        allFont = font;
        allEnemySceneItems = enemySceneItems; 
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKeyDown("1")){
            SceneManager.LoadScene(enemySceneName);
        } else if (Input.GetKeyDown("2"))
        {
            allEnemySceneItems.SetActive(false);
            SceneManager.UnloadScene(enemySceneName);
            MapPlayer.sceneToToggle.SetActive(true);


        }
        
    }

    public static TMP_FontAsset GetAllFont()
    {
        return allFont; 
    }
}
