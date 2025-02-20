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
    private int dieStrength, dieBlock;//Values to add strength and block with roll the dice 

    [SerializeField]
    protected int startPhase2Health, startFinalPhaseHealth;

    [SerializeField]
    protected int firstAOEDefend, secondAOEDefend, finalDefend; 

    [SerializeField]
    private int attack1Value, attack2Value, attack3Value, attack4Value; 


    [SerializeField]
    protected int aoeAttackValue1, aoeAttackValue2, aoeAttackValue3; 

    private bool isSecondPhase, isFinalPhase, hasRolledDice; 

    // Start is called before the first frame update
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
                c.GetComponent<Minion>().AddAffix(Affix.Vulnerable, (die1+die2)/2 );
                c.GetComponent<Minion>().AddAffix(Affix.DamageReduction, (die1+die2)/2 );
            }
            
        }else if (die1 + die2 > 6){
            //New Affix Goes Here

        }
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

    protected virtual void Attack4()
    {
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

            case "Attack4":
                if (attackTarget == null)
                    FindTarget();
                

                if (!CanAttackTarget())
                {
                    AdvanceAttack();
                }
                else
                {
                    moveText.text = $"Attack {attackTarget.GetCharacterName()} for {attack4Value} DMG";
                    moveText.color = attackTarget.GetCharacterColor();
   
                }
                break;

            

            default:
                moveText.text = "Upcoming Move: " + attacks[currentAttack];
                moveText.color = Color.white;
                break;
        }

}




}
