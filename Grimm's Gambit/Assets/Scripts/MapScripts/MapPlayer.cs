using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Code by Jessie Archer
public class MapPlayer : MonoBehaviour
{
    [SerializeField]
    private MapEncounter currentLocation;//A GameObject representing a position on the map, will be changed for eventual Encounter object 

    private List<MapEncounter> locations;//A list representing available locations to move from

    [SerializeField]
    private int yOffset;//A value to put the player above the spot, rather than shrink them into it


    //SetPosition() sets the current player's location to new location
    //Also sets the available move locations to the player's list. 
    public void SetPosition(MapEncounter mapEncounter)
    {
        currentLocation = mapEncounter;
        gameObject.transform.position = currentLocation.gameObject.transform.position;
        locations = currentLocation.GetNextLocations();
    }

    //Checks to see if there are valid locations to move to
    public bool CheckLocations(MapEncounter encounter)
    {
        foreach(MapEncounter e in locations)
        {
            if (e == encounter)
                return true;
        }
        return false; 
    }

}
