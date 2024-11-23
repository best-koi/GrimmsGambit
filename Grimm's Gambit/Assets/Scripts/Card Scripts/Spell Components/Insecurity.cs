using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Insecurity : SpellComponent
{
    public override void DoSpellEffect()
    {
        DamageSelf.ApplyEffect();
    }
}
