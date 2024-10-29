using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Code by Jessie Archer
//A script representing an enemy that heals itself and other enemies
public class EnemyHealer : EnemyTemplate
{
    [SerializeField]
    private int healingAmount;//An int representing an amount to heal by

    private EnemyTemplate enemyTarget;//A target for the enemy to heal

    [SerializeField]
    private List<EnemyTemplate> allies = new List<EnemyTemplate>();//An array of enemies to heal

    //The Healer Update() method considers how to update text in response
    //to what action is being performed
    protected override void Update()
    {

        healthText.text = $"{hp}/ {maxHP}";
        nameText.text = enemyName;
        switch (attacks[currentAttack])
        {
            //The function for healing another enemy
            case "HealOther":
                //Sets an enemy to heal (will not do anything if all full health)
                if(enemyTarget == null)
                    FindEnemyToHeal();
                if (!CheckHealOthers())
                    AdvanceAttack();
                else
                {
                    moveText.text = $"Upcoming Move: Heal {enemyTarget.GetEnemyName()}";
                    moveText.color = enemyTarget.GetEnemyColor();

                }
                break;
            case "HealSelf":
                if (!CheckSelfHeal())
                    AdvanceAttack();
                else
                {
                    moveText.text = "Upcoming Move: Heal Self";
                    moveText.color = this.GetEnemyColor();
                }
                break;
            default:
                moveText.text = "Upcoming Move: " + attacks[currentAttack];
                moveText.color = Color.white;
                break;
        }

    }

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



    //A method for the healer to heal itself
    private void HealSelf()
    {
        //Checks if enemy can heal itself
        //If number to heal exceeds, it become max HP
        if (hp + healingAmount < maxHP)
            hp += healingAmount;
        else 
            hp = maxHP;

    }

    //A method for the healer to heal other enemies
    //Perhaps reference a list of enemies and heal them
    //if there are no enemies, then skip to next attack
    private void HealOther()
    {
        if (enemyTarget.GetHP() + healingAmount < enemyTarget.GetMaxHP())
            enemyTarget.AffectHP(healingAmount);
        else
            enemyTarget.SetHP(enemyTarget.GetMaxHP());
        FindEnemyToHeal();
    }

    //Returns if enemy can heal itself
    private bool CheckSelfHeal()
    {
        if (hp == maxHP)
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

    

}
