using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Deck : MonoBehaviour
{
    // Triggers on draw, broadcast indice of drawn card
    public static Action<int> onDraw;

    // Triggers on discard, broadcast indice of discarded card
    public static Action<int> onDiscard;
    
    // References database of all cards in the game
    [SerializeField] private CardDatabase m_DataBase;

    // To be implemented by design 
    [SerializeField] private int m_StartingHandSize, m_MaxHandSize;
    [SerializeField] private int m_NumDrawsPerTurn = 1;

    // Lists the indices of each card in the database
    [SerializeField] private List<int> m_GameDeck, m_Hand, m_DiscardPile;

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
        if(m_GameDeck.Count() == 0)
        {
            Shuffle();
        }
        
        // Add a card to the hand
        int nextCardID = m_GameDeck[index];
        m_GameDeck.RemoveAt(index);
        m_Hand.Add(nextCardID);

        onDraw?.Invoke(nextCardID);

        if (m_Hand.Count > m_MaxHandSize) Discard();
    }

    // Draw cards from the top of the deck 
    // Automatically draws one 
    // If drawing for turn, will draw set amount
    public void DrawAmount(bool forTurn = false, int amount = 1)
    {
        if (forTurn) amount = m_NumDrawsPerTurn;
        for (int i = 0; i < amount; i++) Draw();
    }

    // Conjure's a card into the hand
    // Will be discarded if the hand would exceed the maximum
    public void Conjure(int cardID)
    {
        if (m_Hand.Count() >= m_MaxHandSize)
        {
            m_DiscardPile.Add(cardID);
            return;
        }

        m_Hand.Add(cardID);
    }

    // Discard a card from hand 
    // Will automatically discard leftmost card
    // If the cardID is valid, will discard the card of the matching id 
    public void Discard(int index = 0, int cardID = -1)
    {
        int nextCardID = m_Hand[index];

        if (cardID >= 0)
        {
            nextCardID = cardID;
            m_Hand.Remove(cardID);
        }
        else
        {
            m_Hand.RemoveAt(index);
        }
       
        m_DiscardPile.Add(nextCardID);

        onDiscard?.Invoke(nextCardID);
    }

    public void DiscardHand()
    {
        m_DiscardPile.Concat(m_Hand).ToList();
        m_Hand.Clear();
    }

    // Put the top card of the deck into the discard pile
    // Or select a specific card from the game deck
    public void Mill(int index = 0)
    {
       int nextCardID = m_GameDeck[index];
       m_GameDeck.RemoveAt(index);
       m_DiscardPile.Add(nextCardID);
    }

    // Mill a specificed amount of cards from the top of the deck 
    public void MillAmount(int amount)
    {
        for (int i = 0; i < amount; i++) Mill();
    }

    // Takes the target card's ID in the database 
    // Defaults to searching the hand
    // Will search discard pile if false 
    // Return true if successful
    public bool ReverseCard(int cardID, bool inHand = true)
    {
        // Search the hand   
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
    }

    // Helper method for ReverseCard
    private int ReverseID(int cardID) {
        if (cardID % 2 == 0) cardID++;
        else cardID--;
        
        return cardID;
    }

    // Remove all cards owned by a specific party member from the deck object
    public void RemoveCards(Minion owner)
    {
        for (int i = m_DiscardPile.Count() - 1; i >= 0; i--)
        {
            Card current = m_DataBase.GetCard(m_DiscardPile[i]);
            if (current.GetCaster() == owner) m_DiscardPile.RemoveAt(i);
        }

        for (int i = m_GameDeck.Count() - 1; i >= 0; i--)
        {
            Card current = m_DataBase.GetCard(m_GameDeck[i]);
            if (current.GetCaster() == owner) m_GameDeck.RemoveAt(i);
        }

        for (int i = m_Hand.Count() - 1; i >= 0; i--)
        {
            Card current = m_DataBase.GetCard(m_Hand[i]);
            if (current.GetCaster() == owner) m_Hand.RemoveAt(i);
        }
    }

    public void Shuffle()
    {
        List<int> temp = m_DiscardPile;
        m_DiscardPile.Clear();

        for (int i = temp.Count - 1; i >= 0; i--)
        {
            int randomNum = UnityEngine.Random.Range(0, temp.Count());
            int randomCard = temp[randomNum];

            temp.RemoveAt(randomNum);
            m_GameDeck.Add(randomCard);
        } 
    }

    // Prepare the deck for the game
    public void StartDeck()
    {
        ClearAll();

        m_GameDeck = m_DataBase.PopulateDeck();
        
        Shuffle();

        // Draw hand
        DrawAmount(false, m_StartingHandSize);
    }

    private void ClearAll()
    {
        m_GameDeck.Clear();
        m_Hand.Clear();
        m_DiscardPile.Clear();
    }

    public List<int> GetHand()
    {
        return m_Hand;
    }

    public List<int> GetDiscardPile()
    {
        return m_DiscardPile;
    }

    public int GetGameDeckSize()
    {
        return m_GameDeck.Count();
    }

    // Check if the player has a card of cardID in the deck, hand or discard pile
    public bool Contains(int cardID)
    {
        return m_GameDeck.Contains(cardID) || m_Hand.Contains(cardID) || m_DiscardPile.Contains(cardID);
    }
}
