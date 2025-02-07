using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class EndDisplay : MonoBehaviour
{
    [SerializeField] private string winText = "You defeated the enemy", loseText = "You were slain...";
    [SerializeField] private TMP_Text endScreenText;

    [SerializeField] private EncounterData encounterData;

    [SerializeField] public Button exitButton;

    public void SetDisplay(bool win)
    {
        Debug.Log("Called");

        if (win) endScreenText.text = winText;
        else endScreenText.text = loseText;
    }

    private void Start()
    {
        //if (exitButton == null) exitButton = FindObjectOfType<Button>();

        SetDisplay(encounterData.GetWin());

        exitButton.clicked += Exit;
    }

    private void Exit()
    {
        Application.Quit(); //Placeholder, can be removed to load in desired scene
    }
}
