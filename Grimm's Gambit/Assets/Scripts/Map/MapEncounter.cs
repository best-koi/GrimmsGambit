using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

//Code by Jessie Archer
public class MapEncounter : MonoBehaviour
{

    [SerializeField]
    private List<MapEncounter> nextLocations;//A list of spaces for the player to move next

    [SerializeField]
    bool isStartingSpace = false;//A boolean used to determine starting spaces on the board

    [SerializeField]
    private TMP_Text displayText;//The display text itself

    [SerializeField]
    private Image image; 

    // [SerializeField]
    // private Sprite icon; 

    [SerializeField]
    private GameObject uiCanvas;//A canvas to hide and show

    [SerializeField]
    private string locationName;//The name of the location

    private void Start()
    {
        //displayText.gameObject.SetActive(false);
        // image.sprite = icon; 
        uiCanvas.SetActive(false);
        
    }

    //GetNextLocations() returns a list of next moves to be used by the player
    public List<MapEncounter> GetNextLocations()
    {
        return nextLocations;
    }

    //OneMouseDown() detects if an object has been clicked and is able to move to it
    private void OnMouseDown()
    {
        if (isStartingSpace && MapManager.GetStartingSpaceUsed() == false)
        {
            MapManager.SetStartingSpaceUsed();
            MapManager.GetPlayer().SetPosition(gameObject.GetComponent<MapEncounter>());
            Debug.Log("ClickWorked!");
        }
        else
        {
            if (MapManager.GetPlayer().CheckLocations(this))
            {
                if (locationName == "Campfire") {
                    PlayerData playerData = FindObjectOfType<PlayerData>();
                    playerData.restoreAll();
                }

                MapManager.GetPlayer().SetPosition(gameObject.GetComponent<MapEncounter>());
            }
        }
    }

    //Shows what location is On Mouse Hover
    private void OnMouseOver()
    {
        //Checks to see if the player can view this or it's a starting location
        if ((isStartingSpace == true && MapManager.GetStartingSpaceUsed() == false) || MapManager.GetPlayer().CheckLocations(this))
        {
            uiCanvas.SetActive(true);
            displayText.text = locationName;

        }
        
    }

    //Hides text when Mouse is not hovering over location
    private void OnMouseExit()
    {
        uiCanvas.gameObject.SetActive(false);
    }

}
