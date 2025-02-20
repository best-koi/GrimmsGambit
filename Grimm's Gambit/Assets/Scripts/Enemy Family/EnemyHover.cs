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

    public void OnMouseEnter(){
        ShowMove();
    }

    public void OnMouseExit(){
        HideMove();
    }
}
