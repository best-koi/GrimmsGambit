using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DialogueHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject seamstress, hound, katze, heir; //Sprites for each character, to be toggled on and off 

    [SerializeField]
    private TMP_Text speakerText, conversationText, chosenCharacterText; 

    private string chosenCharacter;

    [SerializeField]
    private GameObject talkButton, shopButton, party, talkPanel, dialogueWindow;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        chosenCharacterText.text = chosenCharacter; 
    }

    public void SetCharacter(string characterName){
        chosenCharacter = characterName; 

    }

    public void ChooseTalk(){
        talkButton.SetActive(false);
        shopButton.SetActive(false);
        talkPanel.SetActive(true);
        

    }
}
