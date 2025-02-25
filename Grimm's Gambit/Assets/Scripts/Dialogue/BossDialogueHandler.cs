using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class BossDialogueHandler : DialogueHandler
{

    [SerializeField]
    protected GameObject boss; 

    [SerializeField]
    protected List<BossDialogue> bossDialogue;

    [SerializeField]
    protected string bossName; 

    protected int bossIndex = 0;

    [SerializeField]
    protected BossDialogue selectedBossConversation;

    [SerializeField]
    protected AudioClip bossSFX;

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
        if(selectedBossConversation.isBossSpeaking[index] == true && selectedBossConversation.isSpeakerHidden[index] == false){
            speakerText.text = bossName;
        }else if (selectedBossConversation.isNarratorText[index] == true){
            speakerText.text = " ";
        }else if (selectedBossConversation.isSpeakerHidden[index] == true){
            speakerText.text = "???";
        }else{
            speakerText.text = selectedBossConversation.speakerName; 

        }

    }

    protected override IEnumerator TypeLine(){
        SetSpeaker(index); 
        foreach(char letter in selectedBossConversation.lines[index].ToCharArray()){
            conversationText.text += letter;
            yield return new WaitForSeconds(textSpeed);
            switch(speakerText.text){
                case "The Heir":
                CheckBossVisibility();
                PlayGarble();
                
                break;

                case "The Seamstress":
                CheckBossVisibility();
                PlayGarble();
                break;

                case "The Hound":
                CheckBossVisibility();
                PlayGarble();
                break;

                case "Katze":
                CheckBossVisibility();
                PlayGarble();
                break;

                case " ":
                CheckBossVisibility();
                PlayNarratorGarble(); 
                break;

                default:
                CheckBossVisibility();
                PlayBossGarble();
                break;
            }
            
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

            case "Katze":
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
            PlayNarratorGarble();
            break;
        }
        boss.SetActive(true);

    }

    protected override void CloseDialogueWindow(){
        advanceButton.SetActive(false);
        dialogueWindow.SetActive(false); 

    }

    public void PlayBossGarble()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        int[] Semitones = new[] {0, 2, 4, 7, 9};
        int random = Random.Range(0, 5);
        audioSource.pitch = 0.75f;
        for (int i = 0; i < Semitones[random]; i++)
        {
            audioSource.pitch *= 1.059463f;
        }
        audioSource.PlayOneShot(bossSFX);
    }

    private void CheckBossVisibility(){
        if (selectedBossConversation.isSpeakerHidden[index] == true)
                    boss.GetComponent<Image>().color = Color.grey;
                else
                    boss.GetComponent<Image>().color = new Color(255,255,255, 255); 
    }


}
