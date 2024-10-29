using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//Code by Jessie Archer
public abstract class EnemyTemplate : MonoBehaviour
{
    [SerializeField]
    protected int hp, maxHP;//Hit points stat for the enemy

    [SerializeField]
    protected string enemyName;//The enemy's name

    [SerializeField]
    protected List<string> attacks;//A list of strings mapped to methods

    [SerializeField]
    protected int currentAttack;//The index representing the current attack

    [SerializeField]
    protected TMP_Text healthText, nameText, moveText;//Text to indicate the enemy's health

    [SerializeField]
    protected Color enemyColor;//A color that represents the enemy

    [SerializeField]
    protected Renderer renderer;//The enemy's renderer

    [SerializeField]
    protected int attackValue;//The enemy's attack value

    //AttackPattern() essentially calls the next attack from the list
    //Once the attack is done, it advances to the next attack in the pattern
    //Also, checks for going out of bounds
    protected virtual void AttackPattern()
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

    //Shows the default text above and below enemy
    protected virtual void Update()
    {
        healthText.text = $"{hp}/ {maxHP}";
        nameText.text = enemyName;
        moveText.text = "Upcoming Move: " + attacks[currentAttack];
    }

    //Sets the enemy hp
    //Put in positive number to increase
    //Put in negative number to decrease
    public void AffectHP(int amount)
    {
        hp += amount;
    }

    //sets HP to a fixed amount
    public void SetHP(int amount)
    {
        hp = amount; 
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
        Debug.Log("Attacked!");
    }

    //A method representing a defensive move
    protected virtual void Defend()
    {
        Debug.Log("Defending!");
    }

    //Testing AttackPattern
    protected void OnMouseDown()
    {
        AttackPattern();
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
        return hp;
    }

    //Returns the Enemy's Max Health
    public int GetMaxHP()
    {
        return maxHP; 
    }
}