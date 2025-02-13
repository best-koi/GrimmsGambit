using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueCharacter : MonoBehaviour
{

    [SerializeField]
    private string characterName;

    [SerializeField]
    private DialogueHandler dialogueHandler; 

    [SerializeField]
    private GameObject finishSelectionButton; 

private void Start(){
    finishSelectionButton.SetActive(false);
}
    public void CharacterSelected(){
        Debug.Log(characterName);
        dialogueHandler.SetCharacter(characterName);
        if(finishSelectionButton.activeSelf == false){
            finishSelectionButton.SetActive(true);

        }
        
    }

    public void OnMouseDown(){
        CharacterSelected();
    }
        
  


}
