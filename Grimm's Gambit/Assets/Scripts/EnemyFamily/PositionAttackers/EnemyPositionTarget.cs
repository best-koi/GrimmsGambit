using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPositionTarget : EnemyTemplate
{
    protected CharacterTemplate positionTarget;//A character to target based on position, if necessary

    protected List<CharacterTemplate> orderedCharacters = new List<CharacterTemplate>();//A list of ordered characters 

    [SerializeField]
    protected int position;//A position to search for to attack

    //Looks for a target, given a numerical position
    //1 - front; 2 - middle; 3 - back
    //Saves this target to attack
    protected virtual void FindPositionedTarget(int p)
    {
        CharacterTemplate[] characters = CombatInventory.GetActiveCharacters();
        foreach (CharacterTemplate c in characters)
        {
            if (c.GetCharacterPosition() == p)
            {
                positionTarget = c;
                return;
            }

        }
    }


    //An Attack method to be used for honing in on specific positioned players
    protected override void Attack()
    {
      
            FindPositionedTarget(position);
            if (!positionTarget.GetDestroyed())
                minion.MinionUsed(positionTarget.GetComponent<Minion>(), attackValue);

        

    }


    protected override bool CanAttackTarget()
    {
        if (positionTarget.GetDestroyed())
        {
            return SeekNewTargetInOrder();

        }

        return true;
    }


    //Finds a new target and returns a value based on whether a new target was found
    protected bool SeekNewTargetInOrder()
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

    protected override void CheckCurrentAttack()
    {
        switch (attacks[currentAttack])
        {
            case "Attack":
                if (positionTarget == null)
                    FindPositionedTarget(position);

                if (!CanAttackTarget())
                {
                    if (attacks.Count == 1)
                        moveText.text = "Done Acting.";
                    else
                        AdvanceAttack();
                }
                else
                {
                   
                        if (minion.currentAffixes.ContainsKey(Affix.Strength))
                            moveText.text = $"Attack {positionTarget.GetCharacterName()} for {attackValue + buffValue} DMG";
                        else
                            moveText.text = $"Attack {positionTarget.GetCharacterName()} for {attackValue} DMG";
                        moveText.color = positionTarget.GetCharacterColor();

                }

                break;

            case "Strength":
                moveText.text = $"Applying {buffValue} Strength to Self";
                moveText.color = this.GetEnemyColor();
                break;

            default:
                moveText.text = "Upcoming Move: " + attacks[currentAttack];
                moveText.color = Color.white;
                break;
        }


    }

}
