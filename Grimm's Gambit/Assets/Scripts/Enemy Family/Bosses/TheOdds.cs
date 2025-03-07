using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TheOdds : AoEEnemy
{

    [SerializeField]
    protected List<string> secondPhaseAttacks, finalPhaseAttacks;//A list of strings mapped to methods

    [SerializeField]
    private List <int> dice;//A list of ints, each representing a side of a die

    [SerializeField]
    private List<Sprite> dieSprites;//A list of sprites that correspond to the dice list

    [SerializeField]
    private Image dieImage1, dieImage2;//Two images representing the die values

    private int die1, die2;//2 ints represented a dice roll 

    [SerializeField]
    private int dieStrength, dieBlock, powerOfFateGoodValue;//Values to add strength and block with roll the dice 

    [SerializeField]
    protected int startPhase2Health, startFinalPhaseHealth;

    [SerializeField]
    protected int firstAOEDefend, secondAOEDefend, finalDefend; 

    [SerializeField]
    private int attack1Value, attack2Value, attack3Value, attack4Value; 


    [SerializeField]
    protected int aoeAttackValue1, aoeAttackValue2, aoeAttackValue3; 

    private bool isSecondPhase, isFinalPhase, hasRolledDice, phase1, phase2, phase3; 

    protected override void Update()
    {
        if(minion.currentHealth <= startFinalPhaseHealth && isFinalPhase == false){
            isFinalPhase = true;
            minion.RemoveAllAffixes(); 
            PowerOfFateAll();
             
        }
            
        else if (minion.currentHealth <= startPhase2Health && isSecondPhase == false){
            isSecondPhase = true;
            minion.RemoveAllAffixes();
            PowerOfFateAll();
            
                
        }
            
        else{
            if(phase1 == false){
                RollTheDice();
                phase1 = true;

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

    protected override void CheckAttackBounds()
    {
        if(isFinalPhase == true){
            //Checks bounds of secondary attacks list
            if (currentAttack >= finalPhaseAttacks.Count)
                currentAttack = 0;
        else if (isSecondPhase == true){
            if (currentAttack >= secondPhaseAttacks.Count)
                currentAttack = 0;
        }
        }else{
            //Checks bounds of regular attacks list
            if (currentAttack >= attacks.Count)
                currentAttack = 0;

        }
        
    }

    protected override void CheckCurrentAttack()
    {
        if(isFinalPhase)
            FinalAttackPhase();
        else if (isSecondPhase){
            SecondAttackPhase();

        }else   
            FirstAttackPhase();

    }

    public override void AttackPattern()
    {
        if(isFinalPhase == true){
            Invoke(finalPhaseAttacks[currentAttack], 0f);
            CheckAttackBounds();
            currentAttack++;
            CheckAttackBounds();

        }
        else if(isSecondPhase == true){
        CheckAttackBounds();
        //Calls a method from the list of available attacks
        Invoke(secondPhaseAttacks[currentAttack], 0f);
        //Moves onto the next attack
        currentAttack++;
        CheckAttackBounds();

        }else{
        CheckAttackBounds();
        //Calls a method from the list of available attacks
        Invoke(attacks[currentAttack], 0f);
        //Moves onto the next attack
        currentAttack++;
        CheckAttackBounds();
        }
    }

    protected virtual void RollTheDice(){

        if(die1 + die2 == 2){
            minion.MinionUsed(minion, 3 * attackValue);

        } else if (die1 + die2 == 12){
            minion.AddAffix(Affix.Strength, dieStrength);
            minion.AddAffix(Affix.Block, dieBlock);


        } else if (die1 + die2 <= 6){

            foreach(CharacterTemplate c in targets){
                if(c == null)
                    continue;
                c.GetComponent<Minion>().AddAffix(Affix.Vulnerable, (die1+die2));
                c.GetComponent<Minion>().AddAffix(Affix.DamageReduction, (die1+die2));
            }
            
        }else if (die1 + die2 > 6){
            minion.AddAffix(Affix.PowerBurst, die1 + die2);

        }
        hasRolledDice = false; 
    }

    protected virtual void Attack1()
    {
       minion.MinionUsed(attackTarget.GetComponent<Minion>(), attack1Value);
       FindTarget();
    }

    protected virtual void Attack2()
    {
       minion.MinionUsed(attackTarget.GetComponent<Minion>(), attack2Value);
       FindTarget();
    }

    protected virtual void Attack3()
    {
       minion.MinionUsed(attackTarget.GetComponent<Minion>(), attack3Value);
       FindTarget();
    }


    protected virtual void AttackAndDefend(){
        minion.AddAffix(Affix.Block, finalDefend);
        minion.MinionUsed(attackTarget.GetComponent<Minion>(), attack4Value);
       FindTarget();

    }

    protected virtual void AoEAttack(){
        int chosenAOEAttack = 0;
        if(isFinalPhase){
            chosenAOEAttack = aoeAttackValue3;

        }else if (isSecondPhase){
            chosenAOEAttack = aoeAttackValue2;

        }else{
            chosenAOEAttack = aoeAttackValue1;
            
        }
        foreach(CharacterTemplate c in targets){
                if(c == null)
                    continue;
                minion.MinionUsed(c.GetComponent<Minion>(), chosenAOEAttack);
            }
    
    }

    protected virtual void PowerOfFateAll(){
        minion.AddAffix(Affix.Strength, dieStrength);
            minion.AddAffix(Affix.Block, dieBlock);



    }

    protected virtual void PowerOfFateGood(){
        minion.AddAffix(Affix.PowerBurst, powerOfFateGoodValue);
        

    }

    protected virtual void PowerOfFateBad(){
        foreach(CharacterTemplate c in targets){
                if(c == null)
                    continue;
                c.GetComponent<Minion>().AddAffix(Affix.Vulnerable, (die1+die2)/2 );
                c.GetComponent<Minion>().AddAffix(Affix.DamageReduction, (die1+die2)/2 );
            }
        

    }

    protected virtual void DefendAndAttackAOE(){
        if (isSecondPhase){
            minion.AddAffix(Affix.Block, secondAOEDefend);
            AoEAttack();

        }else{
            minion.AddAffix(Affix.Block, firstAOEDefend);
            AoEAttack();
            
        }
    }

   


   

    protected virtual void FirstAttackPhase(){
    switch (attacks[currentAttack])
        {
            case "Attack1":
                if (attackTarget == null)
                    FindTarget();
                
                if (!CanAttackTarget())
                {
                    AdvanceAttack();
                }
                else
                {
                    moveText.text = $"Attack {attackTarget.GetCharacterName()} for {attack1Value} DMG";
                    moveText.color = attackTarget.GetCharacterColor();
   
                }
                break;

            case "DefendAndAttackAOE":
                if (attackTarget == null)
                    FindTarget();
                
                if (!CanAttackTarget())
                {
                    AdvanceAttack();
                }
                else
                {
                    moveText.text = $"Attacking Party for {aoeAttackValue1} DMG and Defending for {firstAOEDefend}";
                    moveText.color = attackTarget.GetCharacterColor();
   
                }
                break;


            case "RollTheDice":
            
            if(hasRolledDice == false){
                int randomDie1 = Random.Range(0, dice.Count);
                int randomDie2 = Random.Range(0, dice.Count);

                die1 = dice[randomDie1];
                die2 = dice[randomDie2];
                dieImage1.sprite = dieSprites[randomDie1];
                dieImage2.sprite = dieSprites[randomDie2];

                hasRolledDice = true;

                if(die1 + die2 == 2){
                    moveText.text = $"Snake Eyes: Taking {3 * attackValue} Damage";

                 } else if (die1 + die2 == 12){
                    moveText.text = $"Box Cars: Applying {dieStrength} Strength & {dieBlock} Block";


                 } else if (die1 + die2 <= 6){

                    moveText.text = $"Afflicting Party with {die1+die2} Vulnerable and Damage Reduction";
            
                }else if (die1 + die2 > 6){
                  moveText.text = $"Applying {die1+die2} Power Burst";
                }
            }
            

            break;


            default:
                moveText.text = "Upcoming Move: " + attacks[currentAttack];
                moveText.color = this.GetEnemyColor();
                break;
        }

}

protected virtual void SecondAttackPhase(){
    switch (secondPhaseAttacks[currentAttack])
        {
            case "Attack2":
                if (attackTarget == null)
                    FindTarget();
                

                if (!CanAttackTarget())
                {
                    AdvanceAttack();
                }
                else
                {
                    moveText.text = $"Attack {attackTarget.GetCharacterName()} for {attack2Value} DMG";
                    moveText.color = attackTarget.GetCharacterColor();
   
                }
                break;

            case "DefendAndAttackAOE":
                if (attackTarget == null)
                    FindTarget();
                
                if (!CanAttackTarget())
                {
                    AdvanceAttack();
                }
                else
                {
                    moveText.text = $"Attacking Party for {aoeAttackValue2} DMG and Defending for {secondAOEDefend}";
                    moveText.color = attackTarget.GetCharacterColor();
   
                }
                break;

            case "PowerOfFateAll":
                moveText.text = $"Power of Fate";
                break;

            case "PowerOfFateGood":
                moveText.text = $"Power of Fate";
                break;

            case "PowerOfFateBad":
                moveText.text = $"Power of Fate";
                break;
                
            
            default:
                moveText.text = "Upcoming Move: " + attacks[currentAttack];
                moveText.color = Color.white;
                break;
        }

}

protected virtual void FinalAttackPhase(){
    switch (finalPhaseAttacks[currentAttack])
        {
            case "Attack3":
                if (attackTarget == null)
                    FindTarget();
                

                if (!CanAttackTarget())
                {
                    AdvanceAttack();
                }
                else
                {
                    moveText.text = $"Attack {attackTarget.GetCharacterName()} for {attack3Value} DMG";
                    moveText.color = attackTarget.GetCharacterColor();
   
                }
                break;

            case "AttackAndDefend":
                if (attackTarget == null)
                    FindTarget();
                moveText.text = $"Attacking {attackTarget.GetCharacterName()} for {attack4Value} and Defending for {finalDefend}";
                break;

            case "RollTheDice":
            moveText.text = $"Rolling the Dice";
            if(hasRolledDice == false){
                int randomDie1 = Random.Range(0, dice.Count);
                int randomDie2 = Random.Range(0, dice.Count);

                die1 = dice[randomDie1];
                die2 = dice[randomDie2];
                dieImage1.sprite = dieSprites[randomDie1];
                dieImage2.sprite = dieSprites[randomDie2];

                hasRolledDice = true;
            }
            break;

            case "PowerOfFateGood":
                moveText.text = $"Power of Fate";
                break;

            

            default:
                moveText.text = "Upcoming Move: " + attacks[currentAttack];
                moveText.color = Color.white;
                break;
        }

}




}
