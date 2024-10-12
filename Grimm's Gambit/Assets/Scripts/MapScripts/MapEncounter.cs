using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Code by Jessie Archer
public class MapEncounter : MonoBehaviour
{

    [SerializeField]
    private List<MapEncounter> nextLocations;//A list of spaces for the player to move next

    [SerializeField]
    bool isStartingSpace = false;//A boolean used to determine starting spaces on the board


    //GetNextLocations() returns a list of next moves to be used by the player
    public List<MapEncounter> GetNextLocations()
    {
        return nextLocations;
    }

    //OneMouseDown() detects if an object has been clicked and is able to move to it
    private void OnMouseDown()
    {
        if (isStartingSpace)
        {
            MapManager.GetPlayer().SetPosition(gameObject.GetComponent<MapEncounter>());
            Debug.Log("ClickWorked!");
        }
    }


}
