using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTarget : SpellComponent
{
    private protected bool m_CheckingSelf;
    private protected bool m_IsBeingTargeted = false;

    public CheckTarget()
    {
        spellName = "CheckTarget";
        spellDescription = "Check if a target or self is being targeted by another minion";

        if (!m_CheckingSelf) requiresTarget = true;
    }

    public override void DoSpellEffect()
    {
        Card thisCard = GetComponent<Card>();
        Minion minionToCheck;
        if (requiresTarget) minionToCheck = thisCard.GetTarget();
        else minionToCheck = thisCard.GetCaster();

        List<GameObject> enemyParty;
        UnitParty[] parties = FindObjectsOfType<UnitParty>();
        List<GameObject> currentParty = parties[0].GetAllMembers();
        if (minionToCheck.ownerPlayer != currentParty[0].GetComponent<Minion>().ownerPlayer)
        {
            enemyParty = currentParty;
        }
        else
        {
            enemyParty = parties[1].GetAllMembers();
        }

        foreach (GameObject member in enemyParty)
        {
            // Unfinished
            // Likely will be changed from minion
            // Check if that member is targeting minionToCheck
            if (member.GetComponent<Minion>())
            {
                m_IsBeingTargeted = true;
                break;
            }
        }

    }

    public virtual void DoRealEffect()
    {
        Debug.Log("Augment another spell");
    }
}
