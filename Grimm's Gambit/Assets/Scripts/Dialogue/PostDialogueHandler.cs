using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PostDialogueHandler : BossDialogueHandler
{
    #region Serialized Fields

    [Header("Post Dialogue Variables")]

    [Header("Ending Dialogue Lists")]
    [SerializeField] private List<BossDialogue> goodEnding;
    [SerializeField] private List<BossDialogue> badEnding;

    [Header("UI References")]
    [SerializeField] private GameObject choicePanel;
    [SerializeField] private Button _goodChoiceButton;
    [SerializeField] private Button _badChoiceButton;

    [Space] [SerializeField] private string endingScene;

    #endregion

    #region Private Fields

    private List<BossDialogue> _currentDialogue;
    private DialogueCloser _currentCloser;

    #endregion

    #region Monobehavior Callbacks

    protected override void Start()
    {
        base.Start();

        _currentDialogue = bossDialogue;
        _currentCloser = _turnOnChoice;

        _goodChoiceButton.onClick.AddListener(StartGoodDialogue);
        _badChoiceButton.onClick.AddListener(StartBadDialogue);
    }

    #endregion

    #region Public Methods

    public override void StartDialogue()
    {
        _revealSelectedCharacters();
        StartCoroutine(_typeLine());
    }

    public void StartGoodDialogue()
    {
        _startDialogue(goodEnding, _endPostDialogue);
    }

    public void StartBadDialogue()
    {
        _startDialogue(badEnding, _endPostDialogue);
    }

    public override void NextLine()
    {
        _nextLine(_currentDialogue, _currentCloser);
    }

    #endregion

    #region Private Fields

    private void _resetDialogue()
    {
        advanceButton.gameObject.SetActive(false);
        conversationText.text = string.Empty;
        bossIndex = 0;
        index = 0;
    }

    private void _startDialogue(List<BossDialogue> dialogueList, DialogueCloser newCloser = null)
    {
        _resetDialogue();
        selectedBossConversation = dialogueList[bossIndex];

        choicePanel.SetActive(false);

        _currentDialogue = dialogueList;
        if (newCloser != null) _currentCloser = newCloser;
        advanceButton.gameObject.SetActive(true);

        StartDialogue();
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
