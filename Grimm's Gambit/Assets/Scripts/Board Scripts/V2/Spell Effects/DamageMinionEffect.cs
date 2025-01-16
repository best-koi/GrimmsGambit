using UnityEngine;

[System.Serializable]
public class DamageMinionEffect : SpellEffect
{
    [SerializeField] private int _damage;
    public DamageMinionEffect()
    {
        _requiresTarget = true;
    }

    public void SetDamage(int dmgToSet)
    {
        _damage = dmgToSet;
    }

    public override void DoSpellEffect(Minion caster, Minion target)
    {
        caster.MinionUsed(target, _damage); //deals damage to target character from the caster character
    }
}
