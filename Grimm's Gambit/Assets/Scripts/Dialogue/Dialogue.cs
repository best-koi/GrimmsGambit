using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue Encounter", menuName = "DialogueEncounters/Basic Dialogue", order = 0)]

public class Dialogue : ScriptableObject
{
    #region Serialized Fields

    [SerializeField] private DialogueLine[] _dialogueLines;

    #endregion

    #region Properties

    public DialogueLine[] DialogueLines { get => _dialogueLines; }

    #endregion
}

[System.Serializable]
public struct DialogueLine
{
    #region Serialized Fields

    [SerializeField] private DialogueSpeaker _speaker;
    [TextArea(3, 20)] [SerializeField] private string _line;

    #endregion

    #region Properties

    public DialogueSpeaker Speaker { get => _speaker; }
    public string Line { get => _line; }

    #endregion
}

public enum DialogueSpeaker { DEFAULT_SPEAKER, HEIR, NARRATOR }