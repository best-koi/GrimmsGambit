using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//This is likely to be replaced by a CombatManager/Turn-Based Manager
public class CombatInventory : MonoBehaviour
{

    private static EnemyTemplate[] allEnemies;
    private static CharacterTemplate[] allCharacters;

    [SerializeField]
    private TMP_Text turnText;

    [SerializeField]
    private GameObject turnButton;

    private static bool isPlayerTurn = true;

    // Start is called before the first frame update
    void Start()
    {
        FindEnemies();
        FindCharacters();
    }

    // Update is called once per frame
    void Update()
    {
        FindEnemies();
        FindCharacters();
        SetTurnUI();

    }

    //Retrieves all active enemies
    private void FindEnemies()
    {
        allEnemies = FindObjectsOfType(typeof(EnemyTemplate)) as EnemyTemplate[];
    }

    //Retrieves all active enemies
    private void FindCharacters()
    {
        allCharacters = FindObjectsOfType(typeof(CharacterTemplate)) as CharacterTemplate[];
    }

    //Returns the array of enemies
    public static EnemyTemplate[] GetActiveEnemies()
    {
        return allEnemies;
    }
    public static CharacterTemplate[] GetActiveCharacters()
    {
        return allCharacters;
    }

    public static void SetPlayerTurnOver(bool currentTurn)
    {
        isPlayerTurn = currentTurn;
    }

    private void SetTurnUI()
    {
        if(isPlayerTurn)
        {
            turnButton.SetActive(true);
            turnText.text = "Player Turn";
  
        }
        else
        {
            turnButton.SetActive(false);
            turnText.text = "Enemy Turn";
            EnemyTurn();
        }
    }

    private void EnemyTurn()
    {
        foreach(EnemyTemplate e in allEnemies)
        {
            e.AttackPattern();
            StartCoroutine("MoveDelay");
        }
        isPlayerTurn = true;
    }

    IEnumerator MoveDelay()
    {
        yield return new WaitForSeconds(20);
    }
}
