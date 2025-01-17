using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    public GameObject self;

    [SerializeField]
    public GameObject healthFill;

    [SerializeField]
    public Color full;

    [SerializeField]
    public Color mid;

    [SerializeField]
    public Color low;

    private int currentHealth;
    private int maxHealth;
    
    // Start is called before the first frame update
    void Start()
    {
        SetColor(full);
        HealthUpdate();
    }

    // Update is called once per frame
    void Update()
    {
        HealthUpdate();
        ColorRange();
        // FillSize();
    }

    void HealthUpdate()
    {
        currentHealth = self.GetComponent<BasicCharacter>().GetHP();
        maxHealth = self.GetComponent<BasicCharacter>().GetMaxHP();
    }

    void SetColor(Color c)
    {
        healthFill.GetComponent<Image>().color = c;
    }

    void ColorRange()
    {
        // percentage of health remaining
        float percent = (float)currentHealth / maxHealth;
        
        if (percent >= 0.75f)
        {
            SetColor(full);
        }
        else if (percent >= 0.5f && percent < 0.75f)
        {
            SetColor(mid);
        }
        else SetColor(low);
    }

    // // will attempt to make work
    // void FillSize()
    // {

    // }
}
