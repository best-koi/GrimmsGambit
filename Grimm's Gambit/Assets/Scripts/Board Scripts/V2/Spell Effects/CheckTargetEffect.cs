using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class CheckTargetEffect : SpellEffectWithCheck
{
    [SerializeField] protected bool m_CheckingSelf;

    public CheckTargetEffect()
    {
        _spellName = "CheckTarget";
        _spellDescription = "Check if a target or self is being targeted by another minion";

        if (!m_CheckingSelf) _requiresTarget = true;
    }

    public override void DoSpellEffect(Minion caster, Minion target)
    {
        Minion minionToCheck;
        if (_requiresTarget) minionToCheck = caster;
        else minionToCheck = target;

        List<Transform> enemyParty;
        UnitParty[] parties = GameObject.FindObjectsOfType<UnitParty>();
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
                DoSuccessEffect(caster, target);
                return;
            }
        }

        DoFailedEffect(caster, target);
    }
}
