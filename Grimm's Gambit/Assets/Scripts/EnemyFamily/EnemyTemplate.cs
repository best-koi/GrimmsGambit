using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//Code by Jessie Archer
public abstract class EnemyTemplate : MonoBehaviour
{
    [SerializeField]
    protected int hp;//A hit points stat for the enemy

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
    protected Renderer renderer;

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

    protected virtual void Update()
    {
        healthText.text = $"{hp}";
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

    protected void CheckAttackBounds()
    {
        if (currentAttack >= attacks.Count)
            currentAttack = 0;
    }

    public string GetEnemyName()
    {
        return enemyName;
    }

    public Color GetEnemyColor()
    {
        return enemyColor;
    }
}