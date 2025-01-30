using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NarrativeEncounterGenerator : MonoBehaviour
{
    [SerializeField]
    private List<NarrativeEncounter> narrativeEncounters; //A list of all narrative encounters to pull from

    [SerializeField]
    private GameObject twoChoiceUI;

    [Header("General UI Elements")]
    [SerializeField]
    private TMP_Text twoTitle, twoDesc, twoChoice1, twoChoice2, twoChoiceOutcome1, twoChoiceOutcome2;


    [SerializeField]
    private GameObject threeChoiceUI;
    
    [Header("Three Choice UI Elements")]
    [SerializeField]
    private TMP_Text threeChoiceTitle, threeChoiceDesc, threeChoice1, threeChoice2, threeChoice3, threeChoiceOutcome1, threeChoiceOutcome2, threeChoiceOutcome3;





    
}
