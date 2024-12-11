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

    // Start is called before the first frame update
    void Start()
    {
        allFont = font;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(Input.GetKeyDown("1")){
            SceneManager.LoadScene("EnemyCombatScene");
        }
        */
    }

    public static TMP_FontAsset GetAllFont()
    {
        return allFont; 
    }
}
