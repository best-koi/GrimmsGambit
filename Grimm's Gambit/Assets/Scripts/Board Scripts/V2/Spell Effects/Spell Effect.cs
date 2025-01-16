using UnityEngine;

[System.Serializable]
public abstract class SpellEffect
{
    #region Private Fields

    protected string _spellName;
    protected string _spellDescription;
    protected bool _requiresTarget = false;

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
    public abstract void DoSpellEffect(Minion caster, Minion target);

    #endregion

    protected void DebugLogEffect(Minion caster, Minion target)
    {
        Debug.Log($"{caster.name} casts {_spellName} onto {target.name}.\n{_spellName}: {_spellDescription}");
    }
}


