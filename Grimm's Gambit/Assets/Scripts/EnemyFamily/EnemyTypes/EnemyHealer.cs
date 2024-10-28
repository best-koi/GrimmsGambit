using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealer : EnemyTemplate
{
    [SerializeField]
    private int healingAmount;//An int representing an amount to heal by

    private EnemyTemplate enemyTarget;//A target for the enemy to heal

    [SerializeField]
    private List<EnemyTemplate> allies = new List<EnemyTemplate>();//An array of enemies to heal


    //Checks for available enemies and heals one
    private void FindEnemyToHeal()
    {
        //Gets array of enemies in scene
        EnemyTemplate[] enemies = CombatInventory.GetActiveEnemies();
        //Creates a sublist of non-self enemies
        foreach (EnemyTemplate e in enemies)
        {
            if (e == this)
                continue;
            allies.Add(e);
            Debug.Log(allies);
        }
        //Sets a random target to heal
        enemyTarget = allies[Random.Range(0, allies.Count)];

    }

    protected override void Update()
    {
        if(enemyTarget == null)
            FindEnemyToHeal();

        healthText.text = $"{hp}";
        nameText.text = name;

        switch (attacks[currentAttack])
        {
            case "HealOther":
                moveText.text = $"Upcoming Move: Heal {enemyTarget.GetEnemyName()}";
                moveText.color = enemyTarget.GetEnemyColor();
                break;
            case "HealSelf":
                moveText.text = "Upcoming Move: Heal Self";
                moveText.color = this.GetEnemyColor();
                break;
            default:
                moveText.text = "Upcoming Move: " + attacks[currentAttack];
                moveText.color = Color.white;
                break;
        }

    }

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
        FindEnemyToHeal();
        enemyTarget.AffectHP(healingAmount);
    }

    

     
}
