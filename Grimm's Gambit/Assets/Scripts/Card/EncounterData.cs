using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Encounters", order = 0)]
public class EncounterData : ScriptableObject
{
    [SerializeField] private bool _playerWon;

    private void Awake()
    {
        EncounterController.onEncounterEnded += SetWin;
    }

    private void SetWin(bool win) { _playerWon = win; }
    public bool GetWin() { return _playerWon; }
}
