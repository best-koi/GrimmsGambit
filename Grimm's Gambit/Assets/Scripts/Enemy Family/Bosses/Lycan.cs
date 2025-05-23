using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lycan : AoEEnemy
{
    
   [SerializeField]
    protected List<string> secondaryAttacks;//A list of strings mapped to methods

/*All attack values for the Lycan
-The regular AttackValue will be used for the second phase of the Lycan's health
    -weakAttackValue is for the weak attack in Phase 1
    -strongAttackValue is for the strong attack in Phase 1
    -weakShredValue is for the weak shred debuff in Phase 1
    -strongShredValue is for the strong shred debuff in Phase 1
    -aoeAttackValue is for the AoE Attack in Phase 2
    -switchPhaseHealth is a threshold of health that results in the Lycan switching phases (i.e. at X health, it starts Phase 2)
    -firstDefend is the value the Lycan defends for in the first phase
    -secondDefend is the value the Lycan defends for in the second phase
*/
[Header("Lycan-Specific Attack Values")]
    [SerializeField]
    private int weakAttackValue, strongAttackValue, weakShredValue, strongShredValue, firstDefend, secondDefend;

[Header("General Attack Values")]
    [SerializeField]
    protected int aoeAttackValue, switchPhaseHealth;

    protected bool isSecondPhase = false;//A bool indicating whether the Lycan is in its second phase of attacks

//A version of CheckAttackBounds() for the Lycan
protected override void CheckAttackBounds()
    {
        if(isSecondPhase == true){
            //Checks bounds of secondary attacks list
            if (currentAttack >= secondaryAttacks.Count)
                currentAttack = 0;

        }else{
            //Checks bounds of regular attacks list
            if (currentAttack >= attacks.Count)
                currentAttack = 0;

        }
        
    }

//Update() checks for the health threshold to switch attacks
protected override void Update()
    {
        if(minion.currentHealth <= switchPhaseHealth)
            isSecondPhase = true; 
        healthText.text = $"{minion.currentHealth}/ {minion.maxHealth}";
        nameText.text = enemyName;
        
        HeirloomManager heirloomManager = FindObjectOfType<HeirloomManager>();
        if (heirloomManager.ContainsHeirloom(Heirloom.Blindfold))
        {
            isBlindfolded = true;
            moveText.text = "Blindfold Active"; //This could be replaced with simply ""
        }
            CheckCurrentAttack();
        
        
        

    }

//A version of AttackPattern() used to switch the Lycan's phases
public override void AttackPattern()
    {
        if(isSecondPhase == true){
        CheckAttackBounds();
        //Calls a method from the list of available attacks
        Invoke(secondaryAttacks[currentAttack], 0f);
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


//A version of regular attack
protected virtual void WeakAttack()
    {
       minion.MinionUsed(attackTarget.GetComponent<Minion>(), weakAttackValue);
       attackTarget.GetComponent<Minion>().AddAffix(Affix.Vulnerable, strongShredValue);

       FindTarget();
    }

protected override void Attack()
    {
       minion.MinionUsed(attackTarget.GetComponent<Minion>(), attackValue);
       FindTarget();
    }

//A version of attack for applying a strong damage value
 protected virtual void StrongAttack()
    {
       minion.MinionUsed(attackTarget.GetComponent<Minion>(), strongAttackValue);
       attackTarget.GetComponent<Minion>().AddAffix(Affix.Vulnerable, weakShredValue);
       FindTarget();
    }
//A method for applying a weak amount of Shred to characters
/*
    protected virtual void WeakShred()
    {
       minion.AddAffix(Affix.Vulnerable, weakShredValue);
       FindTarget();
    }
    */

//A method for applying a strong amount of Shred to characters
/*
 protected virtual void StrongShred()
    {
       minion.AddAffix(Affix.Vulnerable, strongShredValue);
       FindTarget();
    }
*/
//A value to defend by
protected override void Defend(){
    if(isSecondPhase)
        minion.AddAffix(Affix.Block, secondDefend);
    else
        minion.AddAffix(Affix.Block, firstDefend);
        

}

 

//A version of attack for AoE Damage
protected virtual void AoEAttack(){
    foreach(CharacterTemplate c in targets){
                if(c == null)
                    continue;
                minion.MinionUsed(c.GetComponent<Minion>(), aoeAttackValue);
            }
}

//A placeholder method for charging up the Lycan's attack
protected void ChargingUp()
    {

    }

 protected override void CheckCurrentAttack()
    {
        if(isSecondPhase)
            SecondAttackPhase();
        else   
            FirstAttackPhase();

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
                else if (!isBlindfolded)
                {
                    moveText.text = $"Attack {attackTarget.GetCharacterName()} for {attackValue} DMG";
                    moveText.color = attackTarget.GetCharacterColor();
   
                }
                break;

            case "StrongAttack":
                if (attackTarget == null)
                    FindTarget();
                

                if (!CanAttackTarget())
                {
                    AdvanceAttack();
                }
                else if (!isBlindfolded)
                {
                    moveText.text = $"Attacking {attackTarget.GetCharacterName()} for {strongAttackValue} DMG and Applying {weakShredValue} Vulnerable";
                    moveText.color = attackTarget.GetCharacterColor();
   
                }
                break;
            case "WeakAttack":
                if (attackTarget == null)
                    FindTarget();
                

                if (!CanAttackTarget())
                {
                    AdvanceAttack();
                }
                else if (!isBlindfolded)
                {
                    moveText.text = $"Attacking {attackTarget.GetCharacterName()} for {weakAttackValue} DMG and Applying {strongShredValue} Vulnerable";
                    moveText.color = attackTarget.GetCharacterColor();
   
                }
                break;

            case "AoEAttack":

            if (attackTarget == null)
                    FindTarget();
                if (!isBlindfolded)
                    moveText.text = $"Attacking party for {attackValue} DMG";
                break;

            case "ChargingUp":
                moveText.text = $"Charging Up to Attack";
                moveText.color = this.GetEnemyColor();
                break;

            case "StrongShred":
            if (attackTarget == null)
                    FindTarget();
                if (!CanAttackTarget())
                {
                    AdvanceAttack();
                }
                else if (!isBlindfolded){
                moveText.text = $"Shredding {attackTarget.GetCharacterName()} for {strongShredValue} Vulnerable";
                moveText.color = attackTarget.GetCharacterColor();
                }
                break;

            case "WeakShred":
            if (attackTarget == null)
                    FindTarget();
            if (!CanAttackTarget())
                {
                    AdvanceAttack();
                }
            else if (!isBlindfolded){
                moveText.text = $"Shredding {attackTarget.GetCharacterName()} for {weakShredValue} Vulnerable";
                moveText.color = attackTarget.GetCharacterColor();
            }
                break;

            case "Strength":
                if (!isBlindfolded){

                
                moveText.text = $"Applying {buffValue} Strength to Self";
                moveText.color = this.GetEnemyColor();
                }
                break;
            case "Block":
            if (!isBlindfolded){
                moveText.text = $"Blocking for {blockValue}";
                moveText.color = this.GetEnemyColor();
            }
                break;

            case "Defend":
            if (!isBlindfolded){
                moveText.text = $"Defending for {firstDefend}";
                moveText.color = this.GetEnemyColor();
            }
                break;

            case "CombinedAttack":
            if (!isBlindfolded){
                string display = "Planning to ";
                for(int i = 0; i < combinedAttacks.Count; i++){
                    if(i == combinedAttacks.Count - 1)
                        display += $"{combinedAttacks[i]}";
                    else
                        display += $"{combinedAttacks[i]} and ";
                        }

                
   
                moveText.text = display; 
                moveText.color = this.GetEnemyColor();
            }
                break;

            default:
            if (!isBlindfolded){
                moveText.text = "Upcoming Move: " + attacks[currentAttack];
                moveText.color = this.GetEnemyColor();
            }
                break;
        }

}

protected virtual void SecondAttackPhase(){
    switch (secondaryAttacks[currentAttack])
        {
            case "Attack":
                if (attackTarget == null)
                    FindTarget();
                

                if (!CanAttackTarget())
                {
                    AdvanceAttack();
                }
                else if (!isBlindfolded)
                {
                    moveText.text = $"Attack {attackTarget.GetCharacterName()} for {attackValue} DMG";
                    moveText.color = attackTarget.GetCharacterColor();
   
                }
                break;

            case "StrongAttack":
                if (attackTarget == null)
                    FindTarget();
                

                if (!CanAttackTarget())
                {
                    AdvanceAttack();
                }
                else if (!isBlindfolded)
                {
                    moveText.text = $"Attack {attackTarget.GetCharacterName()} for {strongAttackValue} DMG";
                    moveText.color = attackTarget.GetCharacterColor();
   
                }
                break;
            case "WeakAttack":
                if (attackTarget == null)
                    FindTarget();
                

                if (!CanAttackTarget())
                {
                    AdvanceAttack();
                }
                else if (!isBlindfolded)
                {
                    moveText.text = $"Attack {attackTarget.GetCharacterName()} for {weakAttackValue} DMG";
                    moveText.color = attackTarget.GetCharacterColor();
   
                }
                break;

            case "AoEAttack":

            if (attackTarget == null)
                    FindTarget();
                if (!isBlindfolded)
                    moveText.text = $"Attacking party for {attackValue} DMG";
                break;

            case "ChargingUp":
            if (!isBlindfolded){
                moveText.text = $"Charging Up to Attack";
                moveText.color = this.GetEnemyColor();
            }
                break;

            case "StrongShred":
            if (attackTarget == null)
                    FindTarget();
                if (!CanAttackTarget())
                {
                    AdvanceAttack();
                }
                else if (!isBlindfolded){

                moveText.text = $"Applying {strongShredValue} Shred to {attackTarget.GetCharacterName()}";
                moveText.color = attackTarget.GetCharacterColor();
                }
                break;

            case "WeakShred":
            if (attackTarget == null)
                    FindTarget();
            if (!CanAttackTarget())
                {
                    AdvanceAttack();
                }
            else if (!isBlindfolded){
                moveText.text = $"Applying {weakShredValue} Shred to {attackTarget.GetCharacterName()}";
                moveText.color = attackTarget.GetCharacterColor();
            }
                break;

            case "Strength":
            if (!isBlindfolded){
                moveText.text = $"Applying {buffValue} Strength to Self";
                moveText.color = this.GetEnemyColor();
            }
                break;
            case "Block":
            if (!isBlindfolded){
                moveText.text = $"Blocking for {blockValue}";
                moveText.color = this.GetEnemyColor();
            }
                break;

            case "Defend":
                if(isSecondPhase && !isBlindfolded)
                    moveText.text = $"Defending for {secondDefend}";
 
            if (!isBlindfolded)
                moveText.color = this.GetEnemyColor();
                break;

            case "CombinedAttack":
             if (!isBlindfolded){

                string display = "Planning to ";
                for(int i = 0; i < combinedAttacks.Count; i++){
                    if(i == combinedAttacks.Count - 1)
                        display += $"{combinedAttacks[i]}";
                    else
                        display += $"{combinedAttacks[i]} and ";

                }
                
                moveText.text = display; 
                moveText.color = this.GetEnemyColor();
             }
                break;

            default:
                if (!isBlindfolded){

                moveText.text = "Upcoming Move: " + attacks[currentAttack];
                moveText.color = Color.white;
                }
                break;
        }

}

}
