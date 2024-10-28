using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is likely to be replaced by a CombatManager/Turn-Based Manager
public class CombatInventory : MonoBehaviour
{

    private static EnemyTemplate[] allEnemies;


    // Start is called before the first frame update
    void Start()
    {
        FindEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        FindEnemies();
    }

    //Retrieves all active enemies
    private void FindEnemies()
    {
        allEnemies = FindObjectsOfType(typeof(EnemyTemplate)) as EnemyTemplate[];
    }

    //Returns the array of enemies
    public static EnemyTemplate[] GetActiveEnemies()
    {
        return allEnemies;
    }
}
