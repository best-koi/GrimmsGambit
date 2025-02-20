using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue Encounter", menuName = "DialogueEncounters/Boss Dialogue", order = 1)]
public class BossDialogue : ScriptableObject
{
    [SerializeField]
    public List<string> lines;//A list of lines representing a conversation

    [SerializeField]
    public List<bool> isBossSpeaking; 

    [SerializeField]
    public string speakerName; 
}
