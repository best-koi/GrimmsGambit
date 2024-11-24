//Ryan Lockie - Updated 11/16/2024
//The intention of this script is to control the Katze passive functionality of generating coordinated strikes every other turn
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KatzePassive : MonoBehaviour
{
    public Card coordinatedStrikes;

    private EncounterController encounterController;
    // Start is called before the first frame update
    void Start()
    {
        encounterController = FindObjectOfType<EncounterController>();
        EncounterController.onTurnChanged += TurnStart;
    }


    void TurnStart(bool currentTurn)
    {
        if (currentTurn && encounterController.m_TurnCounter%2 == 0) //Activates when it is the player's (katze's) turn and it's an even turn
        {
            Deck deck = FindObjectOfType<Deck>();
            deck.Conjure(coordinatedStrikes.GetData()); //Conjures coordinate strikes on even turns while katze is active
        }
    }
}
