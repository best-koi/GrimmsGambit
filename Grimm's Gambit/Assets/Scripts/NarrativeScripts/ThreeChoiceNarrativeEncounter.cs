using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Narrative Encounter", menuName = "NarrativeEncounters/Three-Choice Encounter", order = 1)]
public class ThreeChoiceNarrativeEncounter : NarrativeEncounter
{
    [SerializeField] private string choice3; 

    [SerializeField]private string choice3Outcome;

    public string GetChoice3(){
        return choice3;
    }

    public string GetChoice3Outcome(){
        return choice3;
    }



}
