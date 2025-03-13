using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDialogueHandler : BaseDialogueHandler
{
    #region Serialized Fields

    [Header("Boss Dialogue Variables")]

    [SerializeField] protected GameObject boss; 
    [SerializeField] protected List<BossDialogue> bossDialogue;
    [SerializeField] protected string bossName; 
    [SerializeField] protected BossDialogue selectedBossConversation;
    [SerializeField] protected AudioClip bossSFX;

    #endregion

    #region Private Fields

    protected int bossIndex = 0;
    protected delegate void DialogueCloser();

    #endregion

    #region Monobehavior Callbacks

    protected override void Start()
    {
        base.Start();
        conversationText.text = string.Empty; 
        selectedBossConversation = bossDialogue[bossIndex];
        StartDialogue(); 
    }

    #endregion

    #region Public Methods

    public override void StartDialogue()
    {
        _revealSelectedCharacters();
        StartCoroutine(_typeLine());
    }

    public override void NextLine()
    {
        _nextLine(bossDialogue, _closeDialogueWindow);
    }

    public void PlayBossGarble()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        int[] Semitones = new[] {0, 2, 4, 7, 9};
        int random = Random.Range(0, 5);
        audioSource.pitch = 0.75f;

        for (int i = 0; i < Semitones[random]; i++)
            audioSource.pitch *= 1.059463f;

        audioSource.PlayOneShot(bossSFX);
    }

    #endregion

    #region Private Fields

    protected override void _setSpeaker(int index)
    {
        BossDialogueLine line = selectedBossConversation.DialogueLines[index];

        if (line.Speaker == BossDialogueSpeaker.NARRATOR)
        {
            speakerText.text = " ";
        }
        else
        {
            if (line.SpeakerHidden)
            {
                speakerText.text = "???";
            }
            else
            {
                if (line.Speaker == BossDialogueSpeaker.BOSS)
                    speakerText.text = bossName;
                else
                    speakerText.text = selectedBossConversation.SpeakerName;
            }
        }
    }

    protected override IEnumerator _typeLine()
    {
        _setSpeaker(index); 

        foreach(char letter in selectedBossConversation.DialogueLines[index].Line.ToCharArray())
        {
            conversationText.text += letter;
            yield return new WaitForSeconds(textSpeed);

            switch(speakerText.text)
            {
                case "The Heir":
                _showSpeaker(heir);
                _hideListener(boss);
                PlayHeirGarble();
                
                break;

                case "The Seamstress":
                _showSpeaker(seamstress);
                _hideListener(boss);
                PlaySeamstressGarble();
                
                break;

                case "The Hound":
                _showSpeaker(hound);
                _hideListener(boss);
                PlayHoundGarble();
                break;

                case "Katze":
                _showSpeaker(katze);
                _hideListener(boss);
                PlayKatzeGarble();
                break;

                case " ":
                _hideListener(heir);
                _hideListener(seamstress);
                _hideListener(katze);
                _hideListener(hound);
                _hideListener(boss);
                PlayNarratorGarble(); 
                break;

                default:
                _hideListener(seamstress);
                _hideListener(heir);
                _hideListener(katze);
                _hideListener(hound);
                _showSpeaker(boss);
                PlayBossGarble();
                break;
            }
        }
    }

    protected void _nextLine(List<BossDialogue> dialogueList, DialogueCloser closer)
    {
        StopAllCoroutines();

        if(canDisplayFullLine){
            conversationText.text = selectedBossConversation.DialogueLines[index].Line;
            canDisplayFullLine = false;
        }
        else if (index < selectedBossConversation.DialogueLines.Length - 1)
        {
            index++;
            conversationText.text = string.Empty;
            canDisplayFullLine = true;
            StartCoroutine(_typeLine());
        }
        else
        {
            if (bossIndex < dialogueList.Count - 1)
            {
                bossIndex++;
                index = 0;
                conversationText.text = string.Empty;
                selectedBossConversation = dialogueList[bossIndex];
                _revealSelectedCharacters();
                canDisplayFullLine = true;
                StartCoroutine(_typeLine());
            }
            else
            {
                closer();
            }
        }
    }

    protected override void _revealSelectedCharacters()
    {
        switch(selectedBossConversation.SpeakerName)
        {
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

    protected override void _closeDialogueWindow()
    {
        advanceButton.gameObject.SetActive(false);
        dialogueWindow.SetActive(false); 
    }

    #endregion
}
