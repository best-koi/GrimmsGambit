using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Affixes //Insert affixes here that are applied from the minion perspective
{
    Basic,
    Default
}

public class Minion : MonoBehaviour
{
    //Health:
    public int maxHealth;
    public int damage;
    private int currentHealth;
    
    //Affixes:
    public List<Affixes> appliedAffixes = new List<Affixes>();

    //Ownership:
    public GameObject ownerCard;
    public GameObject ownerPlayer; //This one could possibly be an int? or enum?
    private GameObject currentTurnPlayer; //This has to be the same type as the owner Player, since this just records whose turn it is currently
    //Insert a reference to the game loop object here if there are "random" attacks so that a random target can be selected from the target options
    
    //Activity:
    private bool usedThisTurn;
    // Start is called before the first frame update
    void Start()
    {
        //GameData.NewTurn += EstablishNewTurn; - Adjust this code for whatever action is invoked from the game loop
        //GameData.EndTurn += EndCurrentTurn; - Adjust this code for whatever action is invoked from the game loop
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void EstablishNewTurn(GameObject currentPlayer) //Function called when the signal is sent from the game logic that a new turn has started
    {
        usedThisTurn = false;
    }

    private void EndCurrentTurn() //Function called when the signal is sent from the game logic taht a turn has ended
    {
        //End of turn effects/affixes go here
    }

    public bool CanBePlayed() //Function to be called from game logic to know if this minion can be used right now
    {
        return ownerPlayer == currentTurnPlayer && !usedThisTurn;
    }

    public void MinionUsed(GameObject enemyTarget) //Function to be called when the game logic uses this minion
    {
        Minion targetMinion = enemyTarget.GetComponent<Minion>();
        if (targetMinion != null)
        {
            targetMinion.DamageTaken(damage);
        }
        else //Insert logic for if enemy player is targetted here
        {

        }
        //Add functionality here for minions that have an extra effect after being used
        usedThisTurn = true;
    }

    public void DamageTaken(int Damage) //Function called when damage is taken
    {
        currentHealth -= Damage;
        if (Damage < 0)
        {
            Destroyed();
        }
    }

    private void Destroyed()
    {
        //Implement This Later depending on game logic
        //Maybe have death effect here if present
        Destroy(gameObject);
    }
}
