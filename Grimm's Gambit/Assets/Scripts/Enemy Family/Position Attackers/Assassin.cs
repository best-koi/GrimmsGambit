using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assassin : EnemyPositionTarget
{

[SerializeField]
private int secondaryAttackValue;

protected override void Attack()
    {
            FindPositionedTarget(position);
            if (attackTarget == null)
                SeekNewTargetInOrder();
            else
                minion.MinionUsed(attackTarget.GetComponent<Minion>(), attackValue);

        
    }

    protected void SecondaryAttack()
    {
      
            FindPositionedTarget(position);
            if (attackTarget == null)
                SeekNewTargetInOrder();
            else
                minion.MinionUsed(attackTarget.GetComponent<Minion>(), secondaryAttackValue);

        

    }
    
    protected void AttackBleed(){
        SecondaryAttack();
        attackTarget.GetComponent<Minion>().AddAffix(Affix.Bleed, buffValue);

    }

    protected void AttackGouge(){
        SecondaryAttack();
        //attackTarget.GetComponent<Minion>().AddAffix(Affix.Gouge, buffValue);
    }

     protected override void CheckCurrentAttack()
    {
        switch (attacks[currentAttack])
        {
            case "Attack":
                if (attackTarget == null)
                    FindPositionedTarget(position);

                if (!CanAttackTarget())
                {
                    if (orderedCharacters.Count == 0)
                        moveText.text = "Done Acting.";
                    else
                        AdvanceAttack();
                }
                else
                {
                   //Text display for buffs; second if is for non-buff
                        if (minion.currentAffixes.ContainsKey(Affix.Strength))
                            moveText.text = $"Attack {attackTarget.GetCharacterName()} for {attackValue + buffValue} DMG";
                        else
                            moveText.text = $"Attack {attackTarget.GetCharacterName()} for {attackValue} DMG";
                        moveText.color = attackTarget.GetCharacterColor();

                }

                break;

            case "Strength":
                moveText.text = $"Applying {buffValue} Strength to Self";
                moveText.color = this.GetEnemyColor();
                break;
             case "AttackBleed":
             if (attackTarget == null)
                    FindPositionedTarget(position);

            if (!CanAttackTarget())
                {
                    if (orderedCharacters.Count == 0)
                        moveText.text = "Done Acting.";
                    else
                        AdvanceAttack();
                }
                else
                {
                moveText.text = $"Attacking for {secondaryAttackValue} and applying {buffValue} Bleed to {attackTarget.GetCharacterName()}";
                moveText.color = this.GetEnemyColor();
                }
                break;
            case "AttackGouge":
            if (attackTarget == null)
                    FindPositionedTarget(position);
                    
                if (!CanAttackTarget())
                {
                    if (orderedCharacters.Count == 0)
                        moveText.text = "Done Acting.";
                    else
                        AdvanceAttack();
                }
                else
                {
                moveText.text = $"Attacking for {secondaryAttackValue} and applying {buffValue} Gouge to {attackTarget.GetCharacterName()}";
                moveText.color = this.GetEnemyColor();
                }
                break;
            case "Block":
                moveText.text = $"Blocking for {blockValue}";
                moveText.color = this.GetEnemyColor();
                break;

            case "CombinedAttack":
                string display = "Planning to ";
                for(int i = 0; i < combinedAttacks.Count; i++){
                    if(i == combinedAttacks.Count - 1)
                        display += $"{combinedAttacks[i]}";
                    else
                        display += $"{combinedAttacks[i]} and ";

                }
                    
                moveText.text = display; 
                moveText.color = this.GetEnemyColor();
                break;

            default:
                moveText.text = "Upcoming Move: " + attacks[currentAttack];
                moveText.color = Color.white;
                break;
        }


    }
}
