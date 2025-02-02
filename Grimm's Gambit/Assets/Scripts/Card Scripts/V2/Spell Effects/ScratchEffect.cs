using UnityEngine;

[System.Serializable]
public class ScratchEffect : SpellEffect
{

    public ScratchEffect()
    {
        _requiresTarget = true;
    }

    public override void DoSpellEffect(Minion caster, Minion target)
    {
        HeirloomManager heirloomManager = GameObject.FindObjectOfType<HeirloomManager>();
        if (heirloomManager.ContainsHeirloom(Heirloom.Lycan))
        {
            target.AddAffix(Affix.Bleed, 2);//Adds 1 if Lycan is active
            target.DamageTaken(5);//Adds 2 if Lycan is active
        }
        else
        {
            target.AddAffix(Affix.Bleed, 1);
            target.DamageTaken(3);
        }
    }
}
