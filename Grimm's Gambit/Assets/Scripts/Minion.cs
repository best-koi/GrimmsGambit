using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum Affix //Insert affixes here that are applied from the minion perspective
{
    Taunt, //Callable for main gameplay loop context - second number could be for length of taunt if we want to implement that
    Block, //In the damage taken function - second number determines the amount of stacks
    Vulnerable, //In the damage taken function - second number determines amount of charges - aka duration (This is is "Armor Reduction" from the document)
    DamageReduction, //In the Minion Used function - second number determines number of stacks
    Thorns, //Completed in "Minion Used" function - second number determines thorns damage
    Regen, //Completed in "EndCurrentTurn" - second number ones digit determines turns remaining, /10 value determines amount of healing ex: 43 should be the initially entered value for 4 healing
    Parasite, //Completed in "Minion Used" function - second number determines parasite healing
    Strength, //Complete in "Minion Used" function - second number determines amount of stacks
    Bleed, //Complete in "Establish New Turn" function - second number determines amount of stacks
    Mark, //Complete in "Damage Taken" function - second number determines amount of stacks
    HoundCounter //Complete in "Minion Used" function - second number is irrelevant
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
    public int currentHealth;
        
    //Affixes:
    public Affixes[] appliedAffixes = new Affixes[1]; //Used for implementing preset affixes in the unity editor
    public Dictionary<Affix, int> currentAffixes = new Dictionary<Affix, int>(); //Used for recording currently applied affixes without need a foreach loop

    //Ownership:
    public bool ownerPlayer; //True for player, false for enemy
    private bool currentTurnPlayer; //This just records whose turn it is currently
    //Insert a reference to the game loop object here if there are "random" attacks so that a random target can be selected from the target options


    //Activity:
    private bool usedThisTurn;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Affixes affix in appliedAffixes)
        {
            AddAffix(affix.Tag, affix.Value); //Stores each affix that was preset into the currentAffixes dictionary
        }
        EncounterController.onTurnChanged += TurnStart; // Adjust this code for whatever action is invoked from the game loop
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddAffix(Affix affix, int value)
    {
        if (!currentAffixes.ContainsKey(affix)) //Adds affixes that are not currently present
        {
            currentAffixes.Add(affix, value);
        }
        else if (affix == Affix.Block || affix == Affix.Vulnerable || affix == Affix.DamageReduction || affix == Affix.Bleed || affix == Affix.Taunt) //For affixes that already exist but need a value added, replaces them by adding the current value to the new one
        {
            int currentValue = currentAffixes[affix];
            currentAffixes.Remove(affix);
            currentAffixes.Add(affix, currentValue+value);
        }
    }

    private void TurnStart(bool currentPlayer)
    {
        currentTurnPlayer = currentPlayer; //Sets current player given by new turn function to be the currentTurnPlayer value
        if (currentPlayer == ownerPlayer)
        {
            EstablishNewTurn(); //Runs function for when a player turn starts
        }
        else
        {
            EndCurrentTurn(); //Runs function for when a player turn ends
        }
    }

    private void EstablishNewTurn() //Function called when the signal is sent from the game logic that a new turn has started
    {
        usedThisTurn = false;       
        //Runs start of turn affixes when the player's turn starts
        if (currentAffixes.ContainsKey(Affix.Block)) //Removing of Block at the start of each new turn (so that when it is used, it can be impactful during the following enemy's turn)
        {
            currentAffixes.Remove(Affix.Block); //Removes block charges at the start of each new turn
        }
        if (currentAffixes.ContainsKey(Affix.Bleed)) //Removing of a Bleed charge at the start of each new turn
        {
            DamageTaken(currentAffixes[Affix.Bleed]); //Applies damage equal to the amount of charges
            RemoveOneCharge(Affix.Bleed); //Removes one charge
        } 
        if (currentAffixes.ContainsKey(Affix.Taunt)) //Removing of a Bleed charge at the start of each new turn
        {
            RemoveOneCharge(Affix.Taunt); //Removes one charge
        } 
    }

    private void EndCurrentTurn() //Function called when the signal is sent from the game logic that a turn has ended
    {
        //Runs functionality for end of turn affix adjustments when the player's turn ends
        if (currentAffixes.ContainsKey(Affix.Regen)) //Using one charge of Regen at the end of each turn
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
        if (currentAffixes.ContainsKey(Affix.Vulnerable)) //Removing one charge from Vulnerable at the end of each turn
        {
            RemoveOneCharge(Affix.Vulnerable);
        }
        if (currentAffixes.ContainsKey(Affix.DamageReduction)) //Removing one charge from Damage Reduction at the end of each turn
        {
            RemoveOneCharge(Affix.DamageReduction);
        }
        if (currentAffixes.ContainsKey(Affix.Mark)) //Removing one charge of mark at the end of each turn from the perspective of the character
        {
            RemoveOneCharge(Affix.Mark);
        }
        //End of turn effects/affixes go here
            
    }

    private void RemoveOneCharge(Affix affix) //Replacing affix with one charge less, as long as the remaining amount of charges is 2 or more
    {
        int currentCharges = currentAffixes[affix];
        currentAffixes.Remove(affix);
        if (currentCharges > 1)
        {
            currentAffixes.Add(affix, currentCharges-1);
        }
    }

    public bool CanBePlayed() //Function to be called from game logic to know if this minion can be used right now
    {
        return ownerPlayer == currentTurnPlayer && !usedThisTurn;
    }

    //For the following: make DamageToDeal be -10000 to use the default damage value
    public void MinionUsed(Minion enemyTarget, int Damage) //Function to be called when the game logic uses this minion - It is expected that this is used even for cards that make a minion attack, if that is a thing, so that parasite and thorns function correctly
    {
        int DamageToDeal = Damage;
        Minion targetMinion = enemyTarget;
        if (targetMinion != null)
        {   
            if (DamageToDeal > 0)
            {
                if (targetMinion.currentAffixes.ContainsKey(Affix.Thorns))
                {
                    DamageTaken(targetMinion.currentAffixes[Affix.Thorns]); //Deals damage equal to the stored thorns value to the one who performs an attack, by checking whether the targetted minion has thorns
                }
                if (targetMinion.currentAffixes.ContainsKey(Affix.Parasite))
                {
                    DamageTaken(-targetMinion.currentAffixes[Affix.Parasite]); //Deals healing equal to the stored parasite value to the one who performs an attack, by checking whether the targetted minion has thorns
                }
                if (currentAffixes.ContainsKey(Affix.Strength)) //Damage Modification for if character has a Strength modifier
                {
                    DamageToDeal += currentAffixes[Affix.Strength];
                    currentAffixes.Remove(Affix.Strength);
                }
                if (currentAffixes.ContainsKey(Affix.DamageReduction)) //Damage Modification for if damage reduction is applied
                {
                    DamageToDeal = (int) (DamageToDeal*.75); //Changes damage, reduced by 25 percent if damage reduction is currently applied
                }
                if (targetMinion.currentAffixes.ContainsKey(Affix.HoundCounter)) //Calculates Hounds counter after all other modifiers have been applied, since it is used on a multiplicative basis
                {
                    DamageTaken(DamageToDeal/2); //Deals have damage to the attacker during the condition of Hound's Counter
                }
            }  
            
            targetMinion.DamageTaken(DamageToDeal); //Deals written damage
        }
        //Add functionality here for minions that have an extra effect after being used
        usedThisTurn = true;
    }

    public void DamageTaken(int Damage) //Function called when damage is taken (Assumed logical order is mark->vulnerable->block)
    {
        int DamageToDeal = Damage;
        if (Damage > 0)
        {
            if (currentAffixes.ContainsKey(Affix.Mark)) //Condition for when the character is marked
            {
                DamageToDeal += 3; //Adds 3 damage if mark is applied
            }
            if (currentAffixes.ContainsKey(Affix.Vulnerable)) //Condition for when the character is vulnerable
            {
                DamageToDeal = (int) (DamageToDeal * 1.5); //Increases damage dealt by 50 percent, but then casts it to int
            }
            if (currentAffixes.ContainsKey(Affix.Block) && DamageToDeal > 0) //Condition for when the character has block charges prepared
            {
                int currentBlock = currentAffixes[Affix.Block];
                if (DamageToDeal < currentBlock) //Condition where block completely covers the damage to deal
                {
                    int remainingBlock = currentBlock - DamageToDeal;
                    DamageToDeal = 0;
                    currentAffixes.Remove(Affix.Block);
                    currentAffixes.Add(Affix.Block, remainingBlock); //Replaces block value with the new remaining value
                }
                else //Case where there isn't any block leftover
                {
                    DamageToDeal -= currentBlock;
                    currentAffixes.Remove(Affix.Block); //Removes block affix since all charges are used
                }
            }
        }
        
        currentHealth -= DamageToDeal;

        if (currentHealth < 0)
        {
            Destroyed(); 
        }
        else if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth; //Caps health to max out at the originally given max health value
        }
    }

    private void Destroyed() //Function for when this character has been defeated
    {
        //Implement This Later depending on game logic
        //Maybe have death effect here if present
        Deck deck = FindObjectOfType<Deck>();
        deck.RemoveCards(this);

        Destroy(gameObject);
    }
}
