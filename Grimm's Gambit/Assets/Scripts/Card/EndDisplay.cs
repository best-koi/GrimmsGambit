using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EndDisplay : MonoBehaviour
{
    [SerializeField] private GameObject endCamera;

    [SerializeField] private string winText = "You defeated the enemy", loseText = "You were slain...";
    [SerializeField] private TMP_Text endScreenText;

    [SerializeField] private EncounterData encounterData;

    public void SetDisplay(bool win)
    {
        Debug.Log("Called");

        if (win) endScreenText.text = winText;
        else endScreenText.text = loseText;
    }

    private void Start()
    {
        SetDisplay(encounterData.GetWin());
    }

    private void OnEnable()
    {
        endCamera.SetActive(true);
    }

    private void OnDisable()
    {
        endCamera.SetActive(false);
    }
}
