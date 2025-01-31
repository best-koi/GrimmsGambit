using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Narrative Encounter", menuName = "NarrativeEncounters/Two-Choice Encounter", order = 0)]
public class NarrativeEncounter : ScriptableObject
{
    [SerializeField] protected string encounterName;//The name of the Encounter

[TextArea(15,20)]
    [SerializeField] protected string description;//The description text
    [SerializeField] protected string choice1, choice2; //The choice names

[TextArea(15,20)]
    [SerializeField]protected string choice1Outcome, choice2Outcome;//The outcomes in text 


    [SerializeField]protected string choice1Type, choice2Type;//The Types of Outcomes (for a future Switch statement)

//returns name of encounter
    public string GetEncounterName(){
        return encounterName;
    }

//returns description
    public string GetDescription(){
        return description;
    }
//returns first choice text
    public string GetChoice1(){
        return choice1;
    }
//returns second choice text
    public string GetChoice2(){
        return choice2;
    }
//returns the first outcome text
    public string GetChoice1Outcome(){
        return choice1Outcome;
    }

//returns the second outcome text
    public string GetChoice2Outcome(){
        return choice2Outcome;
    }

//To be used by buttons in narrative generator
    public virtual void Choice1(){
        Debug.Log("Chose Choice 1");

    }

    public virtual void Choice2(){
        Debug.Log("Chose Choice 2");
    }

//Going to be used to determine what the outcome type is for each choice
    public virtual void CheckOutcomeType(string outcomeType){
        switch (outcomeType){

            default:
                break;
        }
    }



    
}
