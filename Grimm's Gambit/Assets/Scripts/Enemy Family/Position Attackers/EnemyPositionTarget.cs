using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Inherit from this class if the enemy targets a specific player position
//For example, the Berserker targets the front position and moves backward

public class EnemyPositionTarget : EnemyTemplate
{

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
        List<Transform> characters = controller.GetPlayerInventory().GetAll();
        for (int i = 1; i < 4; i++)
        {
            foreach (Transform c in characters)
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
        List<Transform> characters = controller.GetPlayerInventory().GetAll();
        foreach (Transform c in characters)
        {
            if (c != null && c.gameObject.TryGetComponent<CharacterTemplate>(out CharacterTemplate ct)){
            if(ct.GetComponent<Minion>().currentAffixes.ContainsKey(Affix.Taunt)){
                attackTarget = ct;
                return;

            }
            else if(ct.GetCharacterPosition() == p)
            {
                attackTarget = ct;
                return;
            }
            }
        }
    }


    //An Attack method to be used for honing in on specific positioned players
    protected override void Attack()
    {
        FindPositionedTarget(position);
        if (attackTarget == null)
            SeekNewTargetInOrder();
        else
            minion.MinionUsed(attackTarget.GetComponent<Minion>(), attackValue);
    }

    //Checks if the original position's character was destroyed
    //If it was, move onto the next and return true.
    //If no one is left, return false
    protected override bool CanAttackTarget()
    {
        if (attackTarget == null) //positionTarget.GetDestroyed())
        {
            return SeekNewTargetInOrder();
        }

        return true;
    }

    
    //Finds a new target and returns a value based on whether a new target was found
    //Skips an enemy who is the same as the original position or has no hp
    protected virtual bool SeekNewTargetInOrder()
    {
        foreach (CharacterTemplate c in orderedCharacters)
        {
            if (c.GetCharacterPosition() == position)
                continue;
            else if (c.GetHP() > 0)
            {
                position = c.GetCharacterPosition();
                attackTarget = c;
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
                if (attackTarget == null)
                    FindPositionedTarget(position);

                if (!CanAttackTarget())
                {
                    if (orderedCharacters.Count == 0)
                        moveText.text = "Done Acting.";
                    else
                        AdvanceAttack();
                }
                else
                {
                   //Text display for buffs; second if is for non-buff
                        if (minion.currentAffixes.ContainsKey(Affix.Strength))
                            moveText.text = $"Attack {attackTarget.GetCharacterName()} for {attackValue + buffValue} DMG";
                        else
                            moveText.text = $"Attack {attackTarget.GetCharacterName()} for {attackValue} DMG";
                        moveText.color = attackTarget.GetCharacterColor();

                }

                break;

            case "Strength":
                moveText.text = $"Applying {buffValue} Strength to Self";
                moveText.color = this.GetEnemyColor();
                break;
            case "Block":
                moveText.text = $"Blocking for {blockValue}";
                moveText.color = this.GetEnemyColor();
                break;

            case "CombinedAttack":
                string display = "Planning to ";
                for(int i = 0; i < combinedAttacks.Count; i++){
                    if(i == combinedAttacks.Count - 1)
                        display += $"{combinedAttacks[i]}";
                    else
                        display += $"{combinedAttacks[i]} and ";

                }
                    
                moveText.text = display; 
                moveText.color = this.GetEnemyColor();
                break;

            default:
                moveText.text = "Upcoming Move: " + attacks[currentAttack];
                moveText.color = Color.white;
                break;
        }


    }

}
