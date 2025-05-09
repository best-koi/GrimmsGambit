//Ryan Lockie - Updated 11/16/2024
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

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
    HoundCounter, //Complete in "Minion Used" function - second number is irrelevant
    Threaded, //Complete in "Minion Used" function - second number is turn duration of effect (extra stacks will not increase this value)
    Naturopath, //Complete in "Damage Taken" function - second number is value to add to the heal
    Exploit, //Complete in "Damage Taken" function - second number is value of stacks
    Curse, //Debuff that does scaling damage starting at 3
    PowerBurst //Buff, Stackable, Strength but temporary
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

    [SerializeField]
    private GameObject m_CardContainer;

    public AffixDisplay affixDisplay;

    public Animator animator;

        
    //Affixes:
    public bool DirtInEyes = false; //This is not used as an affix because it will have no visual component - set to true in order to track a minion who is effected by the "dirt in your eyes card"

    [SerializeField] 
    public Dictionary<Affix, int> currentAffixes = new Dictionary<Affix, int>(); //Used for recording currently applied affixes without need a foreach loop

    //Ownership:
    public bool ownerPlayer; //True for player, false for enemy
    private bool currentTurnPlayer; //This just records whose turn it is currently
    //Insert a reference to the game loop object here if there are "random" attacks so that a random target can be selected from the target options

    //Activity:
    private bool usedThisTurn;
    public static Action<Minion> onDeath; //Added by Dawson for Check target death
    public static Action damageDealt; //Called every time a character deals damage in order to manage the dirt in eyes effect

    // Start is called before the first frame update
    void Start()
    {
        /*
        foreach (Affixes affix in appliedAffixes)
        {
            AddAffix(affix.Tag, affix.Value); //Stores each affix that was preset into the currentAffixes dictionary
        }
        */
        affixDisplay = GetComponentInChildren<AffixDisplay>(); //Initializes correct affixDisplay object
        EncounterController.onTurnChanged += TurnStart; // Adjust this code for whatever action is invoked from the game loop
        Minion.damageDealt += DirtReception;
    }

    public void AddAffix(Affix affix, int value)
    {
        UnityEngine.Debug.Log("Affix Adding");
        //The following code is for the analysis of the Carnation heirloom:
        HeirloomManager heirloomManager = FindObjectOfType<HeirloomManager>();
        if (heirloomManager.ContainsHeirloom(Heirloom.Carnation) && !ownerPlayer && (affix == Affix.Vulnerable || affix == Affix.DamageReduction || affix == Affix.Bleed || affix == Affix.Mark || affix == Affix.Threaded || affix == Affix.Exploit || affix == Affix.Curse))
        {
            value++; //Increments stacks if minion is an enemy, the player has the carnation, and the affix is a debuff
        }

        if (affix == Affix.Block && animator != null) //Plays block anim if Block affix is called 
        {
            animator.SetTrigger("Defend");
        }

        if (!currentAffixes.ContainsKey(affix)) //Adds affixes that are not currently present
        {
            UnityEngine.Debug.Log("Added " + affix + " at value " + value);
            currentAffixes.Add(affix, value);
            affixDisplay.AddAffix(affix, value); //Adds visual display of affix
            if (affix == Affix.Bleed && animator != null) //Plays Bleeding anim if Bleed affix is called 
            {
                animator.SetTrigger("IsBleeding");
            }
        }
        else if (affix == Affix.Block || affix == Affix.Vulnerable || affix == Affix.DamageReduction || affix == Affix.Bleed || affix == Affix.Taunt || affix == Affix.Naturopath || affix == Affix.Exploit || affix == Affix.PowerBurst) //For affixes that already exist but need a value added, replaces them by adding the current value to the new one
        {
            int currentValue = currentAffixes[affix];
            currentAffixes.Remove(affix);
            if (currentValue + value > 0 || (currentValue + value < 0 && affix != Affix.Bleed)) //Allows for this function to have negative values for stacking affixes - bleed is allowed to have negative stacks though
            {
                currentAffixes.Add(affix, currentValue+value);
                affixDisplay.UpdateStacks(affix, currentValue+value); //Updates count
            }
        }
    }

    private void TurnStart(bool currentPlayer)
    {
        if (gameObject != null) //Prevents minion from being accessed if game object is null
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
    }

    private void EstablishNewTurn() //Function called when the signal is sent from the game logic that a new turn has started
    {
        DirtInEyes = false; //Ends the dirt in eyes effect at turn beginning from the perspective of the one who had the card casted on them
        usedThisTurn = false;       
        //Runs start of turn affixes when the player's turn starts
        if (currentAffixes.ContainsKey(Affix.Block)) //Removing of Block at the start of each new turn (so that when it is used, it can be impactful during the following enemy's turn)
        {
            RemoveAffix(Affix.Block); //Removes block charges at the start of each new turn
        }
        if (currentAffixes.ContainsKey(Affix.Bleed)) //Removing of a Bleed charge at the start of each new turn
        {
            int currentBleed = currentAffixes[Affix.Bleed];
            if (currentBleed > 0) //If bleed stacks are negative, it works as a bleed shield
            {
                DamageTaken(currentBleed); //Applies damage equal to the amount of charges
                RemoveOneCharge(Affix.Bleed); //Removes one charge
            }
            if (currentBleed == 0 && animator != null) // Disable bleed if charges are depleted
            {
                animator.SetTrigger("BleedStop");
            }
        }
        if (currentAffixes.ContainsKey(Affix.Taunt)) //Removing of a Bleed charge at the start of each new turn
        {
            RemoveOneCharge(Affix.Taunt); //Removes one charge
        } 
        if (currentAffixes.ContainsKey(Affix.Curse))
        {
            int currentCurse = currentAffixes[Affix.Curse];
            currentAffixes.Remove(Affix.Curse);
            currentAffixes.Add(Affix.Curse, currentCurse+1);
            affixDisplay.UpdateStacks(Affix.Curse, currentCurse+1); //Updates count
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
                affixDisplay.UpdateStacks(Affix.Regen, newValue); //Updates count
            }
            else
            {
                affixDisplay.RemoveAffix(Affix.Regen); //Removes affix from display is replacement doesn't occur
            }
        }
        if (currentAffixes.ContainsKey(Affix.Curse))
        {
            DamageTaken(currentAffixes[Affix.Curse]); //Deals current curse dmg
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
        if (currentAffixes.ContainsKey(Affix.Threaded)) //Removing threaded at turn end
        {
            RemoveOneCharge(Affix.Threaded);
        }
        if (currentAffixes.ContainsKey(Affix.PowerBurst)) //Removing PowerBurst charge
        {
            RemoveOneCharge(Affix.PowerBurst);
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
            affixDisplay.UpdateStacks(affix, currentCharges-1); //Updates Count
        }
        else
        {
            affixDisplay.RemoveAffix(affix); //Removes visual portion of affix if charges are zeroed out
        }
    }

    public bool CanBePlayed() //Function to be called from game logic to know if this minion can be used right now
    {
        return ownerPlayer == currentTurnPlayer && !usedThisTurn;
    }

    //For the following: make DamageToDeal be -10000 to use the default damage value
    public void MinionUsed(Minion enemyTarget, int Damage) //Function to be called when the game logic uses this minion - It is expected that this is used even for cards that make a minion attack, if that is a thing, so that parasite and thorns function correctly
    {
        damageDealt?.Invoke(); //Sends a message to all other instances of minions that someone has dealt damage in order to manage the dirt in your eyes effect
        int DamageToDeal = Damage;
        Minion targetMinion = enemyTarget;
        if (targetMinion != null && !currentAffixes.ContainsKey(Affix.Threaded)) //Ensures target is valid and minion is not threaded
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
                if (currentAffixes.ContainsKey(Affix.PowerBurst))
                {
                    DamageToDeal += currentAffixes[Affix.PowerBurst]; //Adds damage to an attack based upon power burst
                }
                if (currentAffixes.ContainsKey(Affix.Strength)) //Damage Modification for if character has a Strength modifier
                {
                    DamageToDeal += currentAffixes[Affix.Strength];
                    RemoveAffix(Affix.Strength);
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
            if(currentAffixes.ContainsKey(Affix.Bleed))
            {
                if(animator != null){
                animator.SetTrigger("IsBleeding");
                }
            }
            if (currentAffixes.ContainsKey(Affix.Exploit)) //Similar to strength but only some stacks are removed on use
            {
                int currentExploitStacks = currentAffixes[Affix.Exploit];
                DamageToDeal += currentExploitStacks; //Adds current stacks
                currentAffixes.Remove(Affix.Exploit); //Removes current stack value
                if (currentExploitStacks >= 2)
                {
                    currentAffixes.Add(Affix.Exploit, currentExploitStacks/2); //Readds affix with half the amount of stacks, rounding down
                    affixDisplay.UpdateStacks(Affix.Exploit, currentExploitStacks/2); //Adds visual display of affix
                }
                else
                {
                    affixDisplay.RemoveAffix(Affix.Exploit); //Removes visual portion
                }
            }
            if (currentAffixes.ContainsKey(Affix.Vulnerable)) //Condition for when the character is vulnerable
            {
                DamageToDeal = (int) (DamageToDeal * 1.5); //Increases damage dealt by 50 percent, but then casts it to int
            }
            if (currentAffixes.ContainsKey(Affix.Block) && DamageToDeal > 0) //Condition for when the character has block charges prepared
            {
                int currentBlock = currentAffixes[Affix.Block];
                UnityEngine.Debug.Log("Block Amount: " + currentBlock);
                if (DamageToDeal < currentBlock) //Condition where block completely covers the damage to deal
                {
                    int remainingBlock = currentBlock - DamageToDeal;
                    DamageToDeal = 0;
                    currentAffixes.Remove(Affix.Block);
                    currentAffixes.Add(Affix.Block, remainingBlock); //Replaces block value with the new remaining value
                    affixDisplay.UpdateStacks(Affix.Block, remainingBlock); //Adds visual display of affix
                }
                else //Case where there isn't any block leftover
                {
                    DamageToDeal -= currentBlock;
                    RemoveAffix(Affix.Block); //Removes block affix since all charges are used
                    affixDisplay.RemoveAffix(Affix.Block); //Removes visual portion
                }
            }
        }
        else //Heal condition
        {
            if (currentAffixes.ContainsKey(Affix.Naturopath)) //Condition for naturopath
            {
                int HealModifier = currentAffixes[Affix.Naturopath];
                int RemainingCharges = currentHealth - (DamageToDeal - HealModifier);
                DamageToDeal -= currentAffixes[Affix.Naturopath]; //Adds stored value to healing amount
                currentAffixes.Remove(Affix.Naturopath); //Consumes all naturopath stacks
                if (RemainingCharges > 0)
                {
                    currentAffixes.Add(Affix.Naturopath, RemainingCharges); //Adds back remaining charges to prevent overheal, if possible
                    affixDisplay.UpdateStacks(Affix.Naturopath, RemainingCharges); //Updates count
                }
                else
                {
                    affixDisplay.RemoveAffix(Affix.Naturopath); //Removes visual portion of affix
                }
            }
        }
        
        currentHealth -= DamageToDeal;
        if(animator != null)
        {
            animator.SetTrigger("TakeDamage");
        }

        if (currentHealth <= 0)
        {
            HeirloomManager heirloomManager = FindObjectOfType<HeirloomManager>();
            if (heirloomManager.ContainsHeirloom(Heirloom.Miracle) && ownerPlayer) //Checks if player is the owner of this minion and miracle water is owned
            {
                currentHealth += DamageToDeal; //Undoes attack if miracle water can be consumed
                heirloomManager.RemoveHeirloom(Heirloom.Miracle); //Consumes Miracle Water
            }
            else
            {
                Destroyed(); 
            }
        }
        else if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth; //Caps health to max out at the originally given max health value
        }
    }

    

    public void ConsumeCard(Card c)
    {
        c.DoSpells(this);
        Deck deck = FindObjectOfType<Deck>();

        if (c.IsEphemeral)
        {
            deck.RemoveCard(c);
            return;
        }

        if (c.Spells.Any(spell => spell is RemoveInsteadOfDiscard)) //IRemoves cards instead of discarding them if they have the "RemoveInsteadOfDiscard" spell as a flag for this functionality (Scratch is the only one with this as of 1/25/2025)
        {
            deck.RemoveCard(c);
        }
        else
        {
            deck.Discard(c);
        }
    }

    private void Destroyed() //Function for when this character has been defeated
    {
        Deck deck = FindObjectOfType<Deck>();
        CardDatabase database = FindObjectOfType<CardDatabase>();
        int minionIndex = database.RetrieveIndexFromMinion(this);
        /*UnitParty party = GameObject.Find("PlayerParty").GetComponent<UnitParty>();
        int minionIndex = party.IndexOf(this.GetComponent<Transform>());
        minionIndex = Math.Abs(minionIndex-2); //Inverts value of index because the index values in the database are opposite of that of the transforms*/
        deck.RemoveCards(minionIndex);// Needs to be index in party
  
        EncounterController.onTurnChanged -= TurnStart; //Unsubscribes this minion from the turn changed action upon minion being destroyed

        onDeath?.Invoke(this); // See line 55

        if (gameObject != null) //Safety check for null error
        {
            Destroy(gameObject);
        }
    }

    public void Gouge(int Factor = 2) //Public function to double bleed stacks on minion, doing the gouge effect
    {
        if (currentAffixes.ContainsKey(Affix.Bleed))
        {
            int currentBleedStacks = currentAffixes[Affix.Bleed]; //Stores current stacks
            currentAffixes.Remove(Affix.Bleed); //Removes bleed
            currentAffixes.Add(Affix.Bleed, currentBleedStacks * Factor); //Reimplements bleed with a doubled amount of stacks
            affixDisplay.UpdateStacks(Affix.Bleed, currentBleedStacks * Factor); //Updates count
        }
    }

    public void Cleanse() //Public function to remove all negative affixes
    {
        //Curse is not here on purpose
        if (currentAffixes.ContainsKey(Affix.DamageReduction)) 
        {
            RemoveAffix(Affix.DamageReduction);
        }
        if (currentAffixes.ContainsKey(Affix.Vulnerable)) 
        {
            RemoveAffix(Affix.Vulnerable);
        }
        if (currentAffixes.ContainsKey(Affix.Bleed)) 
        {
            RemoveAffix(Affix.Bleed);
        }
        if (currentAffixes.ContainsKey(Affix.Mark)) 
        {
            RemoveAffix(Affix.Mark);
        }
        if (currentAffixes.ContainsKey(Affix.Threaded)) 
        {
            RemoveAffix(Affix.Threaded);
        }
        if (currentAffixes.ContainsKey(Affix.Exploit)) 
        {
            RemoveAffix(Affix.Exploit);
        }
    }

    public void RemoveAllAffixes()
    {
        foreach (Affix affix in currentAffixes.Keys)
        {
            RemoveAffix(affix);//Removes all affixes and updates affix display accordingly
        }
    }

    public void RemoveAffix(Affix affixToRemove, int amount = -1)
    {
        if (amount < 0)
        {
            currentAffixes.Remove(affixToRemove);
            affixDisplay.RemoveAffix(affixToRemove);
        }
        else
        {
            AddAffix(affixToRemove, -amount);
        }
    }

    public bool HasDebuff() //Returns true if this minion has a debuff
    {
        if (currentAffixes.ContainsKey(Affix.DamageReduction) || currentAffixes.ContainsKey(Affix.Vulnerable ) ||
        currentAffixes.ContainsKey(Affix.Bleed) || currentAffixes.ContainsKey(Affix.Mark)||
        currentAffixes.ContainsKey(Affix.Threaded) || currentAffixes.ContainsKey(Affix.Exploit) || currentAffixes.ContainsKey(Affix.Curse))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public List<Affix> RetrieveBuffs()
    {
        List<Affix> Returnable = new List<Affix>();
        if (currentAffixes.ContainsKey(Affix.Taunt)) 
        {
            Returnable.Add(Affix.Taunt);
        }
        if (currentAffixes.ContainsKey(Affix.Block)) 
        {
            Returnable.Add(Affix.Block);
        }
        if (currentAffixes.ContainsKey(Affix.Thorns)) 
        {
            Returnable.Add(Affix.Thorns);
        }
        if (currentAffixes.ContainsKey(Affix.Regen)) 
        {
            Returnable.Add(Affix.Regen);
        }
        if (currentAffixes.ContainsKey(Affix.Parasite)) 
        {
            Returnable.Add(Affix.Parasite);
        }
        if (currentAffixes.ContainsKey(Affix.Strength)) 
        {
            Returnable.Add(Affix.Strength);
        }
        if (currentAffixes.ContainsKey(Affix.HoundCounter)) 
        {
            Returnable.Add(Affix.HoundCounter);
        }
        if (currentAffixes.ContainsKey(Affix.Naturopath)) 
        {
            Returnable.Add(Affix.Naturopath);
        }
        return Returnable;
    }

    /*
    public int RemoveAffixAndCount(Affix AffixToRemove)
    {
        if (currentAffixes.ContainsKey(AffixToRemove))
        {
            int currentCount = currentAffixes[AffixToRemove]; //Counts current count
            RemoveAffix(AffixToRemove); //Removes affix
            return currentCount;
        }
        return -1; //If affix isnt on minion
    }
    */

    public int GetAffixCount(Affix checkedAffix)
    {
        if (currentAffixes.ContainsKey(checkedAffix))
            return currentAffixes[checkedAffix];
        else
            return -1;
    }

    public void DirtReception() //This function is used to manage the dirt in your eyes effect globally
    {
        if (DirtInEyes)
        {
            AddAffix(Affix.DamageReduction, 1); //Adds 1 stack of damage reduction to this minion when the dirt in eyes effect needs to be applied
        }
    }
}
