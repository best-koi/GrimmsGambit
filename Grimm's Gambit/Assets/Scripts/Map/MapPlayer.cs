using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Code by Jessie Archer
public class MapPlayer : MonoBehaviour
{
    [SerializeField]
    private MapEncounter currentLocation;//A GameObject representing a position on the map, will be changed for eventual Encounter object 

    [SerializeField]
    private NarrativeEncounterGenerator encounterGenerator; 

[SerializeField]
    private List<MapEncounter> locations;//A list representing available locations to move from

    [SerializeField]
    private float yOffset;//A value to put the player above the spot, rather than shrink them into it

    [SerializeField]
    private float moveSpeed;

    private bool isAtLocation = true;

    private bool isMoving = false;

    private Vector3 centerLocation;

    [SerializeField]
    private string encounterScene, campfireScene;

    [SerializeField]
    private string lycanScene, sistersScene, oddsScene, beldamScene, ladyScene;

    [SerializeField]
    private GameObject sceneObjects;

    public static GameObject sceneToToggle;

    static bool isInCombat = false; 

    public void Start(){
        sceneToToggle = sceneObjects; 
    }


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
        //Debug.Log(isMoving);
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<MapEncounter>() != null)
        {
            Debug.Log("MetCondition");
            isAtLocation = true;
            isMoving = false;

        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        switch(collision.gameObject.tag){
            case "Encounter":
            collision.gameObject.tag = "Inactive";
            SceneManager.LoadScene(encounterScene, LoadSceneMode.Additive);
            sceneToToggle.SetActive(false);
            break;
            case "Lycan":
             collision.gameObject.tag = "Inactive";
            SceneManager.LoadScene(lycanScene, LoadSceneMode.Additive);
            sceneToToggle.SetActive(false);
            break;
            case "Sisters":
             collision.gameObject.tag = "Inactive";
            SceneManager.LoadScene(sistersScene, LoadSceneMode.Additive);
            sceneToToggle.SetActive(false);
            break;
            case "Narrative":
            collision.gameObject.tag = "Inactive";
            encounterGenerator.GetRandomNarrativeEncounter(); 
            break;
            case "Campfire":
             collision.gameObject.tag = "Inactive";
            SceneManager.LoadScene(campfireScene, LoadSceneMode.Additive);
            sceneToToggle.SetActive(false);
            break;
            case "Odds":
            collision.gameObject.tag = "Inactive";
            SceneManager.LoadScene(oddsScene, LoadSceneMode.Additive);
            sceneToToggle.SetActive(false);
            break;

            case "LadyOfLake":
            collision.gameObject.tag = "Inactive";
            SceneManager.LoadScene(ladyScene, LoadSceneMode.Additive);
            sceneToToggle.SetActive(false);
            RenderSettings.fog = false;
            break;

            case "Beldam":
            collision.gameObject.tag = "Inactive";
            SceneManager.LoadScene(beldamScene, LoadSceneMode.Additive);
            sceneToToggle.SetActive(false);
            //RenderSettings.fog = false;
            break;
            
            default:
            break; 






        }
      
    }

   



}
