using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndingHandler : MonoBehaviour
{

    [TextArea(3, 20)] [SerializeField]
    private string heirGoodEnding1, heirBadEnding1, seamstressGoodEnding, seamstressBadEnding, katzeGoodEnding, katzeBadEnding, houndGoodEnding, houndBadEnding, heirGoodEnding2, heirBadEnding2;

    [SerializeField]
    private TMP_Text endingText;

    [SerializeField]
    private GameObject theEndButton; 

    private string heirEnding1, seamstressEnding, katzeEnding, houndEnding, heirEnding2;//Strings to set based on choices

    private List<string> endingsList = new List<string>();//a list of strings containing the endings

    private int index = 0;//an index to cycle through the endings


    
    
    // Start is called before the first frame update
    void Start()
    {
        //Add these to list once assigned based on choices 
        endingsList.Add(heirEnding1);
        endingsList.Add(seamstressEnding);
        endingsList.Add(katzeEnding);
        endingsList.Add(houndEnding);
        endingsList.Add(heirEnding2);
    }

    void Update(){
        endingText.text = endingsList[index]; 
    }

//Cycles ending forward
    public void AdvancePlayerEnding(){
        if(index < endingsList.Count)
            index++;
        
    }



    
}
