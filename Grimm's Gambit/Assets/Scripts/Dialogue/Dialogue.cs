using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue Encounter", menuName = "DialogueEncounters/Basic Dialogue", order = 0)]

public class Dialogue : ScriptableObject
{
    [SerializeField]
    public List<string> lines;//A list of lines representing a conversation

    [SerializeField]
    public List<bool> isHeirSpeaking;//A list of bools representing whether the heir is speaking or not 

    
}
