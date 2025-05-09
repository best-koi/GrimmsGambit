using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AddAffixPerTargetEffect : SpellEffect
{
    [SerializeField] GenericDict<Affix, int> m_Affixes;

    // True to apply based on if the enemies are targetting
    // False if to check for the enemies that have yet to target
    [SerializeField] private bool m_IsTargetting, m_CheckingSelf;

    public AddAffixPerTargetEffect()
    {
        _spellName = "Add Affix Per Target";
        _spellDescription = "Add an affix based on the number of enemies targetting (or not targetting) this enemy.";

        if (!m_CheckingSelf) _requiresTarget = true;
    }

    public override void DoSpellEffect(Minion caster, Minion target)
    {
        //The following assumes that only two parties exist and characters have properly labeled owners.
        //This is used to ensure that the friendly party is healed
        List<Transform> enemyParty;
        UnitParty[] parties = GameObject.FindObjectsOfType<UnitParty>();
        List<Transform> currentParty = parties[0].GetAll();
        if (!currentParty[0].GetComponent<Minion>().ownerPlayer)
        {
            enemyParty = currentParty;
        }
        else
        {
            enemyParty = parties[1].GetAll();
        }

        Minion minionToCheck;
        if (_requiresTarget) minionToCheck = caster;
        else minionToCheck = target;

        foreach (Transform member in enemyParty)
        {
            if (member.GetComponent<EnemyTemplate>().GetAttackTarget() == minionToCheck && m_IsTargetting) AddAffix(minionToCheck);
            else if (member.GetComponent<EnemyTemplate>().GetAttackTarget() != minionToCheck && !m_IsTargetting) AddAffix(minionToCheck);
        }
    }

    // Helper method
    private void AddAffix(Minion affixTarget)
    {
        for (int i = 0; i < m_Affixes.GetLength(); i++)
        {
            Affix key = m_Affixes.GetKey(i);
            affixTarget.AddAffix(key, m_Affixes.GetValue(key));
        }
    }
}

