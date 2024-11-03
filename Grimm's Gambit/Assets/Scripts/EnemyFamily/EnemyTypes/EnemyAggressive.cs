using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAggressive : EnemyTemplate
{
    [SerializeField]
    private int buffValue;//A value to apply a buff by

    private List<CharacterTemplate> orderedCharacters = new List<CharacterTemplate>();//A list of ordered characters 

    protected override void Start()
    {
        base.Start();
        OrderCharacters();
    }

    //A way to get characters in an order by position
    //From 1 to 3 (front to back)
    private void OrderCharacters()
    {
        GetAllActiveCharacters();
        for(int i = 1; i < 4; i++)
        {
            foreach(CharacterTemplate c in targets)
            {
                if (c.GetCharacterPosition() == i)
                {
                    orderedCharacters.Add(c);
                    break;

                }
                    
            }

        }
        
    }

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
            if(!positionTarget.GetDestroyed())
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
                {
                    if(!SeekNewTargetInOrder())
                    {
                        moveText.text = "Done Acting.";

                    }
                    else
                        AdvanceAttack();
                }
                else
                {
                    if (!hasPositionTarget)
                    {
                        moveText.text = $"Attack {attackTarget.GetCharacterName()} for {attackValue} DMG";
                        moveText.color = attackTarget.GetCharacterColor();
                    }
                    else
                    {
                        moveText.text = $"Attack {positionTarget.GetCharacterName()} for {attackValue} DMG";
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

    protected override bool CanAttackTarget()
    {
        if (hasPositionTarget && positionTarget.GetDestroyed())
        {
            return SeekNewTargetInOrder();

        }
        else
        {
            if (attackTarget.GetHP() <= 0)
                return false;

        }

        return true;
    }

    //Adds a Strength Affix to the enemy
    private void Strength()
    {
        minion.AddAffix(Affix.Strength, buffValue);
    }

    //Finds a new target and returns a value based on whether a new target was found
    private bool SeekNewTargetInOrder()
    {
        foreach (CharacterTemplate c in orderedCharacters)
        {
            if (c.GetCharacterPosition() == position)
                continue;
            else if (c.GetHP() > 0)
            {
                position = c.GetCharacterPosition();
                positionTarget = c;
                return true;
            }
            else
            {
                continue;
            }
        }

        return false;
    }


}
