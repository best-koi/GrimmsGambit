using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public abstract class BaseDialogueHandler : MonoBehaviour
{
    #region Serialized Fields

    [Header("Base Dialogue Variables")]

    [Header("Character Sprite References")] //Sprites for each character, to be toggled on and off
    [SerializeField] protected GameObject seamstress;
    [SerializeField] protected GameObject hound;
    [SerializeField] protected GameObject katze;
    [SerializeField] protected GameObject heir;

    [Header("Text UI References")] // Text to display
    [SerializeField] protected TMP_Text speakerText;
    [SerializeField] protected TMP_Text conversationText;

    [Header("Object UI References")]
    [SerializeField] protected GameObject dialogueWindow;
    [SerializeField] protected Button advanceButton, skipDialogueButton;

    [Header("Object UI References")]
    [SerializeField] protected AudioClip SoundEffect;
    [SerializeField] protected AudioClip seamstressSFX;
    [SerializeField] protected AudioClip houndSFX;
    [SerializeField] protected AudioClip katzeSFX;
    [SerializeField] protected AudioClip heirSFX;
    [SerializeField] protected AudioClip narratorSFX;

    [Space] [SerializeField] protected float textSpeed; //The speed to advance dialogue

    protected bool canDisplayFullLine = true;

    #endregion

    #region Private Fields

    protected int index = 0; //An index to track conversation progress

    #endregion

    #region Monobehavior Callbacks

    protected virtual void Start()
    {
        advanceButton.onClick.AddListener(NextLine);
        skipDialogueButton.onClick.AddListener(SkipDialogue);
    }

    #endregion

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

    public virtual void SkipDialogue(){
        StopAllCoroutines();
        _closeDialogueWindow();
    }
}
