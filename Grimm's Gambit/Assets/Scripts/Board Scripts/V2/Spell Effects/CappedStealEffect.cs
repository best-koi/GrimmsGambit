using UnityEngine;

[System.Serializable]
public class CappedStealEffect : SpellEffect
{
    [SerializeField] protected Affix _targetDebuff;
    [SerializeField] protected int _limit;

    public CappedStealEffect()
    {
        _spellName = "Capped Steal";
        _spellDescription = "When this card is played, caster steals an amount of the given debuff less than or equal to the given limit.";
    }

    public override void DoSpellEffect(Minion caster, Minion target)    //Tries to steal "limit" amount of "targetDebuff"
    {
        if (target.currentAffixes.ContainsKey(_targetDebuff))
        {
            int currentStacks = target.currentAffixes[_targetDebuff];
            if (currentStacks > _limit)
            {
                target.currentAffixes.Remove(_targetDebuff); //Replaces affix with a lesser amount
                target.currentAffixes.Add(_targetDebuff, currentStacks - _limit);
                caster.AddAffix(_targetDebuff, _limit); //Grants caster limit
            }
            else //Grants caster current Stacks
            {
                target.currentAffixes.Remove(_targetDebuff);
                caster.AddAffix(_targetDebuff, currentStacks);
            }
        }
    }
}
