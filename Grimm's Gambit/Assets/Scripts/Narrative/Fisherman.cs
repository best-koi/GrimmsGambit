using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Narrative Encounter", menuName = "NarrativeEncounters/Special Cases/Fisherman", order = 2)]
public class Fisherman : NarrativeEncounter
{

    [SerializeField]
    private string originalOutcome1;


    [SerializeField]
    private List<string> outcomeTexts; 

    [SerializeField]
    private List<string> outcomeTypes; 

 
  
  private void Start(){
    choice1Outcome = originalOutcome1; 
        

  }
    //Picks a random outcome for the positive choice
    protected virtual int CoinFlip(){
        return Random.Range(0,2);
        
    }

    public override void Choice1(){
        int outcome = CoinFlip();
        choice1Outcome = $"{originalOutcome1}  {outcomeTexts[outcome]}";
        choice1Type = outcomeTypes[outcome];
       
    }
}
