using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public abstract class BaseDialogueHandler : MonoBehaviour
{

    [SerializeField]
    protected GameObject seamstress, hound, katze, heir; //Sprites for each character, to be toggled on and off 


    [SerializeField]
    protected TMP_Text speakerText, conversationText; //Text to display

    [SerializeField]
    protected GameObject dialogueWindow, advanceButton;

    [SerializeField]
    protected float textSpeed; //The speed to advance dialogue

    [SerializeField]
    protected AudioClip SoundEffect, seamstressSFX, houndSFX, katzeSFX, heirSFX, narratorSFX;

    protected int index = 0; //An index to track conversation progress

    protected abstract void Start();



    // Update is called once per frame
    protected abstract void Update();
   
//Sets the character upon clicking 
    

    public abstract void StartDialogue();
    

//Shows the character and picks a dialogue encounter 
    protected abstract void RevealSelectedCharacters();

    protected abstract void SetSpeaker(int index);

    protected abstract IEnumerator TypeLine();

    public abstract void NextLine();

    protected abstract void CloseDialogueWindow();

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
