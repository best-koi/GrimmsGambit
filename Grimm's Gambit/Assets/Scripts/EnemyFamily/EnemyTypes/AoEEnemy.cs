using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoEEnemy : EnemyPositionTarget
{

    //An Attack method to be used for honing in on specific positioned players
    protected override void Attack()
    {
            foreach(CharacterTemplate c in orderedCharacters){
                if(c == null)
                    continue;
                minion.MinionUsed(c.GetComponent<Minion>(), attackValue);
            }
                
        
    }

     protected override void CheckCurrentAttack()
    {
        switch (attacks[currentAttack])
        {
            case "Attack":
                moveText.text = $"Attacking party for {attackValue} DMG";

                break;

            case "Strength":
                moveText.text = $"Applying {buffValue} Strength to Self";
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
