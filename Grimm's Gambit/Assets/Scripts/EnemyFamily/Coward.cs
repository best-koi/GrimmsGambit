using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coward : EnemyPositionTarget
{


//Finds the weakest target in group
    protected override void FindPositionedTarget(int p)
    {
        List<Transform> characters = controller.GetPlayerInventory().GetAll();
        CharacterTemplate weakest = characters[0].GetComponent<CharacterTemplate>();
        foreach (Transform c in characters)
        {
            if (c != null && c.TryGetComponent<CharacterTemplate>(out CharacterTemplate ct) && ct.GetHP() < weakest.GetHP())
            {
                attackTarget = c.GetComponent<CharacterTemplate>();
                return;
            }
        }
    }

    protected override bool SeekNewTargetInOrder()
    {
        foreach (CharacterTemplate c in orderedCharacters)
        {
            if (c == attackTarget)
                continue;
            else if (c.GetHP() > 0)
            {
                attackTarget = c;
                return true;
            }
            else
            {
                continue;
            }
        }

        return false;
    }
}
