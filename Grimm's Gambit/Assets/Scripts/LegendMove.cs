using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegendMove : MonoBehaviour
{
    [SerializeField] public float moveX;
    [SerializeField] public float moveY;
    private Vector3 showLocation;
    private Vector3 hideLocation;

    void Start()
    {
        showLocation = this.transform.localPosition + new Vector3(moveX, moveY, 0);
        hideLocation = this.transform.localPosition;
    }
    
    // Position the legend to be read
    public void Show()
    {
        this.transform.localPosition = showLocation;
    }

    // Position the legend to be "put away"
    public void Hide()
    {
        this.transform.localPosition = hideLocation;
    }
}
