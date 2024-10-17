using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Code by Jessie Archer
public class MapManager : MonoBehaviour
{
    static MapPlayer player;//The player used on the map scene

    static bool startingSpaceUsed = false;//Checks to see if the player has moved to a starting space. Prevents moving horizontally

    [SerializeField]
    private List<GameObject> mapPrefabs;

    static private GameObject roundMap;

    //Finds the player in the scene
    void Start()
    {
        player = FindObjectOfType<MapPlayer>();
        if(roundMap == null)
        roundMap = Instantiate(mapPrefabs[Random.Range(0, mapPrefabs.Count)]);
        
    }

    //Retrieves the player
    public static MapPlayer GetPlayer()
    {
        //Returns the player character
        return player; 
    }

    //Checks if the starting space has been used
    public static bool GetStartingSpaceUsed()
    {
        return startingSpaceUsed;
    }

    //Sets the starting space to used 
    public static void SetStartingSpaceUsed()
    {
        startingSpaceUsed = true;
    }
}
