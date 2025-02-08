using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndDisplay : MonoBehaviour
{
    [SerializeField] private string winText = "You defeated the enemy", loseText = "You were slain...";
    [SerializeField] private TMP_Text endScreenText;

    [SerializeField] private EncounterData encounterData;

    [SerializeField] private Button exitButton;

    public void SetDisplay(bool win)
    {
        if (win) endScreenText.text = winText;
        else endScreenText.text = loseText;
    }

    private void Start()
    {
        if (exitButton == null) exitButton = FindObjectOfType<Button>();

        //SetDisplay(encounterData.GetWin());
        SetDisplay(false);

        exitButton.onClick.AddListener(Exit);
    }

    public void Exit()
    {
        Debug.Log("Exit");
        SceneManager.LoadScene("StartMenu");
    }
}
