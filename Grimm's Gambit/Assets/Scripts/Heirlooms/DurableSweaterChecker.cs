using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DurableSweaterChecker : MonoBehaviour
{
    public Minion seamtressMinion;
    HeirloomManager heirloomManager;
    // Start is called before the first frame update
    void Start()
    {
        EncounterController.onTurnChanged += TurnUpdate; //Connects turn update function to the turn transitions
        heirloomManager = FindObjectOfType<HeirloomManager>();
    }

    private void TurnUpdate(bool currentPlayer)
    {
        if (!currentPlayer && heirloomManager.ContainsHeirloom(Heirloom.Sweater)) //Player turn end when sweater is equipped
        {
            seamtressMinion.AddAffix(Affix.Block, 4);
        }
    }
}
