//Ryan Lockie - 11/17/2024
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeirloomImageLibrary : MonoBehaviour
{
    //This list is used to store all of the sprites for heirloom visualization in order, from Taunt to Exploit, etc. (Index values must match expectations within HeirloomDisplay.cs)
    public List<Sprite> spriteLibrary = new List<Sprite>();
}
