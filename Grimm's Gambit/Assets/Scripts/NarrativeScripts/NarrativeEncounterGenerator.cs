using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class NarrativeEncounterGenerator : MonoBehaviour
{
    [SerializeField]
    private List<NarrativeEncounter> narrativeEncounters; //A list of all narrative encounters to pull from

    [SerializeField]
    private GameObject twoChoiceUI;

    private NarrativeEncounter currentEncounterSelected;

    [Header("Two-Choice UI Elements")]
    [SerializeField]
    private TMP_Text twoTitle, twoDesc, twoChoice1, twoChoice2, twoChoiceOutcome1, twoChoiceOutcome2;

    [SerializeField]
    public Button twoChoice1Button, twoChoice2Button;

    

[Header("Three-Choice UI Elements")]
    [SerializeField]
    private GameObject threeChoiceUI;


    [SerializeField]
    private TMP_Text threeChoiceTitle, threeChoiceDesc, threeChoice1, threeChoice2, threeChoice3, threeChoiceOutcome1, threeChoiceOutcome2, threeChoiceOutcome3;

    [SerializeField]
    public Button threeChoice1Button, threeChoice2Button, threeChoice3Button;
    

    public void GetRandomNarrativeEncounter(){

        int encounterToRemove = Random.Range(0, narrativeEncounters.Count);

        currentEncounterSelected = narrativeEncounters[encounterToRemove];

        if(currentEncounterSelected is ThreeChoiceNarrativeEncounter){
            ThreeChoiceNarrativeEncounter currentThree = currentEncounterSelected as ThreeChoiceNarrativeEncounter;

            threeChoiceUI.SetActive(true);

            //The Name and Description
            threeChoiceTitle.text = currentThree.GetEncounterName();
            threeChoiceDesc.text = currentThree.GetDescription();

            //The Choices
            threeChoice1.text = currentThree.GetChoice1();
            threeChoice2.text = currentThree.GetChoice2();
            threeChoice3.text = currentThree.GetChoice3();

            //The Outcomes
            threeChoiceOutcome1.text = currentThree.GetChoice1Outcome(); 
            threeChoiceOutcome2.text = currentThree.GetChoice2Outcome(); 
            threeChoiceOutcome3.text = currentThree.GetChoice3Outcome(); 


            threeChoice1Button.onClick.AddListener(delegate { currentThree.Choice1(); });
            threeChoice2Button.onClick.AddListener(delegate { currentThree.Choice2(); });
            threeChoice3Button.onClick.AddListener(delegate { currentThree.Choice3(); });

            


        }else{
            twoChoiceUI.SetActive(true);
            //The Text
            twoTitle.text = currentEncounterSelected.GetEncounterName();
            twoDesc.text = currentEncounterSelected.GetDescription();

            //The Choices
            twoChoice1.text = currentEncounterSelected.GetChoice1();
            twoChoice2.text = currentEncounterSelected.GetChoice2();

            //The Outcomes 
            twoChoiceOutcome1.text = currentEncounterSelected.GetChoice1Outcome(); 
            twoChoiceOutcome2.text = currentEncounterSelected.GetChoice2Outcome();


            twoChoice1Button.onClick.AddListener(delegate { currentEncounterSelected.Choice1(); });
            twoChoice2Button.onClick.AddListener(delegate { currentEncounterSelected.Choice2(); });

        }
        narrativeEncounters.RemoveAt(encounterToRemove);

    }

    public void CloseThreeChoice(){
        threeChoiceUI.SetActive(false);


    }

    public void CloseTwoChoice(){
        twoChoiceUI.SetActive(false);
        
    }

     public void UnsubscribeButtons()
    {
        twoChoice1Button.onClick.RemoveAllListeners();
        twoChoice2Button.onClick.RemoveAllListeners();
        threeChoice1Button.onClick.RemoveAllListeners();
        threeChoice2Button.onClick.RemoveAllListeners();
        threeChoice3Button.onClick.RemoveAllListeners();
    }





    
}
