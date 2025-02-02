using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SerpentRingChecker : MonoBehaviour
{
    public Minion houndMinion;
    private bool RingActive = false; //Determines whether the serpent rings' strength proc is activated
    HeirloomManager heirloomManager;
    // Start is called before the first frame update
    void Start()
    {
        EncounterController.onTurnChanged += TurnUpdate; //Connects turn update function to the turn transitions
        heirloomManager = FindObjectOfType<HeirloomManager>();
    }

    private void TurnUpdate(bool currentPlayer)
    {
        if (currentPlayer) //Player turn start - Block stacks are reset here
        {
            if (RingActive)
            {
                houndMinion.AddAffix(Affix.Strength, 1); //Adds 1 Strength at the start of each turn, while the ring is active
            }
        }
        else //Enemy turn (Player turn end)
        {
            if (houndMinion.currentAffixes.ContainsKey(Affix.Block))
            {
                if (heirloomManager.ContainsHeirloom(Heirloom.Serpent) && houndMinion.currentAffixes[Affix.Block] >= 12)
                {
                    RingActive = true; //The ring becomes activated for the duration of a combat once block is greater than 12 at the end of any player turn on the hound.
                }
            }
        }
    }
}
