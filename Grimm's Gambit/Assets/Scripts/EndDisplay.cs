using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndDisplay : MonoBehaviour
{
    [SerializeField] private string winText = "You defeated the enemy", loseText = "You were slain...";
    [SerializeField] private TMP_Text endScreenText;

    public void SetDisplay(bool win)
    {

    }
}
