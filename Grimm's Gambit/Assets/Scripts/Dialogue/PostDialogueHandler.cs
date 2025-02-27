using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PostDialogueHandler : BossDialogueHandler
{
    #region Serialized Fields

    [SerializeField] private List<BossDialogue> goodEnding, badEnding;
    [SerializeField] private GameObject choicePanel, goodButton, badButton;
    [SerializeField] private string endingScene;

    #endregion

    #region Public Methods

    public override void StartDialogue()
    {
        _revealSelectedCharacters();
        StartCoroutine(_typeLine());
    }

    public void StartGoodDialogue()
    {
        _startDialogue(goodEnding);
    }

    public void StartBadDialogue()
    {
        _startDialogue(badEnding);
    }

    public override void NextLine()
    {
        _nextLine(bossDialogue, _turnOnChoice);
    }

    public void NextLineGood()
    {
        _nextLine(goodEnding, _endPostDialogue);
    }

    public void NextLineBad()
    {
        _nextLine(badEnding, _endPostDialogue);
    }

    #endregion

    /*

    public void StartGoodDialogue()
    {
        _resetDialogue();
        selectedBossConversation = goodEnding[bossIndex]; 

        RevealSelectedCharacters();
        choicePanel.SetActive(false);
        goodButton.SetActive(true);
        StartCoroutine(TypeLine());
    }

    public void StartBadDialogue()
    {
        _resetDialogue();
        selectedBossConversation = badEnding[bossIndex]; 

        RevealSelectedCharacters();
        choicePanel.SetActive(false);
        badButton.SetActive(true);
        StartCoroutine(TypeLine());
    }
    */

    /*
    public void NextLine(){
        StopAllCoroutines();
        if(index < selectedBossConversation.lines.Count - 1){
            index++;
            conversationText.text = string.Empty;
            StartCoroutine(TypeLine());
        }else{
            if(bossIndex < bossDialogue.Count - 1){
                bossIndex++; 
                index = 0; 
                conversationText.text = string.Empty;
                selectedBossConversation = bossDialogue[bossIndex];
                RevealSelectedCharacters(); 
                StartCoroutine(TypeLine());

            }else{
                choicePanel.SetActive(true);
                //CloseDialogueWindow(); 
            }


        }
    }

    public void NextLineGood(){
        StopAllCoroutines();
        if(index < selectedBossConversation.lines.Count - 1){
            index++;
            conversationText.text = string.Empty;
            StartCoroutine(TypeLine());
        }else{
            if(bossIndex < goodEnding.Count - 1){
                bossIndex++; 
                index = 0; 
                conversationText.text = string.Empty;
                selectedBossConversation = goodEnding[bossIndex];
                RevealSelectedCharacters(); 
                StartCoroutine(TypeLine());

            }else{
                _endPostDialogue();


            }


        }
    }

    public void NextLineBad(){
        StopAllCoroutines();
        if(index < selectedBossConversation.lines.Count - 1){
            index++;
            conversationText.text = string.Empty;
            StartCoroutine(TypeLine());
        }else{
            if(bossIndex < badEnding.Count - 1){
                bossIndex++; 
                index = 0; 
                conversationText.text = string.Empty;
                selectedBossConversation = badEnding[bossIndex];
                RevealSelectedCharacters(); 
                StartCoroutine(TypeLine());

            }else{ 
                _endPostDialogue();
            }


        }
    }
    */

    #region Private Fields

    private void _resetDialogue()
    {
        advanceButton.SetActive(false);
        conversationText.text = string.Empty;
        bossIndex = 0;
        index = 0;
    }

    private void _startDialogue(List<BossDialogue> dialogueList)
    {
        _resetDialogue();
        selectedBossConversation = dialogueList[bossIndex];

        _revealSelectedCharacters();
        choicePanel.SetActive(false);
        goodButton.SetActive(true);
        StartCoroutine(_typeLine());
    }

    private void _turnOnChoice()
    {
        choicePanel.SetActive(true);
    }

    private void _endPostDialogue()
    {
        RenderSettings.fog = true;
        SceneManager.UnloadSceneAsync(endingScene);
        MapPlayer.sceneToToggle.SetActive(true);
        SaveDataJSON save = FindObjectOfType<SaveDataJSON>();
        save.LoadFromPlayerData();
    }

    #endregion
}
