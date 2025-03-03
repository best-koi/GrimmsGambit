using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingDummy : EnemyRandomTarget
{
    protected override void Update()
    {
        minion.currentHealth = minion.maxHealth;
        base.Update();
        
    }
}
