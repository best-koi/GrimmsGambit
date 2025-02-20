using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DialogueHandler : MonoBehaviour
{
    [SerializeField]
    protected GameObject seamstress, hound, katze, heir; //Sprites for each character, to be toggled on and off 

    [SerializeField]
    protected TMP_Text speakerText, conversationText, chosenCharacterText; //Text to display

[SerializeField]
    protected string chosenCharacter; // The character to generate dialogue for 

    [SerializeField]
    protected GameObject talkButton, shopButton, party, talkPanel, dialogueWindow, advanceButton;//Various items to hide and show

    [SerializeField]
    private List<Dialogue> seamstressConversations, katzeConversations, houndConversations;//The lists of conversations for each character

    [SerializeField]
    protected Dialogue selectedConversation; //A conversation selected at random

    [SerializeField]
    protected float textSpeed; //The speed to advance dialogue
    [SerializeField]
    protected AudioClip SoundEffect;

    protected int index = 0; //An index to track conversation progress

    

protected virtual void Start(){
    conversationText.text = string.Empty; 
}



    // Update is called once per frame
    protected virtual void Update()
    {
        chosenCharacterText.text = chosenCharacter; 
    }

//Sets the character upon clicking 
    public void SetCharacter(string characterName){
        chosenCharacter = characterName; 

    }
//Sets player choice active 
    public void ChooseTalk(){
        talkButton.SetActive(false);
        shopButton.SetActive(false);
        talkPanel.SetActive(true);
        

    }
//Sets up dialogue panel 
    public virtual void StartDialogue(){
        Debug.Log(chosenCharacter);
        talkPanel.SetActive(false);
        party.SetActive(false);
        dialogueWindow.SetActive(true); 
        RevealSelectedCharacters();
        StartCoroutine(TypeLine());
        
        
    }

//Shows the character and picks a dialogue encounter 
    protected virtual void RevealSelectedCharacters(){
        switch(chosenCharacter){
            case "The Seamstress":
            seamstress.SetActive(true);
            selectedConversation = seamstressConversations[Random.Range(0, seamstressConversations.Count)];

            break;

            case "The Hound":
            hound.SetActive(true);
            selectedConversation = houndConversations[Random.Range(0, houndConversations.Count)];
            break;

            case "Die Katze":
            katze.SetActive(true);
            selectedConversation = katzeConversations[Random.Range(0, katzeConversations.Count)];
            break;

            default: 
            break;
        }
        heir.SetActive(true);

    }

    protected virtual void SetSpeaker(int index){
        if(selectedConversation.isHeirSpeaking[index] == true){
            speakerText.text = "The Heir";
        }else if (selectedConversation.isNarratorText[index] == true){
            speakerText.text = " "; 
        }else{
            speakerText.text = chosenCharacter; 
        }

    }

    protected virtual IEnumerator TypeLine(){
        SetSpeaker(index); 
        foreach(char letter in selectedConversation.lines[index].ToCharArray()){
            conversationText.text += letter;
            yield return new WaitForSeconds(textSpeed);
            PlayGarble();
        }

    }

    public virtual void NextLine(){
        StopAllCoroutines();
        if(index < selectedConversation.lines.Count - 1){
            index++;
            conversationText.text = string.Empty;
            StartCoroutine(TypeLine());
        }else{
            CloseDialogueWindow(); 

        }
    }

    protected virtual void CloseDialogueWindow(){
        advanceButton.SetActive(false);
        party.SetActive(true);
        dialogueWindow.SetActive(false); 
        shopButton.SetActive(true);

    }

    public void PlayGarble()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        int[] Semitones = new[] {0, 2, 4, 7, 9};
        int random = Random.Range(0, 5);
        audioSource.pitch = 0.75f;
        for (int i = 0; i < Semitones[random]; i++)
        {
            audioSource.pitch *= 1.059463f;
        }
        audioSource.PlayOneShot(SoundEffect);
    }
    
}
