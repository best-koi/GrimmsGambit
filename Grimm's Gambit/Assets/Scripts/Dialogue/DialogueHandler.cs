using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueHandler : BaseDialogueHandler
{
    [Header("Campfire Dialogue Variables")]

    [Header("Chosen Character Variables")]
    [SerializeField] protected TMP_Text chosenCharacterText; // Text to display
    [SerializeField] protected string chosenCharacter; // The character to generate dialogue for 

    [Header("UI References")] // Various items to hide and show
    [SerializeField] protected GameObject talkButton;
    [SerializeField] protected GameObject shopButton;
    [SerializeField] protected GameObject party;
    [SerializeField] protected GameObject talkPanel;
    [SerializeField] protected GameObject finishSelectionButton;

    [Header("Dialogue Lists")] // The lists of conversations for each character
    [SerializeField] private List<Dialogue> seamstressConversations;
    [SerializeField] private List<Dialogue> katzeConversations;
    [SerializeField] private List<Dialogue> houndConversations;

    [Header("Choice Dialogue")]
    [SerializeField] private Dialogue seamstressDialogue1;
    [SerializeField] private Dialogue seamstressDialogue2Good;
    [SerializeField] private Dialogue seamstressDialogue2Bad;
    [SerializeField] private Dialogue seamstressDialogue3Good;
    [SerializeField] private Dialogue seamstressDialogue3Bad;

    [SerializeField] private Dialogue houndDialogue1;
    [SerializeField] private Dialogue houndDialogue2Good;
    [SerializeField] private Dialogue houndDialogue2Bad;
    [SerializeField] private Dialogue houndDialogue3Good;
    [SerializeField] private Dialogue houndDialogue3Bad;

    [SerializeField] private Dialogue katzeDialogue1;
    [SerializeField] private Dialogue katzeDialogue2Good;
    [SerializeField] private Dialogue katzeDialogue2Bad;
    [SerializeField] private Dialogue katzeDialogue3Good;
    [SerializeField] private Dialogue katzeDialogue3Bad;

    [Header("Randomly Selected Dialogue")]
    [SerializeField] protected Dialogue selectedConversation; // A conversation selected at random

    private bool canTalkSeamstress = true, canTalkHound = true, canTalkKatze = true; 

    protected override void Start()
    {
        base.Start();
        conversationText.text = string.Empty; 
        finishSelectionButton.SetActive(false);
    }

    private void Update()
    {
        chosenCharacterText.text = chosenCharacter; 
    }

    public override void StartDialogue()
    {
        Debug.Log(chosenCharacter);
        finishSelectionButton.SetActive(false);
        talkPanel.SetActive(false);
        advanceButton.gameObject.SetActive(true);
        party.SetActive(false);
        dialogueWindow.SetActive(true); 
        _revealSelectedCharacters();
        StartCoroutine(_typeLine());
    }

    protected override void _setSpeaker(int index)
    {
        switch (selectedConversation.DialogueLines[index].Speaker)
        {
            case DialogueSpeaker.HEIR:
                speakerText.text = "The Heir";
                break;

            case DialogueSpeaker.NARRATOR:
                speakerText.text = " ";
                break;

            default:
                speakerText.text = chosenCharacter;
                break;
        }
    }


    protected override void _revealSelectedCharacters()
    {
        switch(chosenCharacter)
        {
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

    protected override IEnumerator _typeLine()
    {
        _setSpeaker(index); 

        foreach(char letter in selectedConversation.DialogueLines[index].Line.ToCharArray())
        {
            conversationText.text += letter;
            yield return new WaitForSeconds(textSpeed);

            switch(speakerText.text)
            {
                case "The Heir":
                    _showSpeaker(heir);
                    _hideListener(seamstress);
                    _hideListener(katze);
                    _hideListener(hound);

                    PlayGarble();
                    break;

                case "The Seamstress":
                    _showSpeaker(seamstress);
                    _hideListener(heir);
                    PlayGarble();
                    break;

                case "The Hound":
                    _showSpeaker(hound);
                    _hideListener(heir);
                    PlayGarble();
                    break;

                case "Katze":
                    _showSpeaker(katze);
                    _hideListener(heir);
                    PlayGarble();
                    break;

                default:
                    _hideListener(heir);
                    _hideListener(seamstress);
                    _hideListener(katze);
                    _hideListener(hound);
                    PlayNarratorGarble();
                    break;
            }
        }

    }

    public override void NextLine()
    {
        StopAllCoroutines();

        if(index < selectedConversation.DialogueLines.Length - 1)
        {
            index++;
            conversationText.text = string.Empty;
            StartCoroutine(_typeLine());
        }
        else
        {
            _closeDialogueWindow(); 
        }
    }

    protected override void _closeDialogueWindow()
    {
        //Hide all Sprites
        heir.SetActive(false);
        hound.SetActive(false);
        seamstress.SetActive(false);
        katze.SetActive(false);

        //Hide and show certain buttons
        advanceButton.gameObject.SetActive(false);
        party.SetActive(true);
        dialogueWindow.SetActive(false); 
        shopButton.SetActive(true);
        //Determine if player can converse again 
        if(canTalkHound || canTalkSeamstress || canTalkKatze)
        {
            talkButton.SetActive(true);
            index = 0;
            conversationText.text = string.Empty;
            chosenCharacter = "";
            chosenCharacterText.text = chosenCharacter;
        }
    }

    public virtual void SetCharacter(string characterName)
    {
        if(characterName == "The Seamstress" && canTalkSeamstress || characterName == "The Hound" && canTalkHound || characterName == "Katze" && canTalkKatze)
        {
            chosenCharacter = characterName; 
            if(finishSelectionButton.activeSelf == false)
                finishSelectionButton.SetActive(true);
        }
    }


    //Sets player choice active 
    public void ChooseTalk()
    {
        talkButton.SetActive(false);
        shopButton.SetActive(false);
        talkPanel.SetActive(true);
    }
    //Sets up dialogue panel 
}
