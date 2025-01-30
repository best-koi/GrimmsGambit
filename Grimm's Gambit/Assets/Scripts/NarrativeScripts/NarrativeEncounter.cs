using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Narrative Encounter", menuName = "NarrativeEncounters/Two-Choice Encounter", order = 0)]
public class NarrativeEncounter : ScriptableObject
{
    [SerializeField] protected string encounterName;

    [SerializeField] protected string description;
    [SerializeField] protected string choice1, choice2; 

    [SerializeField]protected string choice1Outcome, choice2Outcome;

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
