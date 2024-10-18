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
    private Card[] m_Data;

    [SerializeField] private int m_HandSize;

    // Lists the indices of each card in the database
    [SerializeField] private List<int> m_GameDeck, m_Hand, m_DiscardPile;

    // Draw a card from the top of the deck
    // Alternatively draw specific card from the deck
    public void Draw(int index = 0)
    {
        // Shuffle the discard pile into the game deck if it is empty
        if(m_GameDeck.Count() == 0)
        {
            m_GameDeck = m_DiscardPile;
            m_DiscardPile.Clear();
            Shuffle();
        }
        
        int nextCardID = m_GameDeck[index];
        m_GameDeck.RemoveAt(index);
        m_Hand.Add(nextCardID);

        onDraw?.Invoke(nextCardID);
        Debug.Log("Player drew " + m_Data[nextCardID].GetName() + ".");
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
        Debug.Log(m_Data[nextCardID].GetName() + "entered the discard pile.");
    }

    // Put the top card of the deck into the discard pile
    // Or select a specific card from the game deck
    // Repeat any number of times
    public void Mill(int index = 0, int numTimes = 1)
    {
        for (int i = 0; i < numTimes; i++)
        {
            int nextCardID = m_GameDeck[index];
            m_GameDeck.RemoveAt(index);
            m_DiscardPile.Add(nextCardID);

            Debug.Log(m_Data[nextCardID].GetName() + "entered the discard pile.");
        }
    }

    public void Shuffle()
    {
        List<int> temp = m_GameDeck;
        m_GameDeck.Clear();

        foreach (int card in temp)
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
        m_Data = m_DataBase.GetData();

        ClearAll();

        // Populate the deck based on the database
        foreach (Card card in m_Data)
        {
            for (int i = 0, copies = card.NumCopies; i < copies; i++)
            {
                m_GameDeck.Add(card.GetIndex());
            }
        }
        
        Shuffle();

        // Draw hand
        for (int i = 0; i < m_HandSize - 1; i++) 
        {
            Draw();
        }
    }

    public void ClearAll()
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

    public bool Contains(int cardID)
    {
        return m_GameDeck.Contains(cardID);
    }
}
