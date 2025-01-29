using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

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
    //[SerializeField] private CardDatabase m_DataBase;

    // To be implemented by design 
    [SerializeField] private int m_StartingHandSize, m_MaxHandSize;
    [SerializeField] private int m_NumDrawsPerTurn = 1;

    //These refences are needed for the Deadly Elegance and Trinity Strike spells to function - Added Ryan Lockie 11/19/2024
    public int m_MaxCountThisTurn;

    // Lists the indices of each card in the database
    [SerializeField] private List<CardData> m_GameDeck, m_Hand, m_DiscardPile, m_RemovedZone;

    private void Awake()
    {
        onDraw = null;
        onDiscard = null;
    }

    // Draw a card from the top of the deck
    // Alternatively draw specific card from the deck
    public void Draw(int index = 0)
    {
        // If the player's current hand size is greater than or equal the max hand size
        // Will put the top card of the deck in the discard pile instead of drawing it 
        if (m_Hand.Count() >= m_MaxHandSize)
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

        if (m_Hand.Count > m_MaxHandSize) Discard(nextCardData);
    }

    // Draw cards from the top of the deck 
    // Automatically draws one 
    // If drawing for turn, will draw set amount
    public void DrawAmount(bool forTurn = false, int amount = 1)
    {
        if (forTurn) amount = m_NumDrawsPerTurn;
        for (int i = 0; i < amount; i++) Draw();

        if (forTurn) m_MaxCountThisTurn = m_Hand.Count(); //Counts after this since this includes katze's bonus card which will have been added earlier
    }

    // Conjure's a card into the hand
    // Will be discarded if the hand would exceed the maximum
    public void Conjure(int ownerIndex, int databaseIndex)
    {
        Conjure(new CardData(ownerIndex, databaseIndex));
    }

    public void Conjure(CardData data)
    {
        if (m_Hand.Count() >= m_MaxHandSize)
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
        m_Hand.Remove(data);
        onDiscard?.Invoke(data.databaseIndex);
    }

    // Invokes index in database not hand 
    // Do not use with William's remove function
    // Will cause bugs
    public void Discard(Card card)
    {
        Discard(card.GetData());
    }

    public void Discard(CardV2 card)
    {
        Discard(card.Data);
    }

    public void DiscardRandomInHand()
    {
        System.Random randomObject = new System.Random();
        int randomIndex = randomObject.Next(0, m_Hand.Count-1); //Chooses random int between 0 and final card in hand currently
        //Removes card from randomly selected index
        m_DiscardPile.Add(m_Hand[randomIndex]);
        onDiscard?.Invoke(randomIndex);
        m_Hand.Remove(m_Hand[randomIndex]);
    }

    public void DiscardHand()
    {
        for (int i = m_Hand.Count - 1; i >= 0; i--)
        {
            m_DiscardPile.Add(m_Hand[i]);

            // Invoke the index in the hand
            // Compatible with remove function of Object Container
            onDiscard?.Invoke(i);

            m_Hand.Remove(m_Hand[i]);
        }
    }

    // Removes card to a special zone
    public void RemoveCard(Card card)
    {
        CardData data = card.GetData();
        m_RemovedZone.Add(data);
        m_Hand.Remove(data);
    }

    public void RemoveCard(CardV2 card)
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

    // Set for change
    // Unfinished
    public Card ReverseCard(Card card)
    {
        return null;

        /**
        int reverse = ReverseID(cardID);

        if (m_DataBase.GetCard(reverse) == null)
        {
            Debug.Log("Card of reversed ID has no reversed component.");
            return false;
        }

        if (inHand)
        {
            for (int i = 0; i < m_Hand.Count; i++)
            {
                if (m_Hand[i] == cardID)
                { 
                    m_Hand[i] = reverse;
                    return true;
                }
            }

            Debug.Log("Failed to find card of referenced ID in hand.");
            return false;
        }
        
        // Search the discard pile
        for (int i = 0; i < m_DiscardPile.Count; i++)
        {
            if (m_DiscardPile[i] == cardID)
            {
                m_DiscardPile[i] = reverse;
                return true;
            }
        }

        Debug.Log("Failed to find card of referenced ID in discard pile.");
        return false;
        */
    }

    // Helper method for ReverseCard
    private int ReverseID(int cardID) {
        if (cardID % 2 == 0) cardID++;
        else cardID--;
        
        return cardID;
    }

    // Fixed
    public void RemoveCards(int owner)
    {
        Debug.Log($"{owner}: Called");

        for (int i = m_DiscardPile.Count() - 1; i >= 0; i--)
        {
            if (m_DiscardPile[i].ownerIndex == owner)
            {
                m_RemovedZone.Add(m_DiscardPile[i]);
                m_DiscardPile.RemoveAt(i);
            }
        }

        for (int i = m_GameDeck.Count() - 1; i >= 0; i--)
        {
            if (m_GameDeck[i].ownerIndex == owner)
            {
                m_RemovedZone.Add(m_GameDeck[i]);
                m_GameDeck.RemoveAt(i);
            }
        }

        for (int i = m_Hand.Count() - 1; i >= 0; i--)
        {
            if (m_Hand[i].ownerIndex == owner)
            {
                m_RemovedZone.Add(m_Hand[i]);
                m_Hand.RemoveAt(i);
            }
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

    // Not in use
    private void ClearAll()
    {
        m_GameDeck.Clear();
        m_Hand.Clear();
        m_DiscardPile.Clear();
    }

    public List<CardData> GetHand()
    {
        return m_Hand;
    }

    public List<CardData> GetDiscardPile()
    {
        return m_DiscardPile;
    }

    //Function to return current hand count for card functionality
    public int CurrentCardCount()
    {
        return m_Hand.Count();
    }

    public int GetGameDeckSize()
    {
        return m_GameDeck.Count();
    }
}
