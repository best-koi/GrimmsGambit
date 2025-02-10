using UnityEngine;

[System.Serializable]
public class ClearAffixEffect : SpellEffect
{
    [SerializeField] private ClearType _clearingType;
    [SerializeField] private Affix _affixToClear;
    [Tooltip ("Removes amount of affix manually. Will remove all if amount is negative.")][SerializeField] private int _amount = -1;

    public ClearAffixEffect()
    {
        _spellName = "Cleanse";
        _spellDescription = "When this card is played, target loses all debuffs.";
    }

    public override void DoSpellEffect(Minion caster, Minion target)
    {
        switch (_clearingType)
        {
            case ClearType.ALL:
                target.RemoveAllAffixes();
                break;

            case ClearType.RANDOM:
                break;

            case ClearType.SPECIFIC:
                target.RemoveAffix(_affixToClear, _amount);
                break;
        }
    }
}

public enum ClearType
{
    ALL,
    RANDOM,
    SPECIFIC
}
