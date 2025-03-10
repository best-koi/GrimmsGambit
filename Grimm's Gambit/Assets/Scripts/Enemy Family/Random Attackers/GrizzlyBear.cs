using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrizzlyBear : RegularBear
{

[SerializeField]
    private int vulnerableValue;
   private void AOEDefend(){
        EnemyTemplate[] allies = GameObject.FindObjectsOfType<EnemyTemplate>();
        
        foreach(EnemyTemplate a in allies)
        {
            a.GetComponent<Minion>().AddAffix(Affix.Block, buffValue);
        }

    }

    private void AOEVulnerable(){
        foreach(CharacterTemplate c in targets){
                if(c == null)
                    continue;
                c.GetComponent<Minion>().AddAffix(Affix.Vulnerable, vulnerableValue);
            }

    }

    protected void AOEAttackAndBleed(){
        //Attacks
        foreach(CharacterTemplate c in targets){
                if(c == null)
                    continue;
                minion.MinionUsed(c.GetComponent<Minion>(), attackValue);
                 c.GetComponent<Minion>().AddAffix(Affix.Bleed, bleedValue);

            }
       FindTarget();
    }

    protected virtual void StrengthAndDebuff(){

        minion.AddAffix(Affix.Strength, strengthValue);

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
            case "AOEAttackAndBleed":
                if (attackTarget == null)
                    FindTarget();

                    if(!isBlindfolded)
                    moveText.text = $"Attacking and Bleeding Party";

                
                break;

            case "StrengthAndDebuff":
            if(!isBlindfolded){
                moveText.text = $"Applying Damage Reduction to Party and Strengthening Self";
                moveText.color = this.GetEnemyColor();
            }
                break;

            case "AOEDefend":
            if(!isBlindfolded){
                moveText.text = "Defending Allies";
                moveText.color = new Color(1.0f, 0.64f, 0.0f);
            }
                break;
            
            case "AOEVulnerable":
            if(!isBlindfolded){
                moveText.text = $"Applying Vulnerable to Party";
                moveText.color = new Color(1.0f, 0.64f, 0.0f);
            }
                break;


            case "Block":
            if(!isBlindfolded){
                moveText.text = $"Blocking";
                moveText.color = this.GetEnemyColor();
            }
                break;

            default:
            if(!isBlindfolded){
                moveText.text = "Upcoming Move: " + attacks[currentAttack];
                moveText.color = Color.white;
            }
                break;
        }
    }


}
