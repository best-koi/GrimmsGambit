//Ryan Lockie - 11/16/2024
//The thought process behind this script is that it will recieve signals whenever something is added or removed from the affix list, so that constant dictionary checks are not necessary
//The expectation is also that this script is generalized for anything to use it, but functions have been added to the minion script so that it will call the add affix and remove affix functions here
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AffixDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddAffix(Affix newAffix) //Adds an affix to display
    {
        switch (newAffix) //Adds an image depending on the added affix to display
        {
            case Affix.Taunt:
                Debug.Log(".");
                break;
        }
    }

    public void RemoveAffix(Affix removedAffix) //Removes an affix from display
    {
        switch (removedAffix) //Removes image depending on affix
        {
            case Affix.Taunt:
                break;
        }
    }
}
