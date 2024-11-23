using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeckDisplay : MonoBehaviour
{
    [SerializeField]
    private TMP_Text deckText, discardText;//Text for each value

    [SerializeField]
    private Deck playerDeck;//The player's deck

//Displays the Discard and Deck Size
    private void Update(){
        deckText.text = $"{playerDeck.GetGameDeckSize()}";
        discardText.text = $"{playerDeck.GetDiscardPile().Count}";
    }




}
