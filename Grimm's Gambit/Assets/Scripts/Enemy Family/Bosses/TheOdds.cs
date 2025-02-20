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
    protected int aoeAttackValue, startPhase2Health, startFinalPhaseHealth;

    [SerializeField]
    protected int firstDefend; 




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

    protected virtual void AoEAttack(){
    foreach(CharacterTemplate c in targets){
                if(c == null)
                    continue;
                minion.MinionUsed(c.GetComponent<Minion>(), aoeAttackValue);
            }
}


   

    protected virtual void FirstAttackPhase(){
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

            case "Defend":
                moveText.text = $"Defending for {firstDefend}";
                moveText.color = this.GetEnemyColor();
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

            

            default:
                moveText.text = "Upcoming Move: " + attacks[currentAttack];
                moveText.color = Color.white;
                break;
        }

}

protected virtual void FinalAttackPhase(){
    switch (finalPhaseAttacks[currentAttack])
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

            

            default:
                moveText.text = "Upcoming Move: " + attacks[currentAttack];
                moveText.color = Color.white;
                break;
        }

}




}
