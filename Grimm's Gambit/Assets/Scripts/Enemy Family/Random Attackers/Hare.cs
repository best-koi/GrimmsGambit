using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hare : EnemyRandomTarget
{

[Header("Hare Values")]
    [SerializeField]
    private int secondAttackValue, secondBlock, runBlock, endAttackLoop; 

    private int currentAttackLoop = 0; 
    private bool hasRun = false; 


    

    protected override void Start()
    {
        controller = FindObjectOfType(typeof(EncounterController)) as EncounterController;
        //Sets color to preset color
        renderer.material.color = enemyColor;
        //Starts the enemy with a random attack
        currentAttack = 0;
    }

    protected virtual void DoubleAttack(){
        minion.MinionUsed(attackTarget.GetComponent<Minion>(), attackValue * 2);
        FindTarget();
    }

    private void Run(){
        minion.AddAffix(Affix.Block, runBlock);
    }

    private void Block2(){
        minion.AddAffix(Affix.Block, secondBlock);
    }

    protected override void CheckAttackBounds()
    {
        /*
        if(currentAttackLoop == endAttackLoop){
            //End Encounter Logic
            moveText.text = "Running Away.";
        }
        */
        if (currentAttack >= attacks.Count){
            currentAttack = 0;
            //currentAttackLoop++; 
        }
        
    }

    protected void AttackAndBleed(){

        //Attacks
        minion.MinionUsed(attackTarget.GetComponent<Minion>(), secondAttackValue);
        //Applies Bleed
        attackTarget.GetComponent<Minion>().AddAffix(Affix.Bleed, buffValue);
       FindTarget();
    }

    protected override void Update()
    {
        healthText.text = $"{minion.currentHealth}/ {minion.maxHealth}";
        nameText.text = enemyName;
        //Check to see if the blindfold heirloom is going to prevent current attack from being displayed: - Implemented by Ryan Lockie on 2/2/2025
        HeirloomManager heirloomManager = FindObjectOfType<HeirloomManager>();
        if (heirloomManager.ContainsHeirloom(Heirloom.Blindfold))
        {
            moveText.text = "Blindfold Active"; //This could be replaced with simply ""
            isBlindfolded = true;
            CheckCurrentAttack();
        }
        else if(minion.currentHealth < minion.maxHealth && hasRun == false)
        {
            Run(); 
            hasRun = true;
        }else{
            CheckCurrentAttack();
        }
    }



     protected override void CheckCurrentAttack()
    {
        switch (attacks[currentAttack])
        {
            case "Attack":
                if (attackTarget == null)
                    FindTarget();
                

                if (!CanAttackTarget())
                {
                    if (attacks.Count == 1)
                        moveText.text = "Done Acting.";
                    else
                        AdvanceAttack();
                }
                else if(!isBlindfolded)
                {
                        if (minion.currentAffixes.ContainsKey(Affix.Strength))
                            moveText.text = $"Attack {attackTarget.GetCharacterName()} for {attackValue + buffValue} DMG";
                        else
                            moveText.text = $"Attack {attackTarget.GetCharacterName()} for {attackValue} DMG";
                        moveText.color = attackTarget.GetCharacterColor();
   
                }

                break;

            case "DoubleAttack":
                if (attackTarget == null)
                    FindTarget();
                

                if (!CanAttackTarget())
                {
                    if (attacks.Count == 1)
                        moveText.text = "Done Acting.";
                    else
                        AdvanceAttack();
                }
                else if(!isBlindfolded)
                {
                        if (minion.currentAffixes.ContainsKey(Affix.Strength))
                            moveText.text = $"Attack {attackTarget.GetCharacterName()} for {attackValue + buffValue} DMG twice";
                        else
                            moveText.text = $"Attack {attackTarget.GetCharacterName()} for {attackValue} DMG twice";
                        moveText.color = attackTarget.GetCharacterColor();
   
                }

                break;

            case "CombinedAttack":
                if (attackTarget == null)
                    FindTarget();
                    if(!isBlindfolded){
                moveText.text = $"Attacking {attackTarget.GetCharacterName()} for {attackValue} and Defending for {secondBlock}";
                moveText.color = attackTarget.GetCharacterColor();
                    }
                break;

            case "AttackAndBleed":
                if (attackTarget == null)
                    FindTarget();
                if(!isBlindfolded)
                moveText.text = $"Attacking {attackTarget.GetCharacterName()} for {secondAttackValue} and Inflicting Bleed for {buffValue}";
                break;

            case "Run":
                moveText.text = $"Blocking for {runBlock}";
                moveText.color = new Color(1.0f, 0.64f, 0.0f);

            

            break;

            case "Block":
            if(!isBlindfolded){
                moveText.text = $"Blocking for {blockValue}";
                moveText.color = this.GetEnemyColor();
            }
                break;

            default:
                moveText.text = "Upcoming Move: " + attacks[currentAttack];
                moveText.color = Color.white;
                break;
        }
    }
}
