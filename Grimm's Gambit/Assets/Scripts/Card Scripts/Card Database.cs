using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDatabase : MonoBehaviour
{
    [SerializeField] private Card[] m_Data;

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    public Card[] GetData()
    {
        return m_Data;
    }

    public void AddCard(int index, int numTimes = 1)
    {
        m_Data[index].NumCopies += numTimes;
    }
}
