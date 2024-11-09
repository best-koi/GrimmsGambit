using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRandomTarget : EnemyTemplate
{
    protected CharacterTemplate attackTarget;//A character to attack

    protected List<CharacterTemplate> targets = new List<CharacterTemplate>();//a list of all available targets, for random attacks

    protected EncounterController controller;


    protected override void Start()
    {
        controller = FindObjectOfType(typeof(EncounterController)) as EncounterController;
        base.Start();

    }
    //An Attack method to be used for honing in on specific positioned players
    protected override void Attack()
    {
      
       minion.MinionUsed(attackTarget.GetComponent<Minion>(), attackValue);
       FindTarget();
        
       
    }

    protected virtual void FindTarget()
    {
        GetAllActiveCharacters();
        attackTarget = targets[Random.Range(0, targets.Count)];

    }

    //Checks if enemy is capable of attacking
    protected override bool CanAttackTarget()
    {
        if (attackTarget.GetHP() <= 0)
            return false;

        return true;
    }

    //Gets a list of all active characters from CombatInventory
    protected void GetAllActiveCharacters()
    {
        List<GameObject> characters = controller.GetPlayerInventory().GetAllMembers();
        foreach (GameObject c in characters)
        {
            if (c.GetComponent<CharacterTemplate>().GetHP() <= 0)
                continue;
            targets.Add(c.GetComponent<CharacterTemplate>());
        }
    }

    //Adjusts CurrentAttack() for finding a random target 
    protected override void CheckCurrentAttack()
    {
        switch (attacks[currentAttack])
        {
            case "Attack":
                if (attackTarget == null)
                    FindTarget();
                

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
            case "RandomAttack":
                moveText.text = $"{randomAttackName}";
                moveText.color = this.GetEnemyColor();
                break;
                

            default:
                moveText.text = "Upcoming Move: " + attacks[currentAttack];
                moveText.color = Color.white;
                break;
        }


    }



}
