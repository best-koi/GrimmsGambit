using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    static MapPlayer player;

    void Start()
    {
        player = FindObjectOfType<MapPlayer>();
    }

    public static MapPlayer GetPlayer()
    {
        return player; 
    }
}
