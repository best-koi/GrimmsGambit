using UnityEngine;

[System.Serializable]
public class SpellEffect
{
    #region Private Fields

    [SerializeField] protected string _spellName;
    [SerializeField] protected string _spellDescription;
    [SerializeField] protected bool _requiresTarget = false;

    #endregion

    #region Properties

    public string SpellName { get => _spellName; }
    public string SpellDescription { get => _spellDescription; }
    public bool RequiresTarget { get => _requiresTarget; }

    #endregion

    #region Public Methods

    // Generic function for a spell's effect
    // Meant to be overridden by inherited SpellComponents, will print an error if no override takes place
    // Card calls this function when used
    public virtual void DoSpellEffect(Minion caster, Minion target = null)
    {
        if (target == null)
            Debug.Log($"{caster.name} casts {_spellName} onto themselves.\n{_spellName}: {_spellDescription}");
        else
            Debug.Log($"{caster.name} casts {_spellName} onto {target.name}.\n{_spellName}: {_spellDescription}");
    }

    #endregion
}
