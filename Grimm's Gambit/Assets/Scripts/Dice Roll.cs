using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class DiceRoll : MonoBehaviour
{
    /// Dice Roll MonoBehaviour
    /// 
    /// Represents a dice roll
    /// 
    /// </summary>

    // Maximum roll number
    [SerializeField]
    private int m_MaxRoll;

    public int MaxRoll
    {
        get
        {
            return m_MaxRoll;
        }
        set
        {
            m_MaxRoll = value;
        }
    }

    // Minimum roll number
    [SerializeField]
    private int m_MinRoll;

    public int MinRoll
    {
        get
        {
            return m_MinRoll;
        }
        set
        {
            m_MinRoll = value;
        }
    }

    // Display text for roll result
    [SerializeField]
    private TMP_Text m_ResultText;

    // Checks if dice has rolled
    private bool m_HasRolled;
    public bool HasRolled
    {
        get
        {
            return m_HasRolled;
        }
    }

    [Header("Dice Animation Properties")]

    // List of pre-roll display texts for dice
    [SerializeField]
    private TMP_Text[] m_RollTexts;

    // Dice model object reference
    [SerializeField]
    private GameObject m_Dice;

    // Timer for pre-roll dice animation
    [SerializeField]
    private Timer m_TimeBetweenRandomNum;

    private void Awake()
    {
        m_HasRolled = false;
    }

    private void Update()
    {
        if (!m_HasRolled)
            m_TimeBetweenRandomNum.UpdateTimer(true);
        else if (!m_TimeBetweenRandomNum.TimeRanOut())
            m_TimeBetweenRandomNum.ResetTimer(false);

        UpdateDiceVisuals();
        DisplayRolling();
    }

    private void DisplayRolling()
    {
        if (m_TimeBetweenRandomNum.TimeRanOut())
        {
            string diceDisplay = ReturnRoll() + "";

            foreach (TMP_Text text in m_RollTexts)
            {
                if (text != null)
                    text.text = diceDisplay;
            }
        }
    }

    private void UpdateDiceVisuals()
    {
        m_Dice.SetActive(!m_HasRolled);
        m_ResultText.gameObject.SetActive(m_HasRolled);
    }

    public void ResetDice()
    {
        m_HasRolled = false;
    }

    public int ReturnRoll()
    {
        return (int)Random.Range(m_MinRoll, m_MaxRoll + 1);
    }

    public int RollDice()
    {
        int roll = ReturnRoll();
        m_HasRolled = true;
        m_ResultText.text = roll + "";

        return roll;
    }

}

#if UNITY_EDITOR
[CustomEditor(typeof(DiceRoll))]
[CanEditMultipleObjects]
public class DiceRollEditor : Editor
{
    // Displays the button that generates a string of the list of random drops
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        var tool = target as DiceRoll;
        serializedObject.Update();

        if (GUILayout.Button("Roll the Dice") && !tool.HasRolled)
        {
            Debug.Log(tool.RollDice());
        }

        if (GUILayout.Button("Reset the Dice"))
        {
            tool.ResetDice();
        }
    }
}
#endif
