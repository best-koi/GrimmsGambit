using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Narrative Encounter", menuName = "NarrativeEncounters", order = 0)]
public class NarrativeEncounter : ScriptableObject
{
    [SerializeField] private string encounterName;

    [SerializeField] private string description;
    [SerializeField] private string choice1;

    [SerializeField] private string choice2; 

    public string GetEncounterName(){
        return encounterName;
    }

    public string GetDescription(){
        return description;
    }

    public string GetChoice1(){
        return choice1;
    }

    public string GetChoice2(){
        return choice2;
    }



    
}
