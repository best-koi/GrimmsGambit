using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sister : Lycan
{
    [SerializeField]
    protected List<string> phaseBAttacks;//A list of strings mapped to methods

    protected static int randomStartAttack;//A static variable that should match for both sisters 
    protected bool isBelowHealthThreshold;//A boolean value to be accessed by the other sister

    protected static bool isSecondPhaseB;

    protected static Color sister1Color = new Color(255,255,0);
    protected static Color sister2Color = new Color(255,0,255);

    [SerializeField]
    private string sisterToDefendName;

    private bool usedOneTimeAttack = false; 
    private static bool hasResetStatus = false;


    
[Header("Sister 1 - Phase 1 Values")]
    [SerializeField]
    protected int sisterBlock, tripleAttackValue;//Values for the first phase for Sister 1

    [SerializeField]
    protected bool canGenerateStartIndex; //Syncs up the sisters at the start of the game

[Header("Sister 2 - Phase 1 Values")]
    [SerializeField]
    protected int sisterHeal, sisterStrength;//Amounts to block and heal by 

[Header("Phase 2A Values")]
[SerializeField]
    protected int curseValue, secondAttackValue;//Values for either sister who is left (curse is used in Phase 2B also)


[Header("Sister 1 - Phase 2B Values")]
[SerializeField]
    protected int doubleAttackValue;//The value for Sister 1's Double Attack


[Header("Sister 2 - Phase 2B Values")]
[SerializeField]
    protected int bAttack, healBothValue, secondProtect, secondStrength;//The values of Sister 2 for healing both sisters, protect, and strength



    
/*The Sister Script will function similar to the Lycan with some adjustments
A static variable will be used to ensure both sisters start on the same attack number
*/

    // Start here is used to sync up the sister attack patterns
    protected override void Start()
    {
        controller = FindObjectOfType(typeof(EncounterController)) as EncounterController;
        if(canGenerateStartIndex == true){
            randomStartAttack = Random.Range(0, attacks.Count);
            Sister[] theSisters = FindObjectsOfType<Sister>();
            foreach(Sister s in theSisters){
                s.SetStartingIndex(randomStartAttack);
                if(s.gameObject == this.gameObject)
                    continue;
        }
        }

        
        
    }
    
    //Returns the starting value for the sister
    protected virtual int GetStartingIndex(){
        return currentAttack;
    }

//Sets the starting value for the sister (for syncing purposes)
    protected virtual void SetStartingIndex(int value){
        currentAttack = value;
    }

//Checks health of sisters to determine which phase to switch to
   protected override void Update()
    {
        if(minion.currentHealth <= switchPhaseHealth)
            isBelowHealthThreshold = true; 

        Sister[] theSisters = FindObjectsOfType<Sister>();
        //For determining who to protect (enemy intent display)
        if(theSisters.Length != 2){
            if(isSecondPhaseB){
                sisterToDefendName = GetEnemyName();
            }else{
                isSecondPhase = true;
                if(hasResetStatus != true){
                    minion.RemoveAllAffixes(); 
                    hasResetStatus = true; 

                }
                

            }
                
        }else{

        int sistersBelowHP = 0;
        foreach(Sister s in theSisters){
            if(s.GetIsBelowHealthThreshold() == true){
                sistersBelowHP++;
            }

        }
        if(sistersBelowHP == 2){
            isSecondPhaseB = true; 
            if(hasResetStatus == false){
                foreach(Sister s in theSisters){
                    
                    s.GetComponent<Minion>().RemoveAllAffixes();

            }
                
        }
        hasResetStatus = true;

        }
        }

        healthText.text = $"{minion.currentHealth}/ {minion.maxHealth}";
        nameText.text = enemyName;
        
        HeirloomManager heirloomManager = FindObjectOfType<HeirloomManager>();
        if (heirloomManager.ContainsHeirloom(Heirloom.Blindfold))
        {
            moveText.text = "Blindfold Active"; //This could be replaced with simply ""
        }
        else
        {
            CheckCurrentAttack();
        }
        
        
    }

    protected override void CheckCurrentAttack()
    {
        if(isSecondPhase)
            SecondAttackPhase();
        else if (isSecondPhaseB){
            SecondAttackPhaseB();

        }else   
            FirstAttackPhase();

    }

//Determines which attack pattern to utilize
public override void AttackPattern()
    {
        if(isSecondPhaseB == true){
        CheckAttackBounds();
        //Calls a method from the list of available attacks
        Invoke(phaseBAttacks[currentAttack], 0f);
        //Moves onto the next attack
        currentAttack++;
        CheckAttackBounds();

        }else if(isSecondPhase){
            CheckAttackBounds();
        //Calls a method from the list of available attacks
        Invoke(secondaryAttacks[currentAttack], 0f);
        //Moves onto the next attack
        currentAttack++;
        CheckAttackBounds();
        }  
        else {
        CheckAttackBounds();
        //Calls a method from the list of available attacks
        Invoke(attacks[currentAttack], 0f);
        //Moves onto the next attack
        currentAttack++;
        CheckAttackBounds();
        }
    }

//Checks the attack boundaries for each phase's attacks
protected override void CheckAttackBounds()
    {
        if(isSecondPhase == true){
            //Checks bounds of secondary attacks list
            if (currentAttack >= secondaryAttacks.Count)
                currentAttack = 0;
        else if (isSecondPhaseB == true){
            if (currentAttack >= phaseBAttacks.Count)
                currentAttack = 0;
        }
        }else{
            //Checks bounds of regular attacks list
            if (currentAttack >= attacks.Count)
                currentAttack = 0;

        }
        
    }






//Returns if the sister reached the health threshold 
    protected bool GetIsBelowHealthThreshold(){
        return isBelowHealthThreshold;
    }

//Used for each sister to protect one another
//Accounts for if a sister is the only one left 
    protected virtual void Protect(){
        Sister[] theSisters = FindObjectsOfType<Sister>();
        if(theSisters.Length != 2)
            minion.AddAffix(Affix.Block, secondProtect);

        foreach(Sister s in theSisters){
            if(s.gameObject == this.gameObject){
                continue;

            }else  
                if(isSecondPhaseB)
                    s.GetComponent<Minion>().AddAffix(Affix.Block, secondProtect);
                else
                    s.GetComponent<Minion>().AddAffix(Affix.Block, sisterBlock);
            }

        }

//Used for the sister to strengthen the other 
//Takes into account which sister is left
    protected virtual void Strengthen(){
        Sister[] theSisters = FindObjectsOfType<Sister>();
        if(theSisters.Length != 2)
            minion.AddAffix(Affix.Strength, secondStrength);

        foreach(Sister s in theSisters){
            if(s.gameObject == this.gameObject){
                continue;
            }else  
                if(isSecondPhaseB)
                 s.GetComponent<Minion>().AddAffix(Affix.Strength, secondStrength);
                else
                    s.GetComponent<Minion>().AddAffix(Affix.Strength, sisterStrength);
            }

        }

//Used to heal the other sister
    protected virtual void HealTwin(){
        Sister[] theSisters = FindObjectsOfType<Sister>();
        foreach(Sister s in theSisters){
            if(s.gameObject == this.gameObject){
                continue;

            }else  {
                Minion sisterMinion = s.GetComponent<Minion>();
                if (sisterMinion.currentHealth + sisterHeal < sisterMinion.maxHealth)
                    sisterMinion.currentHealth += sisterHeal;
                else
                    sisterMinion.currentHealth = sisterMinion.maxHealth;
                    }

            }

        }

//For use in Phase 2B
    protected virtual void HealBothTwins(){
        Sister[] theSisters = FindObjectsOfType<Sister>();
        foreach(Sister s in theSisters){
            Minion sisterMinion = s.GetComponent<Minion>();
            if (sisterMinion.currentHealth + healBothValue < sisterMinion.maxHealth)
                sisterMinion.currentHealth += healBothValue;
            else
                sisterMinion.currentHealth = sisterMinion.maxHealth;
                }
            usedOneTimeAttack = true;

            }

        
    

       

//For use in Phase 2A: Used for a standard attack in Phase 2A
    protected virtual void SecondAttack()
    {
       minion.MinionUsed(attackTarget.GetComponent<Minion>(), secondAttackValue);
       FindTarget();
    }

    //For use in Phase 2B: Deals damage to a player according to phase 2B's value
    protected virtual void BAttack()
    {
       minion.MinionUsed(attackTarget.GetComponent<Minion>(), bAttack);
       FindTarget();
    }

//For use in Phase 2b: Deals damage twice to a player
    protected virtual void DoubleAttack(){
        minion.MinionUsed(attackTarget.GetComponent<Minion>(), doubleAttackValue * 2);
        FindTarget();
    }

//For use in Phase 1: Deals damage three times to a player
    protected virtual void TripleAttack(){
        minion.MinionUsed(attackTarget.GetComponent<Minion>(), tripleAttackValue * 3);
        FindTarget();
    }

//For use in Phase 2A and 2B
protected virtual void AoECurse(){
    foreach(CharacterTemplate c in targets){
                if(c == null)
                    continue;
                c.GetComponent<Minion>().AddAffix(Affix.Curse, curseValue);
            }
        usedOneTimeAttack = true;
}


//Used in First Attack Phase
    protected override void FirstAttackPhase(){
    switch (attacks[currentAttack])
        {
            case "Attack":
                if (attackTarget == null)
                    FindTarget();
                

                if (!CanAttackTarget())
                {
                    AdvanceAttack();
                }
                else
                {
                    moveText.text = $"Attack {attackTarget.GetCharacterName()} for {attackValue} DMG";
                    moveText.color = attackTarget.GetCharacterColor();
   
                }
                break;

        case "Protect":
                moveText.text = $"Protecting {sisterToDefendName} for {sisterBlock}";
                moveText.color = sister2Color;
                break;
        case "Strengthen":
                moveText.text = $"Strengthening {sisterToDefendName} for {sisterStrength}";
                moveText.color = sister1Color;
                break;

            
        case "TripleAttack":
                if (attackTarget == null)
                    FindTarget();
                

                if (!CanAttackTarget())
                {
                    AdvanceAttack();
                }
                else
                {
                    moveText.text = $"Attacking {attackTarget.GetCharacterName()} 3 times for {tripleAttackValue} DMG";
                    moveText.color = attackTarget.GetCharacterColor();
   
                }
                break;
        case "HealTwin":
                moveText.text = $"Healing {sisterToDefendName} for {sisterHeal}";
                moveText.color = sister1Color;
                break;

            default:
                moveText.text = "Upcoming Move: " + attacks[currentAttack];
                moveText.color = Color.white;
                break;
        }
  
}

//Used in Phase 2A
protected virtual void SecondAttackPhase(){
    switch (secondaryAttacks[currentAttack])
        {
            case "SecondAttack":
                if (attackTarget == null)
                    FindTarget();
                

                if (!CanAttackTarget())
                {
                    AdvanceAttack();
                }
                else
                {
                    moveText.text = $"Attack {attackTarget.GetCharacterName()} for {secondAttackValue} DMG";
                    moveText.color = attackTarget.GetCharacterColor();
   
                }
                break;
            case "AoECurse":
                
            if (attackTarget == null)
                    FindTarget();

                if(usedOneTimeAttack)
                    AdvanceAttack();
                else
                    moveText.text = $"Cursing party for {curseValue}";
                break;

            case "AoEAttack":
            if (attackTarget == null)
                    FindTarget();
                moveText.text = $"Attacking party for {aoeAttackValue} DMG";
                break;

            case "Strength":
                moveText.text = $"Applying {buffValue} Strength to Self";
                moveText.color = this.GetEnemyColor();
                break;
            case "Block":
                moveText.text = $"Blocking for {blockValue}";
                moveText.color = this.GetEnemyColor();
                break;

            case "CombinedAttack":
                if (attackTarget == null)
                    FindTarget();
                moveText.text = $"Attacking {attackTarget.GetCharacterName()} for {secondAttackValue} and Defending for {blockValue}";
                break;

            default:
                moveText.text = "Upcoming Move: " + attacks[currentAttack];
                moveText.color = Color.white;
                break;
        }

}

//Used in Phase 2B
protected virtual void SecondAttackPhaseB(){
    switch (phaseBAttacks[currentAttack])
        {
            case "BAttack":
                if (attackTarget == null)
                    FindTarget();
                

                if (!CanAttackTarget())
                {
                    AdvanceAttack();
                }
                else
                {
                    moveText.text = $"Attack {attackTarget.GetCharacterName()} for {bAttack} DMG";
                    moveText.color = attackTarget.GetCharacterColor();
   
                }
                break;
            case "DoubleAttack":
                if (attackTarget == null)
                    FindTarget();
                

                if (!CanAttackTarget())
                {
                    AdvanceAttack();
                }
                else
                {
                    moveText.text = $"Attacking {attackTarget.GetCharacterName()} 2 times for {doubleAttackValue} DMG";
                    moveText.color = attackTarget.GetCharacterColor();
   
                }
                break;
            case "AoECurse":
            if (attackTarget == null)
                    FindTarget();
                if(usedOneTimeAttack)
                    AdvanceAttack();
                else
                moveText.text = $"Cursing party for {curseValue}";
                break;

            case "AoEAttack":
            if (attackTarget == null)
                    FindTarget();
                moveText.text = $"Attacking party for {aoeAttackValue} DMG";
                break;

            case "Protect":
                moveText.text = $"Protecting {sisterToDefendName} for {sisterBlock}";

                if(sisterToDefendName == "Sister 1")
                    moveText.color = sister1Color;
                else    
                    moveText.color = sister2Color;
                break;

            case "Strengthen":
                moveText.text = $"Strengthening {sisterToDefendName} for {sisterStrength}";
                if(sisterToDefendName == "Sister 1")
                    moveText.color = sister1Color;
                else    
                    moveText.color = sister2Color;

                break;

            case "Block":
                moveText.text = $"Blocking for {blockValue}";
                moveText.color = this.GetEnemyColor();
                break;
            case "HealBothTwins":
                if(usedOneTimeAttack)
                    AdvanceAttack();
                else
                    moveText.text = $"Healing Both Sisters for {healBothValue}";
                    moveText.color = sister1Color;
                break;

            default:
                moveText.text = "Upcoming Move: " + attacks[currentAttack];
                moveText.color = Color.white;
                break;
        }

}






}


















