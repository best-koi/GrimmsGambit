using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//Code by Jessie Archer
public abstract class EnemyTemplate : MonoBehaviour
{
    [SerializeField]
    protected Minion minion;

    [SerializeField]
    protected string enemyName;//The enemy's name

    [SerializeField]
    protected List<string> attacks;//A list of strings mapped to methods

    [SerializeField]
    protected int currentAttack;//The index representing the current attack

    [SerializeField]
    protected TMP_Text healthText, nameText, moveText;//Text to indicate the enemy's health

    protected Color enemyColor;//A color that represents the enemy

    [SerializeField]
    protected Renderer renderer;//The enemy's renderer

    [SerializeField]
    protected int attackValue;//The enemy's attack value

    protected CharacterTemplate attackTarget;//A character to attack

    protected List<CharacterTemplate> targets = new List<CharacterTemplate>();//a list of all available targets, for random attacks


    [SerializeField]
    protected bool hasPositionTarget;//A boolean used to determine if this enemy only locks onto one type of player, based on position

    protected CharacterTemplate positionTarget;//A character to target based on position, if necessary

    [SerializeField]
    protected int position;//A position to search for to attack


    //AttackPattern() essentially calls the next attack from the list
    //Once the attack is done, it advances to the next attack in the pattern
    //Also, checks for going out of bounds
    public virtual void AttackPattern()
    {
        CheckAttackBounds();
        //Calls a method from the list of available attacks
        Invoke(attacks[currentAttack], 0f);
        //Moves onto the next attack
        currentAttack++;
        CheckAttackBounds();
    }

    protected virtual void Act()
    {
        AttackPattern();
    }

    protected virtual void FindTarget()
    {
        GetAllActiveCharacters();
        attackTarget = targets[Random.Range(0, targets.Count)];
        
    }

    protected void GetAllActiveCharacters()
    {
        CharacterTemplate[] characters = CombatInventory.GetActiveCharacters();
        foreach (CharacterTemplate c in characters)
        {
            if (c.GetHP() <= 0)
                continue;
            targets.Add(c);
        }
    }

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



    //A default Start() method 
    protected virtual void Start()
    {
        //Sets color to preset color
        renderer.material.color = enemyColor;
        //Starts the enemy with a random attack
        currentAttack = Random.Range(0, attacks.Count);
    }

    public void SetColor(Color c)
    {
        enemyColor = c;
    }

    //Shows the default text above and below enemy

    protected virtual void Update()
    {
        if (attackTarget == null)
            FindTarget();
        healthText.text = $"{minion.currentHealth}/ {minion.maxHealth}";
        nameText.text = enemyName;
        CheckCurrentAttack();

    }

    //Sets the enemy hp
    //Put in positive number to increase
    //Put in negative number to decrease
    public void AffectHP(int amount)
    {
        minion.currentHealth += amount;
    }

    //sets HP to a fixed amount
    public void SetHP(int amount)
    {
        minion.currentHealth = amount; 
    }

    //Advances past the current attack
    //Ensures that this attack is not out of the attack pattern bounds
    protected void AdvanceAttack()
    {
        currentAttack++;
        CheckAttackBounds();
    }

    //A method representing an attack
    protected virtual void Attack()
    {
        minion.MinionUsed(attackTarget.GetComponent<Minion>(), attackValue);
        FindTarget();
    }

    //A method representing a defensive move
    protected virtual void Defend()
    {
        Debug.Log("Defending!");
    }


    //Helps prevent Index Out of Bounds Errors
    protected void CheckAttackBounds()
    {
        if (currentAttack >= attacks.Count)
            currentAttack = 0;
    }

    //Returns the Enemy's Name
    public string GetEnemyName()
    {
        return enemyName;
    }

    //Returns the Enemy's Color (for Intent)
    public Color GetEnemyColor()
    {
        return enemyColor;
    }

    //Returns the Enemy's Current Health
    public int GetHP()
    {
        return minion.currentHealth;
    }

    //Returns the Enemy's Max Health
    public int GetMaxHP()
    {
        return minion.maxHealth; 
    }

    protected virtual bool CanAttackTarget()
    {
        if (hasPositionTarget)
        {
            if (positionTarget.GetHP() <= 0)
                return false;

        }
        else
        {
            if (attackTarget.GetHP() <= 0)
                return false;

        }
        
        return true;
    }

    protected virtual void CheckCurrentAttack()
    {
        switch (attacks[currentAttack])
        {
            default:
                moveText.text = "Upcoming Move: " + attacks[currentAttack];
                moveText.color = Color.white;
                break;
        }


    }


    




}