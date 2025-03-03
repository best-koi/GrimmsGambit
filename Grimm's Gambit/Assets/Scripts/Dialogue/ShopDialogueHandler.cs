using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopDialogueHandler : BossDialogueHandler
{

   protected override void Start()
    {
        advanceButton.onClick.AddListener(NextLine);
        conversationText.text = string.Empty; 
        selectedBossConversation = bossDialogue[bossIndex];
    }

//StartShopDialogue() is intended to display the shop dialogue for players
//It is tied to a button click
//This will likely just be information from Beatrice to the player 
    public void StartShopDialogue(){
        index = 0;
        bossIndex = 0; 
        conversationText.text = string.Empty; 
        dialogueWindow.SetActive(true);
        advanceButton.gameObject.SetActive(true);
        StartDialogue();
        

    }
}
