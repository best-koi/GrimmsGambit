using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnDeath : CheckTargetDeath
{
    [SerializeField] private int m_DirectAmount, m_AOEAmount;

    public virtual void DoTriggeredEffect()
    {
        target.DamageTaken(m_DirectAmount);
    }

    public virtual void DoTrueEffect()
    {
        List<Transform> enemyParty;
        UnitParty[] parties = FindObjectsOfType<UnitParty>();
        List<Transform> currentParty = parties[0].GetAll();
        if (!currentParty[0].GetComponent<Minion>().ownerPlayer)
        {
            enemyParty = currentParty;
        }
        else
        {
            enemyParty = parties[1].GetAll();
        }
        foreach (Transform member in enemyParty)
        {
            target = member.GetComponent<Minion>();
            target.DamageTaken(m_AOEAmount);
        }
    }
}
