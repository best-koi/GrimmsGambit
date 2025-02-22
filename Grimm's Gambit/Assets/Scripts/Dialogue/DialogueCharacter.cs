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


private void Start(){
    //finishSelectionButton.SetActive(false);
}
    public void CharacterSelected(){
        Debug.Log(characterName);
        dialogueHandler.SetCharacter(characterName);
        
        
    }


    public void OnMouseDown(){
        CharacterSelected();
    }
        
  


}
