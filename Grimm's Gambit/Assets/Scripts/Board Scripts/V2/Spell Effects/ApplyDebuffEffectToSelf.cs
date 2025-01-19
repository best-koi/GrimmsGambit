using UnityEngine;

[System.Serializable]
public class ApplyDebuffEffectToSelf : SpellEffect
{
    [SerializeField] private Affix _debuff;
    [SerializeField] private int _value;

    public ApplyDebuffEffectToSelf()
    {
        _requiresTarget = false;
    }

    public override void DoSpellEffect(Minion caster, Minion target)
    {
        caster.AddAffix(_debuff, _value);
    }
}
