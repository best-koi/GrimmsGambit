using UnityEngine;

[System.Serializable]
public class DamageSelfEffect : SpellEffect
{
    [SerializeField] private int m_Damage;

    private static bool applyingEffect = false;

    public DamageSelfEffect()
    {
        _spellName = "DamageSelf";
        _spellDescription = "Deal an amount of damage to the caster of the spell.";
    }

    public void SetDamage(int dmgToSet)
    {
        m_Damage = dmgToSet;
    }

    public static void ApplyEffect()
    {
        applyingEffect = true;
    }

    public override void DoSpellEffect(Minion caster, Minion target)
    {
        caster.MinionUsed(caster, m_Damage);

        // Used for the insecurity spell
        // Can be changed to make affix/value a member variable for more customization
        if (applyingEffect) caster.AddAffix(Affix.Strength, 3);
    }
}
