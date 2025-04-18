using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class OutcomeChanges
{
    public enum heirloomEffect {
        None,
        Add,
        Remove
    }
    [SerializeField] public heirloomEffect addHeirloom = heirloomEffect.None;
    [SerializeField] public Heirloom heirloomChange = 0;
    [SerializeField] public List<HPChange> partyHealth;
    [SerializeField] public bool cardDraft;
    [SerializeField] public int randomDiscard;
}

[CreateAssetMenu(fileName = "New Narrative Encounter", menuName = "NarrativeEncounters/Two-Choice Encounter", order = 0)]
public class NarrativeEncounter : ScriptableObject
{
    [SerializeField] protected string encounterName;//The name of the Encounter

[TextArea(15,20)]
    [SerializeField] protected string description;//The description text
    [SerializeField] protected string choice1, choice2; //The choice names

[TextArea(15,20)]
    [SerializeField] protected string choice1Outcome, choice2Outcome;//The outcomes in text 


    [SerializeField] protected string choice1Type, choice2Type;//The Types of Outcomes (for a future Switch statement)

    [SerializeField] protected OutcomeChanges outcomeChange1, outcomeChange2;

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
    public virtual void Choice(int outcome) {
        OutcomeChanges outcomeChange;

        outcomeChange = (outcome == 1) ? outcomeChange1 : outcomeChange2;

        PlayerData playerData = FindObjectOfType<PlayerData>();
        if (outcomeChange.partyHealth.Count > 0) {
            foreach (HPChange health in outcomeChange.partyHealth) {
                playerData.changeCurrentHP(health.partyMember, health.currentHP);
                playerData.changeMaxHP(health.partyMember, health.maxHP);
            }
        }

        if (outcomeChange.addHeirloom == OutcomeChanges.heirloomEffect.Add) {
            playerData.addHeirloom(outcomeChange.heirloomChange);
        } else if (outcomeChange.addHeirloom == OutcomeChanges.heirloomEffect.Remove) {
            playerData.removeHeirloom(outcomeChange.heirloomChange);
        }

        if (outcomeChange.cardDraft) {
            SceneManager.LoadScene("Encounter Victory Scene", LoadSceneMode.Additive);
        }

        if (outcomeChange.randomDiscard > 0) {
            playerData.randomDiscard(outcomeChange.randomDiscard);
        }
    }

//Going to be used to determine what the outcome type is for each choice
    public virtual void CheckOutcomeType(string outcomeType){
        switch (outcomeType){

            default:
                break;
        }
    }



    
}