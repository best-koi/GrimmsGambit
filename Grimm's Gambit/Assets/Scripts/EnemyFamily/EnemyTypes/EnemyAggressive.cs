using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAggressive : EnemyTemplate
{
    //An Attack method to be used for honing in on specific positioned players
    protected override void Attack()
    {
        if(hasPositionTarget == false)
        {
            minion.MinionUsed(attackTarget.GetComponent<Minion>(), attackValue);
            FindTarget();
        }
        else
        {
            FindPositionedTarget(position);
            minion.MinionUsed(positionTarget.GetComponent<Minion>(), attackValue);

        }
        
    }

    //A version of the CheckCurrentAttack(), which takes into consideration position of players
    protected override void CheckCurrentAttack()
    {
        switch (attacks[currentAttack])
        {
            case "Attack":

                if (hasPositionTarget == false)
                    FindTarget();
                else
                    FindPositionedTarget(position);

                if (!CanAttackTarget())
                    AdvanceAttack();
                else
                {
                    if (!hasPositionTarget)
                    {
                        moveText.text = $"Upcoming Move: Attack {attackTarget.GetCharacterName()}";
                        moveText.color = attackTarget.GetCharacterColor();
                    }
                    else
                    {
                        moveText.text = $"Upcoming Move: Attack {positionTarget.GetCharacterName()}";
                        moveText.color = positionTarget.GetCharacterColor();

                    }
                    

                }

                break;

            default:
                moveText.text = "Upcoming Move: " + attacks[currentAttack];
                moveText.color = Color.white;
                break;
        }


    }




}
