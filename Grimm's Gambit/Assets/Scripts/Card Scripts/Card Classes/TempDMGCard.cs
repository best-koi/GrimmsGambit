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
    }

    public TempDMGCard()
    {
        cardName = "Damage Card";
        cardCost = 2;
    }
}
