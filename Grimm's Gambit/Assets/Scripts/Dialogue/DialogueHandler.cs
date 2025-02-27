using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DialogueHandler : BaseDialogueHandler
{


    [SerializeField]
    protected TMP_Text chosenCharacterText; //Text to display

[SerializeField]
    protected string chosenCharacter; // The character to generate dialogue for 


    [SerializeField]
    protected GameObject talkButton, shopButton, party, talkPanel, finishSelectionButton;//Various items to hide and show

    [SerializeField]
    private List<Dialogue> seamstressConversations, katzeConversations, houndConversations;//The lists of conversations for each character

    [SerializeField]
    private Dialogue seamstressDialogue1, seamstressDialogue2Good, seamstressDialogue2Bad, seamstressDialogue3Good, seamstressDialogue3Bad;


    [SerializeField]
    private Dialogue houndDialogue1, houndDialogue2Good, houndDialogue2Bad, houndDialogue3Good, houndDialogue3Bad;


    [SerializeField]
    private Dialogue katzeDialogue1, katzeDialogue2Good, katzeDialogue2Bad, katzeDialogue3Good, katzeDialogue3Bad;

    [SerializeField]
    protected Dialogue selectedConversation; //A conversation selected at random

    private bool canTalkSeamstress = true, canTalkHound = true, canTalkKatze = true; 

    protected override void Start(){
    conversationText.text = string.Empty; 
    finishSelectionButton.SetActive(false);
}

protected override void Update()
    {
        chosenCharacterText.text = chosenCharacter; 
    }

public override void StartDialogue(){
        Debug.Log(chosenCharacter);
        finishSelectionButton.SetActive(false);
        talkPanel.SetActive(false);
        advanceButton.SetActive(true);
        party.SetActive(false);
        dialogueWindow.SetActive(true); 
        RevealSelectedCharacters();
        StartCoroutine(TypeLine());
        
        
    }

    protected override void SetSpeaker(int index){
        if(selectedConversation.isHeirSpeaking[index] == true){
            speakerText.text = "The Heir";
        }else if (selectedConversation.isNarratorText[index] == true){
            speakerText.text = " "; 
        }else{
            speakerText.text = chosenCharacter; 
        }

    }


     protected override void RevealSelectedCharacters(){
        switch(chosenCharacter){
            case "The Seamstress":
            seamstress.SetActive(true);
            canTalkSeamstress = false;

            /*SAVE SYSTEM LOGIC HERE(SWITCH STATEMENT?)
            switch (boolean for tracking player choices)
            case 1:

            case 2:
            if(goodChoice) - add the sweater to heirlooms here as well

            else

            case 3:

            if(goodChoice2)

            else



            */
            selectedConversation = seamstressDialogue1;
            break;

            case "The Hound":
            hound.SetActive(true);
            canTalkHound = false;

            /*SAVE SYSTEM LOGIC HERE(SWITCH STATEMENT?)
            switch (boolean for tracking player choices)
            case 1:

            case 2:
            if(goodChoice)

            else

            case 3:

            if(goodChoice2)

            else



            */
            selectedConversation = houndDialogue1;
            break;

            case "Katze":
            katze.SetActive(true);
            canTalkKatze = false; 

            /*SAVE SYSTEM LOGIC HERE(SWITCH STATEMENT?)
            switch (boolean for tracking player choices)
            case 1:

            case 2:
            if(goodChoice)

            else

            case 3:

            if(goodChoice2)

            else



            */
            selectedConversation = katzeDialogue1;
            break;

            default: 
            break;
        }
        heir.SetActive(true);

    }

    protected override IEnumerator TypeLine(){
        SetSpeaker(index); 
        foreach(char letter in selectedConversation.lines[index].ToCharArray()){
            conversationText.text += letter;
            yield return new WaitForSeconds(textSpeed);
            switch(speakerText.text){
                case "The Heir":
                ShowSpeaker(heir);
                HideListener(seamstress);
                HideListener(katze);
                HideListener(hound);

                PlayGarble();
                break;

                case "The Seamstress":
                ShowSpeaker(seamstress);
                HideListener(heir);
                PlayGarble();
                break;

                case "The Hound":
                ShowSpeaker(hound);
                HideListener(heir);
                PlayGarble();
                break;

                case "Katze":
                ShowSpeaker(katze);
                HideListener(heir);
                PlayGarble();
                break;

                default:
                HideListener(heir);
                HideListener(seamstress);
                HideListener(katze);
                HideListener(hound);
                PlayNarratorGarble();
                break;
            }
        }

    }

    public override void NextLine(){
        StopAllCoroutines();
        if(index < selectedConversation.lines.Count - 1){
            index++;
            conversationText.text = string.Empty;
            StartCoroutine(TypeLine());
        }else{
            CloseDialogueWindow(); 

        }
    }

     protected override void CloseDialogueWindow(){
        //Hide all Sprites
        heir.SetActive(false);
        hound.SetActive(false);
        seamstress.SetActive(false);
        katze.SetActive(false);

        //Hide and show certain buttons
        advanceButton.SetActive(false);
        party.SetActive(true);
        dialogueWindow.SetActive(false); 
        shopButton.SetActive(true);
        //Determine if player can converse again 
        if(canTalkHound || canTalkSeamstress || canTalkKatze){
            talkButton.SetActive(true);
            index = 0;
            conversationText.text = string.Empty;
            chosenCharacter = "";
            chosenCharacterText.text = chosenCharacter;


        }
            

    }
public virtual void SetCharacter(string characterName){
        if(characterName == "The Seamstress" && canTalkSeamstress){
            chosenCharacter = characterName; 
            if(finishSelectionButton.activeSelf == false)
                finishSelectionButton.SetActive(true);
        }
        else if(characterName == "The Hound" && canTalkHound){
            chosenCharacter = characterName; 
            if(finishSelectionButton.activeSelf == false)
                finishSelectionButton.SetActive(true);
        }else if (characterName == "Katze" && canTalkKatze){
            chosenCharacter = characterName; 
            if(finishSelectionButton.activeSelf == false)
                finishSelectionButton.SetActive(true);
        }
        

    }


//Sets player choice active 
    public void ChooseTalk(){
        talkButton.SetActive(false);
        shopButton.SetActive(false);

        talkPanel.SetActive(true);
        

    }
//Sets up dialogue panel 
    
    
}
