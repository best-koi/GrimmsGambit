//Ryan Lockie - 11/17/2024
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AffixImageLibrary : MonoBehaviour
{
    //This list is used to store all of the sprites for affix visualization in order, from Taunt to Exploit, etc. (Index values must match expectations within AffixDisplay.cs)
    public List<Sprite> spriteLibrary = new List<Sprite>();
}
