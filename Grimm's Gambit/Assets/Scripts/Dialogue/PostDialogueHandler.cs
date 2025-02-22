using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PostDialogueHandler : BossDialogueHandler
{
    [SerializeField]
    private List<BossDialogue> goodEnding, badEnding;

    [SerializeField]
    private GameObject choicePanel, goodButton, badButton;

    [SerializeField]
    private string endingScene; 

public override void StartDialogue(){
        RevealSelectedCharacters();
        StartCoroutine(TypeLine());
    }

private void ResetDialogue(){
    advanceButton.SetActive(false);
        conversationText.text = string.Empty;
        bossIndex = 0;
        index = 0;
}

public void StartGoodDialogue(){
        ResetDialogue();
        selectedBossConversation = goodEnding[bossIndex]; 
        

        RevealSelectedCharacters();
        choicePanel.SetActive(false);
        goodButton.SetActive(true);
        StartCoroutine(TypeLine());
    }


public void StartBadDialogue(){
        ResetDialogue();
        selectedBossConversation = badEnding[bossIndex]; 

        RevealSelectedCharacters();
        choicePanel.SetActive(false);
        badButton.SetActive(true);
        StartCoroutine(TypeLine());
    }

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
                //CloseDialogueWindow(); 
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
                //CloseDialogueWindow(); 
            }
            

        }
    }


}
