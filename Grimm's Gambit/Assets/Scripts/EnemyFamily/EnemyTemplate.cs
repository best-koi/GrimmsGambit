using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//Code by Jessie Archer
public abstract class EnemyTemplate : MonoBehaviour
{
    [SerializeField]
    protected Minion minion;//A minion assigned to this enemy

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

    [SerializeField]
    protected int buffValue;//A value to apply a buff by

    [SerializeField]
    protected int blockValue;//A value to apply block

    [SerializeField]
protected List<string> randomAttacks;//A list of random attacks to pull from (for RandomAttack() method)

[SerializeField]
protected List<string> combinedAttacks;//A list of random attacks to pull from (for RandomAttack() method)

protected CharacterTemplate attackTarget;//A character to attack

protected string randomAttackName;//The name of the planned random attack

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


    //A default Start() method 
    protected virtual void Start()
    {
        //Sets color to preset color
        renderer.material.color = enemyColor;
        //Starts the enemy with a random attack
        currentAttack = Random.Range(0, attacks.Count);
    }

    //Sets the enemy's color 
    public void SetColor(Color c)
    {
        enemyColor = c;
    }

    //Shows the default text above and below enemy
    protected virtual void Update()
    {
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
    protected abstract void Attack();

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

    //A method for a strength buff
    protected void Strength()
    {
        minion.AddAffix(Affix.Strength, buffValue);
    }

    protected void Block()
    {
        minion.AddAffix(Affix.Block, blockValue);
    }

    //An abstract method for attacking targets (to be implemented by children)
    protected abstract bool CanAttackTarget();

    //A method for checking attacks; has a basic functionality for now, but
    //is meant to be defined by children 
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

//Randomly attacks based on attacks in randomAttacks list. 
protected void RandomAttack(){
        string plannedAttack = randomAttacks[Random.Range(0, randomAttacks.Count)];
        randomAttackName = plannedAttack;
        Invoke(plannedAttack, 0f);
    }

//Executes multiple moves at once
    protected void CombinedAttack(){
        foreach(string s in combinedAttacks)
            Invoke(s, 0f);
    }

//Returns the character who is being targeted
protected Minion GetAttackTarget(){
    return attackTarget.GetComponent<Minion>();
}

    




}