using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoEEnemy : EnemyRandomTarget
{

    //An Attack method to be used for honing in on specific positioned players
    protected override void Attack()
    {
            foreach(CharacterTemplate c in targets){
                if(c == null)
                    continue;
                minion.MinionUsed(c.GetComponent<Minion>(), attackValue);
            }
                
        
    }

    private void AOEDefend(){
        EnemyTemplate[] allies = GameObject.FindObjectsOfType<EnemyTemplate>();
        
        foreach(EnemyTemplate a in allies)
        {
            a.GetComponent<Minion>().AddAffix(Affix.Block, buffValue);
        }

    }

     protected override void CheckCurrentAttack()
    {
        switch (attacks[currentAttack])
        {
            case "Attack":

            if (attackTarget == null)
                    FindTarget();
                if(!isBlindfolded)
                moveText.text = $"Attacking party for {attackValue} DMG";

                break;

            case "Strength":
                if(!isBlindfolded){
                moveText.text = $"Applying {buffValue} Strength to Self";
                moveText.color = this.GetEnemyColor();
                }
                break;
            case "Block":
            if(!isBlindfolded){
                moveText.text = $"Blocking for {blockValue}";
                moveText.color = this.GetEnemyColor();
            }
                break;
            case "AOEDefend":
            if(!isBlindfolded){
                moveText.text = "Defending Allies";
                moveText.color = new Color(1.0f, 0.64f, 0.0f);
            }
                break;

            case "CombinedAttack":
                string display = "Planning to ";
                for(int i = 0; i < combinedAttacks.Count; i++){
                    if(i == combinedAttacks.Count - 1)
                        display += $"{combinedAttacks[i]}";
                    else
                        display += $"{combinedAttacks[i]} and ";

                }
                if(!isBlindfolded){
                moveText.text = display; 
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
