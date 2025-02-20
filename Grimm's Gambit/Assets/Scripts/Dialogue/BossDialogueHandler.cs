using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDialogueHandler : DialogueHandler
{

    [SerializeField]
    protected GameObject boss; 

    [SerializeField]
    private List<BossDialogue> bossDialogue;

    [SerializeField]
    private string bossName; 

    private int bossIndex = 0;

    [SerializeField]
    protected BossDialogue selectedBossConversation;

    protected override void Start(){
    conversationText.text = string.Empty; 
    selectedBossConversation = bossDialogue[bossIndex];
    StartDialogue(); 
    }

public override void StartDialogue(){
        RevealSelectedCharacters();
        StartCoroutine(TypeLine());
    }

     protected override void Update()
    {
        
    }

    protected override void SetSpeaker(int index){
        if(selectedBossConversation.isBossSpeaking[index] == true){
            speakerText.text = bossName;
        }else{
            speakerText.text = selectedBossConversation.speakerName; 
        }

    }

    protected virtual IEnumerator TypeLine(){
        SetSpeaker(index); 
        foreach(char letter in selectedBossConversation.lines[index].ToCharArray()){
            conversationText.text += letter;
            yield return new WaitForSeconds(textSpeed);
            PlayGarble();
        }

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
                CloseDialogueWindow(); 
            }
            

        }
    }

    protected override void RevealSelectedCharacters(){
        switch(selectedBossConversation.speakerName){
            case "The Seamstress":
            seamstress.SetActive(true);
            heir.SetActive(false);
            katze.SetActive(false);
            hound.SetActive(false);

            break;

            case "The Hound":
            hound.SetActive(true);
            heir.SetActive(false);
            katze.SetActive(false);
            seamstress.SetActive(false);
            break;

            case "Die Katze":
            katze.SetActive(true);
            heir.SetActive(false);
            hound.SetActive(false);
            seamstress.SetActive(false);
            break;

            case "The Heir":
            heir.SetActive(true);
            hound.SetActive(false);
            seamstress.SetActive(false);
            katze.SetActive(false);
            break;

            default: 
            break;
        }
        boss.SetActive(true);

    }

    protected override void CloseDialogueWindow(){
        advanceButton.SetActive(false);
        dialogueWindow.SetActive(false); 

    }


}
