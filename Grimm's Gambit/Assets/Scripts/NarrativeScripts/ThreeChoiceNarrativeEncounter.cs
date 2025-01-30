using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Narrative Encounter", menuName = "NarrativeEncounters/Three-Choice Encounter", order = 1)]
public class ThreeChoiceNarrativeEncounter : NarrativeEncounter
{
    [SerializeField] private string choice3; //Third choice text

    [TextArea(15,20)]
    [SerializeField]private string choice3Outcome;//Third outcome text

//Returns the text for third choice
    public string GetChoice3(){
        return choice3;
    }

//Returns the outcome for third choice (text)
    public string GetChoice3Outcome(){
        return choice3Outcome;
    }

//To be used by buttons
    public virtual void Choice3(){
        Debug.Log("Chose Choice 3");
        
    }




}
