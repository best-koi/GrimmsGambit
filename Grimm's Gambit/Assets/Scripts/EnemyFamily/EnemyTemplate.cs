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

    protected virtual void Start()
    {
        //Sets color to preset color
        renderer.material.color = enemyColor;
    }

    //Shows the default text above and below enemy
    protected virtual void Update()
    {
        healthText.text = $"{hp}/ {maxHP}";
        nameText.text = name;
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

    public int GetHP()
    {
        return hp;
    }

    public int GetMaxHP()
    {
        return maxHP; 
    }
}