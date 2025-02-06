using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DamageOnDeathEffect : CheckTargetDeathEffect
{
    [SerializeField] private int m_DirectAmount, m_AOEAmount;

    public override void DoSuccessEffect(Minion caster, Minion target)
    {
        target.DamageTaken(m_DirectAmount);
    }

    public override void DoFailedEffect(Minion caster, Minion target)
    {
        List<Transform> enemyParty;
        UnitParty[] parties = GameObject.FindObjectsOfType<UnitParty>();
        List<Transform> currentParty = parties[0].GetAll();
        
        if (!currentParty[0].GetComponent<Minion>().ownerPlayer)
            enemyParty = currentParty;
        else
            enemyParty = parties[1].GetAll();

        foreach (Transform member in enemyParty)
        {
            target = member.GetComponent<Minion>();
            target.DamageTaken(m_AOEAmount);
        }
    }
}
