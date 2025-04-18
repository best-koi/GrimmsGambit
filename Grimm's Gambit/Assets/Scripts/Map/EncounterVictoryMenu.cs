using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EncounterVictoryMenu : MonoBehaviour
{
    public Deck currentDeck;
    public CardDatabase cardDatabase;
    public List<List<bool>> validPairs = new List<List<bool>>(); //Valid indices are set to true within this, invalid(in deck) are false, null are not cards at all
    public List<int> trueCounters = new List<int>(); //This list is simply used to count how many true values exist for each character list - this is important for choosing what range to use for random values without checking validPairs
    //The following are all references for ui display elements:
    [SerializeField]
    private TMP_Text zeroName, zeroDesc, oneName, oneDesc, twoName, twoDesc; //These are all stored separately so that it would be easy for me to setup my scene - This portion is entirely contingent on scene setup
    [SerializeField]
    private Image zeroImage, oneImage, twoImage;
    private List<CardTemplate> currentData = new List<CardTemplate>(); //Current data is used for confirming selection
    private PlayerData playerData;
    void Start()
    {
        //LOAD DECK FROM JSON FILE HERE
        playerData = FindObjectOfType<PlayerData>();
        currentDeck.m_GameDeck = playerData.GetPlayerDeck();

        InitializeBooleanLists(); //Sorts valid cards for display
        ChooseCards(); //Displays cards
    }

    private void InitializeBooleanLists() //Determines which card indices can be used for the display
    {
        //Initializes a list for each card category
        for (int i = 0; i < cardDatabase._cardPrefabs.BaseArray.Length; i++) //Iterates through each card owner
        {
            validPairs.Add(new List<bool>());
            trueCounters.Add(0); //Adds a zero value to initialize the true count
            //Sets all valid used database indices to true
            for (int j = 0; j < cardDatabase._cardPrefabs.BaseArray[i].Row.Length; j++)
            {
                string cardName = cardDatabase.GetTemplate(i, j).CardName;
                if(cardName == "Roar" || cardName == "Leader's Might" || cardName == "Clean" || cardName == "Bravado"
                    || cardName == "Euphoric Frenzy" || cardName == "Fury Swipes" || cardName == "Deadly Elegance" || cardName == "Devastation" || cardName == "Scratch") //Checks to see if the current card is excluded from the draft
                {
                    validPairs[i].Add(false); //Initializes coordinate pair value to false
                }
                else
                {
                    validPairs[i].Add(true); //Initializes coordinate pair value to true
                    trueCounters[i] += 1; //Increments the amount of valid cards for this minion
                }
            }
        }

        //Sets values false based upon indeces in the deck
        for (int i = 0; i < currentDeck.m_GameDeck.Count; i++)
        {
            CardData currentData = currentDeck.m_GameDeck[i];
            if (validPairs[currentData.ownerIndex][currentData.databaseIndex] != false) //Ensures the pair being set to false isn't already false
            {
                validPairs[currentData.ownerIndex][currentData.databaseIndex] = false; //Sets value found in deck to false
                trueCounters[currentData.ownerIndex] -= 1; //Decrements true count for this row
            }
        }
    }

    //Chooses three random cards to display - Reload was bound to this as well, but the reload button is no longer being used
    public void ChooseCards()
    {
        for (int i = 0; i < trueCounters.Count; i++)
        {
            if (trueCounters[i] != 0)
            {
                break;
            }
            else if (i == trueCounters.Count-1)
            {
                EndScene();
                return; //Exits if there are no cards to choose from
            }
        }
        for (int i = 0; i < 3; i++) //Chooses three cards
        {
            int randomMinion = UnityEngine.Random.Range(0, cardDatabase._cardPrefabs.BaseArray.Length);
            //Cycles index if invalid
            while(trueCounters[randomMinion] == 0)
            {
                if (randomMinion == cardDatabase._cardPrefabs.BaseArray.Length)
                {
                    randomMinion = 0;
                }
                else
                {
                    randomMinion++;
                }
            }

            //Sets Display:
            int randomIndex = 0;
            int randomIndexPre = UnityEngine.Random.Range(0, trueCounters[randomMinion]); //Chooses a random card for the given random minion
            while (validPairs[randomMinion][randomIndex] == false)
            {
                randomIndex++; //Ensures the initial value is valid
            }
            for (int j = 0; j < randomIndexPre; j++) //Iterates based upon random variable
            {
                randomIndex++; //Increments once per value generated by the random
                while(validPairs[randomMinion][randomIndex] == false) 
                {
                    randomIndex++; //Skips values that are false
                }
            }

            //Sets data
            if (currentData.Count <= i) //This means the current value has never been added to currentData(first iteration)
            {
                currentData.Add(cardDatabase.GetTemplate(randomMinion, randomIndex));
            }
            else
            {   
                currentData[i] = cardDatabase.GetTemplate(randomMinion, randomIndex);
            }
            //Setting of TMP elements:
            if (i == 0)
            {
                zeroName.text = currentData[0].CardName;
                zeroDesc.text = currentData[0].CardDescription;
                zeroImage.sprite = currentData[0].CardSprite;
            }
            else if (i == 1)
            {
                oneName.text = currentData[1].CardName;
                oneDesc.text = currentData[1].CardDescription;
                oneImage.sprite = currentData[1].CardSprite;
            }
            else if (i == 2)
            {
                twoName.text = currentData[2].CardName;
                twoDesc.text = currentData[2].CardDescription;
                twoImage.sprite = currentData[2].CardSprite;
            }
        }
        
    }

    //Functions to choose a card to claim:
    public void ClaimZero()
    {
        currentDeck.m_GameDeck.Add(currentData[0].Data); //Adds data to deck
        EndScene();
    }

    public void ClaimOne()
    {
        currentDeck.m_GameDeck.Add(currentData[1].Data);
        EndScene();
    }

    public void ClaimTwo()
    {
        currentDeck.m_GameDeck.Add(currentData[2].Data);
        EndScene();
    }

    public void EndScene()
    {
        //Load new deck into json and close this additive scene
        playerData.SetPlayerDeck(currentDeck.m_GameDeck);
        SaveDataJSON save = FindObjectOfType<SaveDataJSON>();
        save.SaveData();

        //Closing of scene:
        SceneManager.UnloadSceneAsync("Encounter Victory Scene");
        MapPlayer.sceneToToggle.SetActive(true);
    }
}
