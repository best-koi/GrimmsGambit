using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sister : Lycan
{
    protected static int randomStartAttack;//A static variable that should match for both sisters 
    


    // Start is called before the first frame update
    protected override void Start()
    {
        randomStartAttack = Random.Range(0, attacks.Count);
        currentAttack = randomStartAttack;
        
    }

   
}
