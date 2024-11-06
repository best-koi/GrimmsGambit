using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Inherit from this class if the enemy targets a specific player position
//For example, the Berserker targets the front position and moves backward

public class EnemyPositionTarget : EnemyTemplate
{
    [SerializeField]    protected CharacterTemplate positionTarget;//A character to target based on position, if necessary

    protected List<CharacterTemplate> orderedCharacters = new List<CharacterTemplate>();//A list of ordered characters 

    [SerializeField]
    protected int position;//A position to search for to attack

    protected EncounterController controller;

    //Orders characters at the beginning 
    protected override void Start()
    {
        controller = FindObjectOfType(typeof(EncounterController)) as EncounterController;
        base.Start();
        OrderCharacters();
        //FindPositionedTarget(position);
    }

    //A way to get characters in an order by position
    //From 1 to 3 (front to back)
    protected void OrderCharacters()
    {
        List<GameObject> characters = controller.GetPlayerInventory().GetAllMembers();
        for (int i = 1; i < 4; i++)
        {
            foreach (GameObject c in characters)
            {
                if (c.GetComponent<CharacterTemplate>().GetCharacterPosition() == i)
                {
                    orderedCharacters.Add(c.GetComponent<CharacterTemplate>());
                    break;

                }

            }

        }

    }

    //Looks for a target, given a numerical position
    //1 - front; 2 - middle; 3 - back
    //Saves this target to attack
    protected virtual void FindPositionedTarget(int p)
    {
        List<GameObject> characters = controller.GetPlayerInventory().GetAllMembers();
        foreach (GameObject c in characters)
        {
            if (c != null && c.TryGetComponent<CharacterTemplate>(out CharacterTemplate ct) && ct.GetCharacterPosition() == p)
            {
                positionTarget = c.GetComponent<CharacterTemplate>();
                return;
            }
        }
    }


    //An Attack method to be used for honing in on specific positioned players
    protected override void Attack()
    {
      
            FindPositionedTarget(position);
            if (positionTarget == null)
                SeekNewTargetInOrder();
            else
                minion.MinionUsed(positionTarget.GetComponent<Minion>(), attackValue);

        

    }

    //Checks if the original position's character was destroyed
    //If it was, move onto the next and return true.
    //If no one is left, return false
    protected override bool CanAttackTarget()
    {
        if (positionTarget == null) //positionTarget.GetDestroyed())
        {
            return SeekNewTargetInOrder();

        }

        return true;
    }

    
    //Finds a new target and returns a value based on whether a new target was found
    //Skips an enemy who is the same as the original position or has no hp
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

    //Adapts attacking pattern to find a positioned target.
    //Each string represents a move. 
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
                   //Text display for buffs; second if is for non-buff
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
