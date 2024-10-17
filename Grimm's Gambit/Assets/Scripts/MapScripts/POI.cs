using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POI : MonoBehaviour
{
    [SerializeField]
    private List<Color> testColors;

    [SerializeField]
    private Renderer r;

    // Start is called before the first frame update
    void Start()
    {
        r.material.SetColor("_Color",testColors[Random.Range(0, testColors.Count)]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
