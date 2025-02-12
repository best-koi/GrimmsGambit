using UnityEngine;

[System.Serializable]
public class ApplyBasedOnAffixEffect : SpellEffect
{
    [SerializeField] private Affix _checkedDebuff;
    [SerializeField] private bool _checkSelf = false;
    [SerializeField] private bool _removeCheckedDebuff = false;
    [SerializeField] private ApplyType _applyType = ApplyType.DAMAGE;
    [SerializeField] private Affix _newDebuff;
    [SerializeField] private float _amountMultiplier = 1.0f;
    [SerializeField] private int _capValue = -1;
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

        Minion minionToApply = _applyToSelf ? caster : target;
        int newAmount = (int)Mathf.Ceil(amount * _amountMultiplier);
        if (_capValue >= 0)
            newAmount = Mathf.Min(newAmount, _capValue);

        switch (_applyType)
        {
            case ApplyType.DAMAGE:
                minionToApply.DamageTaken(newAmount);
                break;

            case ApplyType.HEALTH:
                minionToApply.DamageTaken(-newAmount);
                break;

            case ApplyType.NEW_EFFECT:
                minionToApply.AddAffix(_newDebuff, newAmount);
                break;
        }
    }
}

public enum ApplyType
{
    DAMAGE,
    HEALTH,
    NEW_EFFECT
}
