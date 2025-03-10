using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DireWolf : EnemyRandomTarget
{

[SerializeField] protected List<EnemyTemplate> enemies;
//Finds enemies to buff who are wolves
    private void Howl(){
        EnemyTemplate[] allies = GameObject.FindObjectsOfType<EnemyTemplate>();
        
        foreach(EnemyTemplate a in allies)
        {
            a.GetComponent<Minion>().AddAffix(Affix.Strength, buffValue);
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

            case "Strength":
            if(!isBlindfolded){
                moveText.text = $"Applying {buffValue} Strength to Self";
                moveText.color = this.GetEnemyColor();
            }
                break;
            case "RandomAttack":
                if(hasChosenRandomAttack != true)
                    randomAttackName = randomAttacks[Random.Range(0, randomAttacks.Count)];
                if(randomAttackName == "Block")
                    hasChosenRandomAttack = true; 

                if(randomAttackName == "Block" && !isBlindfolded)
                    moveText.text = $"Blocking for {blockValue}";
                else
                {

                    
                    if(hasChosenRandomAttack != true){
                        FindTarget();
                    }
                    hasChosenRandomAttack = true; 
                    if(!isBlindfolded)
                        moveText.text = $"Attacking {attackTarget.GetCharacterName()} for {attackValue}";
                    
                }
                if(!isBlindfolded)
                    moveText.color = this.GetEnemyColor();
                break;
            case "Howl":
            if(!isBlindfolded){
                moveText.text = "Applying Strength to Allies";
                moveText.color = new Color(1.0f, 0.64f, 0.0f);
            }

            

            break;

            default:
                moveText.text = "Upcoming Move: " + attacks[currentAttack];
                moveText.color = Color.white;
                break;
        }
    }
}
