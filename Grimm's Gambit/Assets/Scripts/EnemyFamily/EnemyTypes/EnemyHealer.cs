using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealer : EnemyTemplate
{
    [SerializeField]
    private int healingAmount;//An int representing an amount to heal by

    //A method for the healer to heal itself
    private void HealSelf()
    {
        hp += healingAmount;
    }

    //A method for the healer to heal other enemies
    //Perhaps reference a list of enemies and heal them
    //if there are no enemies, then skip to next attack
    private void HealOther(EnemyTemplate enemy)
    {
        enemy.AffectHP(healingAmount);
    }

     
}
