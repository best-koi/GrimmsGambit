using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class CardData
{
    // Used for 2D array

    [SerializeField] internal int ownerIndex, databaseIndex;

    public CardData (int ownerIndex, int dataIndex)
    {
        this.ownerIndex = ownerIndex;
        this.databaseIndex = dataIndex;
    }

    public static bool operator ==(CardData data1, CardData data2) => data1.Equals(data2);
    public static bool operator !=(CardData data1, CardData data2) => !(data1 == data2);
    public override bool Equals(object obj)
    {
        if (obj == null)
            return false;

        return obj is CardData b2 ? (ownerIndex == b2.ownerIndex && databaseIndex == b2.databaseIndex) : false;
    }
    public override int GetHashCode() => (ownerIndex, databaseIndex).GetHashCode();
}


public class Deck : MonoBehaviour
{
    // Triggers on draw, broadcast indice of drawn card
    // Intended to display index in database
    public static Action<int, int> onDraw;

    // Triggers on discard, broadcast indice of discarded card
    // Intended to display index in database
    // Sometimes will display index in hand
    public static Action<int> onDiscard;
    
    // References database of all cards in the game
    [SerializeField] private CardDatabase m_DataBase;

    // To be implemented by design 
    [SerializeField] private int m_StartingHandSize, m_MaxHandSize;
    [SerializeField] private int m_NumDrawsPerTurn = 1;

    //These refences are needed for the Deadly Elegance and Trinity Strike spells to function - Added Ryan Lockie 11/19/2024
    public int m_MaxCountThisTurn;

    // Lists the indices of each card in the database
    public List<CardData> m_GameDeck, m_Hand, m_DiscardPile, m_RemovedZone;


    //Function to return current hand count for card functionality
    public int CurrentCardCount { get => m_Hand.Count; }
    public int GameDeckSize { get => m_GameDeck.Count; }



    private void Awake()
    {
        onDraw = null;
        onDiscard = null;

        if (m_GameDeck == null)
            m_GameDeck = new List<CardData>();
        if (m_Hand == null)
            m_Hand = new List<CardData>();
        if (m_DiscardPile == null)
            m_DiscardPile = new List<CardData>();
        if (m_RemovedZone == null)
            m_RemovedZone = new List<CardData>();
    }

    // Draw a card from the top of the deck
    // Alternatively draw specific card from the deck
    public void Draw(int index = 0)
    {
        // If the player's current hand size is greater than or equal the max hand size
        // Will put the top card of the deck in the discard pile instead of drawing it 
        if (CurrentCardCount >= m_MaxHandSize)
        {
            Mill();
            Debug.Log("Exceeded maximum hand size. Drawn card will placed in the discard pile.");
            return;
        }

        // Shuffle the discard pile into the game deck if it is empty
        if(m_GameDeck.Count() <= 0)
        {
            Shuffle();
        }
        
        // Add a card to the hand
        CardData nextCardData = m_GameDeck[index];
        m_GameDeck.RemoveAt(index);
        m_Hand.Add(nextCardData);

        // Compatible with AddCard
        onDraw?.Invoke(nextCardData.ownerIndex, nextCardData.databaseIndex);

        if (CurrentCardCount > m_MaxHandSize) Discard(nextCardData);
    }

    // Draw cards from the top of the deck 
    // Automatically draws one 
    // If drawing for turn, will draw set amount
    public void DrawAmount(bool forTurn = false, int amount = 1)
    {
        if (forTurn) amount = m_NumDrawsPerTurn;
        for (int i = 0; i < amount; i++) Draw();
        if (forTurn) m_MaxCountThisTurn = CurrentCardCount; //Counts after this since this includes katze's bonus card which will have been added earlier
    }

    // Conjure's a card into the hand
    // Will be discarded if the hand would exceed the maximum
    public void Conjure(int ownerIndex, int databaseIndex)
    {
        Conjure(new CardData(ownerIndex, databaseIndex));
    }

    public void Conjure(CardData data)
    {
        if (CurrentCardCount >= m_MaxHandSize)
        {
            /*m_DiscardPile.Add(data);
            onDiscard?.Invoke(data.databaseIndex); // Not compatible with Remove function*/ //Changes on 1/25/25 By Ryan Lockie so that cards which are conjured are not added to the discard pile if over capped.
            Debug.Log("Exceeded maximum hand size. Conjuered card will placed in the discard pile.");
            return;
        }

        onDraw?.Invoke(data.ownerIndex, data.databaseIndex); // Compatible with AddCard
        m_Hand.Add(data);
    }

    public void Discard(CardData data)
    {
        m_DiscardPile.Add(data);

        for (int i = 0; i < CurrentCardCount; i++)
            if (m_Hand[i] == data)
            {
                m_Hand.RemoveAt(i);
                break;
            }
    }

    // Invokes index in database not hand 
    // Do not use with William's remove function
    // Will cause bugs


    public void Discard(Card card)
    {
        Discard(card.Data);
    }

    public void DiscardFromHand(int index)
    {
        CardData cardToDiscard = m_Hand[index];
        m_DiscardPile.Add(cardToDiscard);
        onDiscard?.Invoke(index);
        m_Hand.Remove(cardToDiscard);
    }

    public void DiscardRandomInHand()
    {
        System.Random randomObject = new System.Random();
        int randomIndex = randomObject.Next(0, CurrentCardCount); //Chooses random int between 0 and final card in hand currently
        DiscardFromHand(randomIndex);   //Removes card from randomly selected index
    }

    public void DiscardHand()
    {
        for (int i = CurrentCardCount - 1; i >= 0; i--)
            DiscardFromHand(i);
    }

    public void RemoveCard(Card card)
    {
        CardData data = card.Data;
        m_RemovedZone.Add(data);
        m_Hand.Remove(data);
    }

    // Put the top card of the deck into the discard pile
    // Or select a specific card from the game deck
    public void Mill(int index = 0)
    {
       CardData nextCardData = m_GameDeck[index];
       m_GameDeck.RemoveAt(index);
       m_DiscardPile.Add(nextCardData);
    }

    // Mill a specificed amount of cards from the top of the deck 
    public void MillAmount(int amount)
    {
        for (int i = 0; i < amount; i++) Mill();
    }


    /*
    // Helper method for ReverseCard
    private int ReverseID(int cardID) 
    {
        if (cardID % 2 == 0) cardID++;
        else cardID--;
        
        return cardID;
    }
    */

    // Fixed
    public void RemoveCards(int owner)
    {
        Debug.Log($"{owner}: Called");

        _removeAllFromOwner(owner, m_DiscardPile);
        _removeAllFromOwner(owner, m_GameDeck);
        _removeAllFromOwner(owner, m_Hand);
    }

    private void _removeCard(CardData cardData, List<CardData> listFrom)
    {
        m_RemovedZone.Add(cardData);
        listFrom.Remove(cardData);
    }

    private void _removeCard(Card card, List<CardData> listFrom)
    {
        _removeCard(card.Data, listFrom);
    }

    private void _removeAllFromOwner(int ownerIndex, List<CardData> listFrom)
    {
        for (int i = listFrom.Count() - 1; i >= 0; i--)
        {
            CardData data = listFrom[i];
            if (data.ownerIndex == ownerIndex) _removeCard(data, listFrom);
        }
    }

    // Used by draw function
    public void Shuffle()
    {
        List<CardData> temp = new List<CardData>(m_DiscardPile);
        m_DiscardPile.Clear();

        for (int i = temp.Count - 1; i >= 0; i--)
        {
            System.Random rnd = new System.Random();
            int randomNum = rnd.Next(0, temp.Count());
            CardData randomCard = temp[randomNum];

            temp.RemoveAt(randomNum);
            m_GameDeck.Add(randomCard);
        } 
    }

    // Used by encounter controller
    public void ShuffleDeck()
    {
        List<CardData> temp = new List<CardData>(m_GameDeck);
        m_GameDeck.Clear();

        for (int i = temp.Count - 1; i >= 0; i--)
        {
            System.Random rnd = new System.Random();
            int randomNum = rnd.Next(0, temp.Count());
            CardData randomCard = temp[randomNum];

            temp.RemoveAt(randomNum);
            m_GameDeck.Add(randomCard);
        } 
    }


    public CardTemplate GetCard (CardData data)
    {
       
        return (CardTemplate)m_DataBase._cardPrefabs.GetValue(data.ownerIndex, data.databaseIndex);
    }

    /*
    // Not in use
    private void ClearAll()
    {
        m_GameDeck.Clear();
        m_Hand.Clear();
        m_DiscardPile.Clear();
    }
    */
}
