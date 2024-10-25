using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum Affix //Insert affixes here that are applied from the minion perspective
{
    Taunt, //Callable for main gameplay loop context - second number could be for length of taunt if we want to implement that
    Vulnerable, //In the damage taken function - second number determines the factor for a damage multiplier
    Thorns, //Completed in "Minion Used" function - second number determines thorns damage
    Regen, //Completed in "EndCurrentTurn" - second number ones digit determines turns remaining, /10 value determines amount of healing ex: 43 should be the initially entered value for 4 healing
    Parasite //Completed in "Minion Used" function - second number determines parasite healing
}

public class Affixes //Allows for the storing of values associated with each affix while using the editor (these values will get added into the dictionary upon game start)
{
    public Affix Tag; //Used to record what the tag of the affix is
    public int Value; //Used to record charges of the affix
}

public class Minion : MonoBehaviour
{
    //Health:
    public int maxHealth;
    public int damage;
    private int currentHealth;
        
    //Affixes:
    public Affixes[] appliedAffixes = new Affixes[1]; //Used for implementing preset affixes in the unity editor
    Dictionary<Affix, int> currentAffixes = new Dictionary<Affix, int>(); //Used for recording currently applied affixes without need a foreach loop

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
        foreach (Affixes affix in appliedAffixes)
        {
            if (!currentAffixes.ContainsKey(affix.Tag))
            {
                currentAffixes.Add(affix.Tag, affix.Value); //Stores each affix that was preset into the currentAffixes dictionary
            }
        }
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
        if (currentAffixes.ContainsKey(Affix.Regen))
        {
            int HealingToDeal = currentAffixes[Affix.Regen]/10;
            DamageTaken(-HealingToDeal); //Heals by the tens place of the regen value
            //Updating Value
            int onesPlace = currentAffixes[Affix.Regen]%10 - 1;
            currentAffixes.Remove(Affix.Regen); //Removes the regen affix (will re-add if needed)
            if (onesPlace > 0)
            {
                int newValue = HealingToDeal * 10 + onesPlace; //Multiplies the tens place by ten then adds the ones place back in
                currentAffixes.Add(Affix.Regen, newValue); //Reimplements the affix with the new value
            }
        }

        //End of turn effects/affixes go here
    }

    public bool CanBePlayed() //Function to be called from game logic to know if this minion can be used right now
    {
        return ownerPlayer == currentTurnPlayer && !usedThisTurn;
    }

    public void MinionUsed(GameObject enemyTarget) //Function to be called when the game logic uses this minion - It is expected that this is used even for cards that make a minion attack, if that is a thing, so that parasite and thorns function correctly
    {
        Minion targetMinion = enemyTarget.GetComponent<Minion>();
        if (targetMinion != null)
        {
            if (targetMinion.currentAffixes.ContainsKey(Affix.Thorns))
            {
                DamageTaken(targetMinion.currentAffixes[Affix.Thorns]); //Deals damage equal to the stored thorns value to the one who performs an attack, by checking whether the targetted minion has thorns
            }
            if (targetMinion.currentAffixes.ContainsKey(Affix.Parasite))
            {
                DamageTaken(-targetMinion.currentAffixes[Affix.Parasite]); //Deals healing equal to the stored parasite value to the one who performs an attack, by checking whether the targetted minion has thorns
            }
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
        if (currentAffixes.ContainsKey(Affix.Vulnerable))
        {
            currentHealth -= Damage * currentAffixes[Affix.Vulnerable]; //Deals damage multiplied by the amount of vulnerable factor
        }
        else
        {
            currentHealth -= Damage;
        }

        if (currentHealth < 0)
        {
            Destroyed();
        }
        else if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth; //Caps health to max out at the originally given max health value
        }
    }

    private void Destroyed()
    {
        //Implement This Later depending on game logic
        //Maybe have death effect here if present
        Destroy(gameObject);
    }
}
