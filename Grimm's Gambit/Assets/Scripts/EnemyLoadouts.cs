using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLoadouts : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> enemyLoadouts;//a serializable list of names representing loadouts

    private static List<GameObject> availableLoadouts;//a list of enemy loadouts by name

    private static GameObject spawner;
    // Start is called before the first frame update
    void Start()
    {
        spawner = this.gameObject;
        
        availableLoadouts = enemyLoadouts;
        PickLoadout();
    }

public static void PickLoadout(){
    int randomLoadout = Random.Range(0, availableLoadouts.Count);
    GameObject spawnedEncounter = Instantiate (availableLoadouts[randomLoadout]) as GameObject;
    
    spawnedEncounter.transform.position = spawner.transform.position; 
    spawnedEncounter.transform.parent = spawner.transform;

     Debug.Log(availableLoadouts[randomLoadout]);
    availableLoadouts.RemoveAt(randomLoadout);
}
}
