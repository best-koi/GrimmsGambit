using UnityEngine;
using TMPro;

public class DeckDisplay : MonoBehaviour
{
    [SerializeField]
    private TMP_Text deckText, discardText;//Text for each value

    [SerializeField]
    private Deck playerDeck;//The player's deck

    //Displays the Discard and Deck Size
    private void Update()
    {
        deckText.text = $"{playerDeck.GameDeckSize}";
        discardText.text = $"{playerDeck.m_DiscardPile.Count}";
    }
}
