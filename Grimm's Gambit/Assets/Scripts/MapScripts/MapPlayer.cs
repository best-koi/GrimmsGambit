using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Code by Jessie Archer
public class MapPlayer : MonoBehaviour
{
    [SerializeField]
    private MapEncounter currentLocation;//A GameObject representing a position on the map, will be changed for eventual Encounter object 

    private List<MapEncounter> locations;


    //SetPosition() sets the current player's location to new location
    //Also sets the available move locations to the player's list. 
    public void SetPosition(MapEncounter mapEncounter)
    {
        currentLocation = mapEncounter;
        gameObject.transform.position = currentLocation.gameObject.transform.position;
        locations = currentLocation.GetNextLocations();
    }


}
