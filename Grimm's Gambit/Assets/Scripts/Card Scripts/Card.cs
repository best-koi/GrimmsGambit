using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] private protected string cardName;

    [SerializeField] private protected int cardCost;

    [SerializeField] private protected bool isReversed = false;

    // Creature information doesn't exist yet, I'll add it once the Unit class is made

    // [SerializeField]
    // private Creature creature;

    private void OnMouseDown()
    {
        Component[] spells = gameObject.GetComponents(typeof(SpellComponent));
        foreach(SpellComponent spell in spells) {
            spell.DoSpellEffect();
        }

        //  if (creature is not null)
        //      creature.doSpawn() etc

        PlayerHand hand = FindObjectOfType<PlayerHand>();
        hand.RemoveCard(this);
        
        Destroy(gameObject);
    }

    public int GetCardCost()
    {
        return cardCost;
    }

    public void ReverseCard()
    {
        isReversed = !isReversed;
    }
}
