using UnityEngine;

[System.Serializable]
public class ApplyBasedOnAffixEffect : SpellEffect
{
    [SerializeField] private Affix _checkedDebuff;
    [SerializeField] private bool _checkSelf = false;
    [SerializeField] private bool _removeCheckedDebuff = false;
    [SerializeField] private Affix _newDebuff;
    [SerializeField] private float _amountMultiplier = 1.0f;
    [SerializeField] private bool _applyToSelf = false;

    public ApplyBasedOnAffixEffect()
    {
        _spellName = "Remove Affix + Do Effect";
        _spellDescription = "Removes an Affix and does an effect based on the amount of that affix removed";
    }

    public override void DoSpellEffect(Minion caster, Minion target)
    {
        Minion minionToCheck = _checkSelf ? caster : target;

        int amount = minionToCheck.GetAffixCount(_checkedDebuff);
        if (_removeCheckedDebuff)
            minionToCheck.RemoveAffix(_checkedDebuff);

        if (amount < 0)
        {
            Debug.Log("Minion does not have selected affix applied.");
            return;
        }

        if (_applyToSelf)
            caster.AddAffix(_newDebuff, (int)(amount * _amountMultiplier));
        else
            target.AddAffix(_newDebuff, (int)(amount * _amountMultiplier));
    }
}
