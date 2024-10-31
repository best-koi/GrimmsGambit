using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DamageMinion))]
public class TempDMGCard : Card
{
    private DamageMinion dmgMin;
    void Start()
    {
        dmgMin = GetComponent<DamageMinion>();
        dmgMin.SetDamage(4);
    }

    public TempDMGCard()
    {
        cardName = "Damage Card";
        cardCost = 2;
    }
}
