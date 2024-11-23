using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDict : MonoBehaviour
{
    [SerializeField] private GenericDict<Affix, int> affixes;

    void Start()
    {
        // Initialize the dictionary to a length of five
        affixes = new GenericDict<Affix, int>(5);

        // Set key at index 0 to thorns
        affixes.SetKey(Affix.Thorns);

        // Set key at index 1 to strength
        affixes.SetKey(Affix.Strength, 1);
        

        // Set the value of the thorns affix to three 
        affixes.SetValue(Affix.Thorns, 3);

        // Set the value of the strength affix to five 
        affixes.SetValue(Affix.Strength, 5);


        Debug.Log("Log the affix's key at index 0: " + affixes.GetKey());
        Debug.Log("Log the affix's key at index 1: " + affixes.GetKey(1));

        Debug.Log("Log the affix value of thorns: " + affixes.GetValue(Affix.Thorns));
        Debug.Log("Log the affix value of strength: " + affixes.GetValue(Affix.Strength));
    }

    void Update()
    {
        
    }
}
