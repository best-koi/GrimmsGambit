using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHover : MonoBehaviour
{
    [SerializeField]
    private GameObject EnemyMove; 

    private void Start(){
        HideMove();
    }

    public void ShowMove(){
        EnemyMove.SetActive(true);
    }

    public void HideMove() {
        EnemyMove.SetActive(false);
    }

    public void OnMouseDown(){
        ShowMove();
    }

    public void OnMouseUp(){
        HideMove();
    }
}
