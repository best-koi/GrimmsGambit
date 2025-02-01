using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSelf : SpellComponent
{
    [SerializeField] private int m_Damage;

    private static bool applyingEffect = false;

    public DamageSelf()
    {
        spellName = "DamageSelf";
        spellDescription = "Deal an amount of damage to the caster of the spell.";
    }

    public void SetDamage(int dmgToSet)
    {
        m_Damage = dmgToSet;
    }

    public override void DoSpellEffect()
    {
        caster.MinionUsed(caster, m_Damage); 

        // Used for the insecurity spell
        // Can be changed to make affix/value a member variable for more customization
        if (applyingEffect) caster.AddAffix(Affix.Strength, 3);
    }

    public static void ApplyEffect()
    {
        applyingEffect = true;
    }
}
