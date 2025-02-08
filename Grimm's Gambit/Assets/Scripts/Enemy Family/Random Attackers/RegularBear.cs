using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularBear : EnemyRandomTarget
{

    [Header("Bear Values")]
    [SerializeField]
    private int secondAttackValue, bleedValue, damageReductionValue, healValue, strengthValue;


    // Update is called once per frame
    protected void AttackAndBleed(){
        //Attacks
        minion.MinionUsed(attackTarget.GetComponent<Minion>(), attackValue);
        //Applies Bleed
        attackTarget.GetComponent<Minion>().AddAffix(Affix.Bleed, bleedValue);
       FindTarget();
    }

    protected virtual void DoubleAttack(){
        minion.MinionUsed(attackTarget.GetComponent<Minion>(), secondAttackValue * 2);
        FindTarget();
    }


    protected virtual void HealAndStrength(){
        //Heals self
            if (minion.currentHealth + healValue < minion.maxHealth)
                minion.currentHealth += healValue;
            else
                minion.currentHealth = minion.maxHealth;

            minion.AddAffix(Affix.Strength, strengthValue);
                }
            


    protected virtual void ProtectAndDebuff(){

        minion.AddAffix(Affix.Block, blockValue);

        foreach(CharacterTemplate c in targets){
                if(c == null)
                    continue;
                c.GetComponent<Minion>().AddAffix(Affix.DamageReduction, damageReductionValue);
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
                else
                {
                        if (minion.currentAffixes.ContainsKey(Affix.Strength))
                            moveText.text = $"Attack {attackTarget.GetCharacterName()} for {attackValue + strengthValue} DMG";
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
                else
                {
                        if (minion.currentAffixes.ContainsKey(Affix.Strength))
                            moveText.text = $"Attack {attackTarget.GetCharacterName()} for {secondAttackValue + buffValue} DMG twice";
                        else
                            moveText.text = $"Attack {attackTarget.GetCharacterName()} for {secondAttackValue} DMG twice";
                        moveText.color = attackTarget.GetCharacterColor();
   
                }

                break;


            case "AttackAndBleed":
                if (attackTarget == null)
                    FindTarget();

                    if (minion.currentAffixes.ContainsKey(Affix.Strength))
                            moveText.text = $"Attack {attackTarget.GetCharacterName()} for {attackValue + strengthValue} and Inflicting Bleed for {bleedValue}";
                        else
                            moveText.text = $"Attacking {attackTarget.GetCharacterName()} for {attackValue} and Inflicting Bleed for {bleedValue}";

                
                break;

            case "ProtectAndDebuff":
                moveText.text = $"Defending for {blockValue} and Applying {damageReductionValue} Damage Reduction to Party";
                moveText.color = this.GetEnemyColor();
                break;

            case "HealAndStrength":
                moveText.text = $"Healing and Applying Strength";
                moveText.color = this.GetEnemyColor();
                break;

 

            case "Block":
                moveText.text = $"Blocking for {blockValue}";
                moveText.color = this.GetEnemyColor();
                break;

            default:
                moveText.text = "Upcoming Move: " + attacks[currentAttack];
                moveText.color = Color.white;
                break;
        }
    }
}
