using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Code by Jessie Archer
public abstract class EnemyTemplate : MonoBehaviour
{
    [SerializeField]
    protected int hp;//A hit points stat for the enemy

    [SerializeField]
    protected string enemyName;//The enemy's name

    [SerializeField]
    protected List<System.Action> attacks;//A list of methods for attacks

    [SerializeField]
    protected int currentAttack;//The index representing the current attack

    //AttackPattern() essentially calls the next attack from the list
    //Once the attack is done, it advances to the next attack in the pattern
    //Also, checks for going out of bounds
    protected virtual void AttackPattern()
    {
        if (currentAttack >= attacks.Count)
            currentAttack = 0;

        //Calls a method from the list of available attacks
        attacks[currentAttack]();
        //Moves onto the next attack
        currentAttack++;

        
    }

}