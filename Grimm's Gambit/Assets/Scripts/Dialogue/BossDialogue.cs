using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue Encounter", menuName = "DialogueEncounters/Boss Dialogue", order = 1)]
public class BossDialogue : ScriptableObject
{
    #region Serialized Fields

    [SerializeField] private string speakerName;
    [SerializeField] private BossDialogueLine[] _dialogueLines;

    #endregion

    #region Properties

    public string SpeakerName { get => speakerName; }
    public BossDialogueLine[] DialogueLines { get => _dialogueLines; }

    #endregion
}

[System.Serializable]
public struct BossDialogueLine
{
    #region Serialized Fields

    [SerializeField] private BossDialogueSpeaker _speaker;
    [SerializeField] private bool _speakerHidden;
    [TextArea(3, 20)] [SerializeField] private string _line;

    #endregion

    #region Properties

    public BossDialogueSpeaker Speaker { get => _speaker; }
    public bool SpeakerHidden { get => _speakerHidden; }
    public string Line { get => _line; }

    #endregion
}

public enum BossDialogueSpeaker { DEFAULT_SPEAKER, BOSS, NARRATOR }
