using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DialogueHandler : MonoBehaviour
{
    [Header("Character Sprites")]
    [SerializeField]
    protected GameObject seamstress, hound, katze, heir; //Sprites for each character, to be toggled on and off 

[Header("Character Text")]
    [SerializeField]
    protected TMP_Text speakerText, conversationText, chosenCharacterText; //Text to display

[SerializeField]
    protected string chosenCharacter; // The character to generate dialogue for 

[Header("UI Elements")]
    [SerializeField]
    protected GameObject talkButton, shopButton, party, talkPanel, dialogueWindow, advanceButton, finishSelectionButton;//Various items to hide and show

    [SerializeField]
    private List<Dialogue> seamstressConversations, katzeConversations, houndConversations;//The lists of conversations for each character

    [Header("Seamstress Campfire Dialogues")]
    [SerializeField]
    private Dialogue seamstressDialogue1, seamstressDialogue2Good, seamstressDialogue2Bad, seamstressDialogue3Good, seamstressDialogue3Bad;

    [Header("Hound Campfire Dialogues")]
    [SerializeField]
    private Dialogue houndDialogue1, houndDialogue2Good, houndDialogue2Bad, houndDialogue3Good, houndDialogue3Bad;

    [Header("Katze Campfire Dialogues")]
    [SerializeField]
    private Dialogue katzeDialogue1, katzeDialogue2Good, katzeDialogue2Bad, katzeDialogue3Good, katzeDialogue3Bad;


    [SerializeField]
    protected Dialogue selectedConversation; //A conversation selected at random

    [SerializeField]
    protected float textSpeed; //The speed to advance dialogue
    [SerializeField]
    protected AudioClip SoundEffect, seamstressSFX, houndSFX, katzeSFX, heirSFX, narratorSFX;

    protected int index = 0; //An index to track conversation progress

    private bool canTalkSeamstress = true, canTalkHound = true, canTalkKatze = true; 

    

protected virtual void Start(){
    conversationText.text = string.Empty; 
    finishSelectionButton.SetActive(false);
}



    // Update is called once per frame
    protected virtual void Update()
    {
        chosenCharacterText.text = chosenCharacter; 
    }

//Sets the character upon clicking 
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
    public virtual void StartDialogue(){
        Debug.Log(chosenCharacter);
        finishSelectionButton.SetActive(false);
        talkPanel.SetActive(false);
        advanceButton.SetActive(true);
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
                PlayNarratorGarble();
                break;
            }
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

    public void PlayHeirGarble()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        int[] Semitones = new[] {0, 2, 4, 7, 9};
        int random = Random.Range(0, 5);
        audioSource.pitch = 0.75f;
        for (int i = 0; i < Semitones[random]; i++)
        {
            audioSource.pitch *= 1.059463f;
        }
        audioSource.PlayOneShot(heirSFX);
    }

    public void PlaySeamstressGarble()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        int[] Semitones = new[] {0, 2, 4, 7, 9};
        int random = Random.Range(0, 5);
        audioSource.pitch = 0.75f;
        for (int i = 0; i < Semitones[random]; i++)
        {
            audioSource.pitch *= 1.059463f;
        }
        audioSource.PlayOneShot(seamstressSFX);
    }
    public void PlayHoundGarble()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        int[] Semitones = new[] {0, 2, 4, 7, 9};
        int random = Random.Range(0, 5);
        audioSource.pitch = 0.75f;
        for (int i = 0; i < Semitones[random]; i++)
        {
            audioSource.pitch *= 1.059463f;
        }
        audioSource.PlayOneShot(houndSFX);
    }

    public void PlayKatzeGarble()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        int[] Semitones = new[] {0, 2, 4, 7, 9};
        int random = Random.Range(0, 5);
        audioSource.pitch = 0.75f;
        for (int i = 0; i < Semitones[random]; i++)
        {
            audioSource.pitch *= 1.059463f;
        }
        audioSource.PlayOneShot(katzeSFX);
    }

    public void PlayNarratorGarble()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        int[] Semitones = new[] {0, 2, 4, 7, 9};
        int random = Random.Range(0, 5);
        audioSource.pitch = 0.75f;
        for (int i = 0; i < Semitones[random]; i++)
        {
            audioSource.pitch *= 1.059463f;
        }
        audioSource.PlayOneShot(narratorSFX);
    }

     protected void HideListener(GameObject character){
        character.GetComponent<Image>().color = Color.grey;
    }
    protected void ShowSpeaker(GameObject character){
        character.GetComponent<Image>().color = Color.white;
    }
    
}
