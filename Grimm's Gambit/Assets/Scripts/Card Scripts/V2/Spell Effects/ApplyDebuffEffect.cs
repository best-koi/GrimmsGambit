using UnityEngine;

[System.Serializable]
public class ApplyDebuffEffect : SpellEffect
{
    [SerializeField] private Affix _debuff;
    [SerializeField] private int _value;

    public ApplyDebuffEffect()
    {
        _requiresTarget = true;
    }

    public override void DoSpellEffect(Minion caster, Minion target)
    {
        target.AddAffix(_debuff, _value);
    }
}
