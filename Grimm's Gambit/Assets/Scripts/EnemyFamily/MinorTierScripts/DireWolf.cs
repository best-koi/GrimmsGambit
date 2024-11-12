using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DireWolf : EnemyRandomTarget
{

[SerializeField] protected List<EnemyTemplate> enemies;
//Finds enemies to buff who are wolves
    private void Howl(){
        List<GameObject> spawners = controller.GetEnemyInventory().GetAllMembers();
        foreach(GameObject s in spawners){
            enemies.Add(s.GetComponent<EnemySpawner>().GetSpawnedEnemy().GetComponent<EnemyTemplate>());

        }
        
        foreach(EnemyTemplate e in enemies){
            if(e == this)
                continue;
            else if(e.GetEnemyName().Contains("Wolf")){
                e.GetComponent<Minion>().AddAffix(Affix.Strength, buffValue);
            }
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
            case "RandomAttack":
                if(randomAttackName == "Block")
                    moveText.text = $"Blocking for {blockValue}";
                else
                    moveText.text = $"Attacking for {attackValue}";
                moveText.color = this.GetEnemyColor();
                break;

            default:
                moveText.text = "Upcoming Move: " + attacks[currentAttack];
                moveText.color = Color.white;
                break;
        }


    }

}
