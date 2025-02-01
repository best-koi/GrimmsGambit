using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTarget : SpellComponent
{
    [SerializeField] private protected bool m_CheckingSelf;

    public CheckTarget()
    {
        spellName = "CheckTarget";
        spellDescription = "Check if a target or self is being targeted by another minion";

        if (!m_CheckingSelf) requiresTarget = true;
    }

    public override void DoSpellEffect()
    {
        Minion minionToCheck;
        if (requiresTarget) minionToCheck = caster;
        else minionToCheck = target;

        List<Transform> enemyParty;
        UnitParty[] parties = FindObjectsOfType<UnitParty>();
        List<Transform> currentParty = parties[0].GetAll();
        if (minionToCheck.ownerPlayer != currentParty[0].GetComponent<Minion>().ownerPlayer)
        {
            enemyParty = currentParty;
        }
        else
        {
            enemyParty = parties[1].GetAll();
        }

        foreach (Transform member in enemyParty)
        {
            if (member.GetComponent<EnemyTemplate>().GetAttackTarget() == minionToCheck)
            {
                DoTrueEffect();
                return;
            }
        }

        DoFalseEffect();
    }

    public virtual void DoTrueEffect()
    {
        Debug.Log("Do the effect where where the condition is met.");
    }

    public virtual void DoFalseEffect()
    {
        Debug.Log("Do the effect where where the condition is not met.");
    }
}
