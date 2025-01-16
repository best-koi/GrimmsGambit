using UnityEngine;

[System.Serializable]
public class FollowThroughEffect : CheckHealthEffect
{
    [SerializeField] private int _buffedDamage, _baseDamage;

    public override void DoSuccessEffect(Minion caster, Minion target)
    {
        caster.MinionUsed(target, _buffedDamage);
    }

    public override void DoFailedEffect(Minion caster, Minion target)
    {
        caster.MinionUsed(target, _baseDamage);
    }
}
