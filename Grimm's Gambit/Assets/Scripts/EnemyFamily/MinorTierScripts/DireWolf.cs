using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DireWolf : EnemyAggressiveRandom
{
//Finds enemies to buff who are wolves
    private void Howl(){
        List<GameObject> enemies = controller.GetEnemyInventory().GetAllMembers();
        foreach(GameObject e in enemies){
            if(e == this)
                continue;
            else if(e.GetComponent<EnemyTemplate>().GetEnemyName() == "Wolf"){
                e.GetComponent<Minion>().AddAffix(Affix.Strength, buffValue);
            }
        }

    }

}
