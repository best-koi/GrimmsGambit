using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Code by Jessie Archer
//A script representing an enemy that heals itself and other enemies
public class EnemyHealerRandom : EnemyRandomTarget
{
    [SerializeField]
    private int healingAmount;//An int representing an amount to heal by

    private EnemyTemplate enemyTarget;//A target for the enemy to heal

    [SerializeField]
    private List<EnemyTemplate> allies = new List<EnemyTemplate>();//An array of enemies to heal

    //Checks for available enemies and heals one
    private void FindEnemyToHeal()
    {
        //Breaks out of function if healer can not heal anyone
        if (!CheckHealOthers())
            return;

        allies = new List<EnemyTemplate>();
        //Gets array of enemies in scene
        EnemyTemplate[] enemies = CombatInventory.GetActiveEnemies();
        //Creates a sublist of non-self enemies
        foreach (EnemyTemplate e in enemies)
        {
            if (e == this || e.GetHP() == e.GetMaxHP())
                continue;
            allies.Add(e);
        }
        //Sets a random target to heal
        if (allies.Count != 0)
            enemyTarget = allies[Random.Range(0, allies.Count)];


    }

    public override void AttackPattern()
    {
        CheckCurrentAttack();
        base.AttackPattern();
    }

    //A method for the healer to heal itself
    private void HealSelf()
    {
        //Checks if enemy can heal itself
        //If number to heal exceeds, it become max HP
        if (minion.currentHealth + healingAmount < minion.maxHealth)
            minion.DamageTaken(healingAmount);
        else
            minion.currentHealth = minion.maxHealth;

    }

    //A method for the healer to heal other enemies
    //Perhaps reference a list of enemies and heal them
    //if there are no enemies, then skip to next attack
    private void HealOther()
    {
        if (enemyTarget.GetHP() + healingAmount < enemyTarget.GetMaxHP())
            minion.MinionUsed(enemyTarget.GetComponent<Minion>(), healingAmount);
        else
            enemyTarget.SetHP(enemyTarget.GetMaxHP());
        FindEnemyToHeal();
    }

    //Returns if enemy can heal itself
    private bool CheckSelfHeal()
    {
        if (minion.currentHealth == minion.maxHealth)
            return false;
        return true;
    }


    //Returns if enemy can heal any others
    //Bases this off of the current state of all enemies
    private bool CheckHealOthers()
    {
        //Retrieves current list of enemies and returns true if at least
        //one is capable of healing
        EnemyTemplate[] enemies = CombatInventory.GetActiveEnemies();
        foreach (EnemyTemplate e in enemies)
        {
            if (e.GetHP() < e.GetMaxHP())
                return true;
            else
                continue;
        }
        return false;
    }

    //Checks current attack
    protected override void CheckCurrentAttack()
    {
        switch (attacks[currentAttack])
        {
            //The function for healing another enemy
            case "HealOther":
                //Sets an enemy to heal (will not do anything if all full health)
                FindEnemyToHeal();
                if (enemyTarget == null)
                    AdvanceAttack();
                else{
                    moveText.text = $"Heal {enemyTarget.GetEnemyName()} for {-healingAmount} HP";
                    moveText.color = enemyTarget.GetEnemyColor();

        }
                break;
            case "HealSelf":
                if (!CheckSelfHeal())
                    AdvanceAttack();
                else
                {
                    moveText.text = $"Heal Self for {-healingAmount} HP";
                    moveText.color = this.GetEnemyColor();
                }
                break;

            case "Attack":
                if (attackTarget == null)
                    FindTarget();
                if (!CanAttackTarget())
                    AdvanceAttack();
                else
                {
                    moveText.text = $"Attack {attackTarget.GetCharacterName()} for {attackValue} DMG";
                    moveText.color = attackTarget.GetCharacterColor();

                }

                break;

            default:
                moveText.text = "Upcoming Move: " + attacks[currentAttack];
                moveText.color = Color.white;
                break;
        }


    }

    

}
