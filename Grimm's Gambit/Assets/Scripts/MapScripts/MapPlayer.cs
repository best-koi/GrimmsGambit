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
    private float yOffset;//A value to put the player above the spot, rather than shrink them into it

    [SerializeField]
    private float moveSpeed;

    private bool isAtLocation = true;

    private bool isMoving = false;

    private Vector3 centerLocation;


    //SetPosition() sets the current player's location to new location
    //Also sets the available move locations to the player's list. 
    public void SetPosition(MapEncounter mapEncounter)
    {
        currentLocation = mapEncounter;
        //Aims for the center of the object
        centerLocation = currentLocation.GetComponent<Renderer>().bounds.center;
        isAtLocation = false;
        locations = currentLocation.GetNextLocations();
        //gameObject.transform.position = new Vector3(currentLocation.gameObject.transform.position.x, currentLocation.gameObject.transform.position.y + yOffset, currentLocation.gameObject.transform.position.z);

    }

    //Checks to see if there are valid locations to move to
    public bool CheckLocations(MapEncounter encounter)
    {
        Debug.Log(isMoving);
        //Makes sure the player is not actively moving
        if (isMoving == false)
        {
            foreach (MapEncounter e in locations)
            {
                if (e == encounter)
                    return true;
            }
            return false;
        }
        
        return false; 
    }

    //Slides the player to a location as long as they are not already there
    private void Update()
    {
        if(isAtLocation == false)
        {
            isMoving = true;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3 (currentLocation.gameObject.transform.position.x, currentLocation.gameObject.transform.position.y + yOffset, currentLocation.gameObject.transform.position.z) , moveSpeed);
        }
    }

    //Checks if player has reached point
    private void OnCollisionEnter(Collision collision)
    {
            if (collision.gameObject.GetComponent<MapEncounter>() != null)
            {
                isAtLocation = true;
                isMoving = false;

            }
        
    }

 

}