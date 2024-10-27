using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealer : EnemyTemplate
{
    [SerializeField]
    private int healingAmount;//An int representing an amount to heal by

    private EnemyTemplate enemyTarget;//A target for the enemy to heal

    //A method for the healer to heal itself
    private void HealSelf()
    {
        hp += healingAmount;
    }

    //A method for the healer to heal other enemies
    //Perhaps reference a list of enemies and heal them
    //if there are no enemies, then skip to next attack
    private void HealOther()
    {

        enemyTarget.AffectHP(healingAmount);
    }


    private void FindEnemyToHeal()
    {

    }

     
}
