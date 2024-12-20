using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//This is likely to be replaced by a CombatManager/Turn-Based Manager
public class CombatInventory : MonoBehaviour
{
    private static EnemyTemplate[] allEnemies;
    private static CharacterTemplate[] allCharacters;

    // Start is called before the first frame update
    void Start()
    {
        // Commenting out as I think this part is defunct for now 
        
        /**
        FindEnemies();
        FindCharacters();
        */
    }

    // Update is called once per frame
    void Update()
    {
        /**
         FindEnemies();
         FindCharacters();
         */
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

    private void EnemyTurn()
    {
        foreach(EnemyTemplate e in allEnemies)
        {
            e.AttackPattern();
            StartCoroutine("MoveDelay");
        }
    }

    IEnumerator MoveDelay()
    {
        yield return new WaitForSeconds(20);
    }
}
