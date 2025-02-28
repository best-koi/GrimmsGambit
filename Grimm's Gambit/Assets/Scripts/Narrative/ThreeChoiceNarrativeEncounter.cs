using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Narrative Encounter", menuName = "NarrativeEncounters/Three-Choice Encounter", order = 1)]
public class ThreeChoiceNarrativeEncounter : NarrativeEncounter
{
    [SerializeField] private OutcomeChanges outcomeChange3;
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
    public override void Choice(int outcome) {
        OutcomeChanges outcomeChange;

        if (outcome == 1) outcomeChange = outcomeChange1;
        else if (outcome == 2) outcomeChange = outcomeChange2;
        else outcomeChange = outcomeChange3;

        PlayerData playerData = FindObjectOfType<PlayerData>();
        if (outcomeChange.partyHealth.Count > 0) {
            //playerData.changeCurrentHP(PlayerData.PartyMember.Party, outcomeChange.currentHPChange);
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

        if (outcomeChange.cardChange.ownerIndex != -1) {

        }
    }




}
