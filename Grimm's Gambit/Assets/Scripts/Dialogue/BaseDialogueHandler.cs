using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public abstract class BaseDialogueHandler : MonoBehaviour
{
    #region Serialized Fields

    [SerializeField] protected GameObject seamstress, hound, katze, heir; //Sprites for each character, to be toggled on and off 
    [SerializeField] protected TMP_Text speakerText, conversationText; //Text to display
    [SerializeField] protected GameObject dialogueWindow, advanceButton;
    [SerializeField] protected float textSpeed; //The speed to advance dialogue
    [SerializeField] protected AudioClip SoundEffect, seamstressSFX, houndSFX, katzeSFX, heirSFX, narratorSFX;

    #endregion

    #region Private Fields

    protected int index = 0; //An index to track conversation progress

    #endregion

    /*
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
    */

    #region Public Methods

    public void PlayGarble()
    {
        _playGarble(SoundEffect);
    }

    public void PlayHeirGarble()
    {
        _playGarble(heirSFX);
    }

    public void PlaySeamstressGarble()
    {
        _playGarble(seamstressSFX);
    }

    public void PlayHoundGarble()
    {
        _playGarble(houndSFX);
    }

    public void PlayKatzeGarble()
    {
        _playGarble(katzeSFX);
    }

    public void PlayNarratorGarble()
    {
        _playGarble(narratorSFX);
    }

    public abstract void StartDialogue();
    public abstract void NextLine();

    #endregion

    #region Private Fields

    protected void _playGarble(AudioClip sfx)
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        int[] Semitones = new[] { 0, 2, 4, 7, 9 };
        int random = Random.Range(0, 5);
        audioSource.pitch = 0.75f;

        for (int i = 0; i < Semitones[random]; i++)
            audioSource.pitch *= 1.059463f;

        audioSource.PlayOneShot(sfx);
    }

    protected void _hideListener(GameObject character)
    {
        character.GetComponent<Image>().color = Color.grey;
    }

    protected void _showSpeaker(GameObject character)
    {
        character.GetComponent<Image>().color = Color.white;
    }

    //Shows the character and picks a dialogue encounter 
    protected abstract void _revealSelectedCharacters();
    protected abstract void _setSpeaker(int index);
    protected abstract IEnumerator _typeLine();
    protected abstract void _closeDialogueWindow();

    #endregion
}
