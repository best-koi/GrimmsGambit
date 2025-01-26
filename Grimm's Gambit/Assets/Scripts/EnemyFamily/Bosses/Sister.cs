using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sister : Lycan
{
    protected static int randomStartAttack;//A static variable that should match for both sisters 
    protected bool isBelowHealthThreshold;//A boolean value to be accessed by the other sister

    protected static bool isSecondPhaseB;
    
[Header("Sister 1 - Phase 1 Values")]
    [SerializeField]
    protected int sisterBlock, tripleAttackValue;

[Header("Sister 2 - Phase 1 Values")]
    [SerializeField]
    protected int sisterHeal, sisterStrength;//Amounts to block and heal by 

    
/*The Sister Script will function similar to the Lycan with some adjustments
A static variable will be used to ensure both sisters start on the same attack number
*/

    // Start here is used to sync up the sister attack patterns
    protected override void Start()
    {
        controller = FindObjectOfType(typeof(EncounterController)) as EncounterController;
        randomStartAttack = Random.Range(0, attacks.Count);
        currentAttack = randomStartAttack;
        
    }
    

   protected override void Update()
    {
        if(minion.currentHealth <= switchPhaseHealth)
            isBelowHealthThreshold = true; 

        Sister[] theSisters = FindObjectsOfType<Sister>();
        int sistersBelowHP = 0;
        foreach(Sister s in theSisters){
            if(s.GetIsBelowHealthThreshold() == true){
                sistersBelowHP++;

            }

        }

        if(sistersBelowHP == 2){
            isSecondPhaseB = true; 
        }

        healthText.text = $"{minion.currentHealth}/ {minion.maxHealth}";
        nameText.text = enemyName;
        CheckCurrentAttack();
        
        
    }

public override void AttackPattern()
    {
        if(isSecondPhaseB == true){
        CheckAttackBounds();
        //Calls a method from the list of available attacks
        Invoke(secondaryAttacks[currentAttack], 0f);
        //Moves onto the next attack
        currentAttack++;
        CheckAttackBounds();

        }else if(isSecondPhase){



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








    protected bool GetIsBelowHealthThreshold(){
        return isBelowHealthThreshold;
    }


    protected virtual void Protect(){
        Sister[] theSisters = FindObjectsOfType<Sister>();
        foreach(Sister s in theSisters){
            if(s.gameObject == this.gameObject){
                continue;

            }else  
                s.GetComponent<Minion>().AddAffix(Affix.Block, sisterBlock);
            }

        }

    protected virtual void Strengthen(){
        Sister[] theSisters = FindObjectsOfType<Sister>();
        foreach(Sister s in theSisters){
            if(s.gameObject == this.gameObject){
                continue;

            }else  
                s.GetComponent<Minion>().AddAffix(Affix.Strength, sisterStrength);
            }

        }

    protected virtual void HealTwin(){
        Sister[] theSisters = FindObjectsOfType<Sister>();
        foreach(Sister s in theSisters){
            if(s.gameObject == this.gameObject){
                continue;

            }else  
                s.GetComponent<Minion>().currentHealth += sisterHeal;
            }

        }

        


    

    protected virtual void TripleAttack(){
        minion.MinionUsed(attackTarget.GetComponent<Minion>(), tripleAttackValue * 3);
        FindTarget();
    }



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
                moveText.text = $"Protecting Sister for {sisterBlock}";
                moveText.color = Color.white;
                break;
        case "Strengthen":
                moveText.text = $"Strengthening Sister for {sisterStrength}";
                moveText.color = Color.white;
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
                moveText.text = $"Healing Sister for {sisterHeal}";
                moveText.color = Color.white;
                break;

            default:
                moveText.text = "Upcoming Move: " + attacks[currentAttack];
                moveText.color = Color.white;
                break;
        }




   
}
}
