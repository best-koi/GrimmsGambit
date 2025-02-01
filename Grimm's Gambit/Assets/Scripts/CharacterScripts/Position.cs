using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position : MonoBehaviour
{
    [SerializeField]
    private string positionName;

    public string GetPosition()
    {
        return positionName;
    }
}
