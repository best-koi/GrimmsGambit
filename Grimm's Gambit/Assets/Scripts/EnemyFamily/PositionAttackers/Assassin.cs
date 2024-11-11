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
            if (positionTarget == null)
                SeekNewTargetInOrder();
            else
                minion.MinionUsed(positionTarget.GetComponent<Minion>(), attackValue);

        
    }

    protected void SecondaryAttack()
    {
      
            FindPositionedTarget(position);
            if (positionTarget == null)
                SeekNewTargetInOrder();
            else
                minion.MinionUsed(positionTarget.GetComponent<Minion>(), secondaryAttackValue);

        

    }
    
    protected void AttackBleed(){
        SecondaryAttack();
        positionTarget.GetComponent<Minion>().AddAffix(Affix.Bleed, buffValue);

    }

    protected void AttackGouge(){
        SecondaryAttack();
        //positionTarget.GetComponent<Minion>().AddAffix(Affix.Gouge, buffValue);
    }

     protected override void CheckCurrentAttack()
    {
        switch (attacks[currentAttack])
        {
            case "Attack":
                if (positionTarget == null)
                    FindPositionedTarget(position);

                if (!CanAttackTarget())
                {
                    if (attacks.Count == 1)
                        moveText.text = "Done Acting.";
                    else
                        AdvanceAttack();
                }
                else
                {
                   //Text display for buffs; second if is for non-buff
                        if (minion.currentAffixes.ContainsKey(Affix.Strength))
                            moveText.text = $"Attack {positionTarget.GetCharacterName()} for {attackValue + buffValue} DMG";
                        else
                            moveText.text = $"Attack {positionTarget.GetCharacterName()} for {attackValue} DMG";
                        moveText.color = positionTarget.GetCharacterColor();

                }

                break;

            case "Strength":
                moveText.text = $"Applying {buffValue} Strength to Self";
                moveText.color = this.GetEnemyColor();
                break;
             case "AttackBleed":
             if (positionTarget == null)
                    FindPositionedTarget(position);
                moveText.text = $"Attacking for {secondaryAttackValue} and applying {buffValue} Bleed to {positionTarget.GetCharacterName()}";
                moveText.color = this.GetEnemyColor();
                break;
            case "AttackGouge":
            if (positionTarget == null)
                    FindPositionedTarget(position);
                moveText.text = $"Attacking for {secondaryAttackValue} and applying {buffValue} Gouge to {positionTarget.GetCharacterName()}";
                moveText.color = this.GetEnemyColor();
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
