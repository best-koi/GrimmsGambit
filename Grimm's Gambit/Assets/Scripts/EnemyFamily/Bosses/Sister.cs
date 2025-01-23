using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sister : Lycan
{
    protected static int randomStartAttack;//A static variable that should match for both sisters 
    
/*The Sister Script will function similar to the Lycan with some adjustments
A static variable will be used to ensure both sisters start on the same attack number
*/

    // Start here is used to sync up the sister attack patterns
    protected override void Start()
    {
        randomStartAttack = Random.Range(0, attacks.Count);
        currentAttack = randomStartAttack;
        
    }

   
}
