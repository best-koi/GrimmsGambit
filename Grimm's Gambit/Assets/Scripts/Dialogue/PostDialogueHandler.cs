using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostDialogueHandler : BossDialogueHandler
{
    [SerializeField]
    private BossDialogue goodEnding, badEnding;


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
                CloseDialogueWindow(); 
            }
            

        }
    }

    public void FinalNextLine(){
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
                CloseDialogueWindow(); 
            }
            

        }
    }


}
