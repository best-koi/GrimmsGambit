using UnityEngine;
using TMPro; 

public class DescriptionPopUp : MonoBehaviour
{
    [SerializeField]
    private GameObject popup;//A popup to display info

    [SerializeField]
    private TMP_Text displayText; 

    static GameObject staticPopup;

    static TMP_Text staticText; 


    // Start is called before the first frame update
    void Start()
    {
        staticText = displayText;
        staticPopup = popup;
        staticPopup.SetActive(false);
    }


    public static void ActivateText(string name, string description){
        staticPopup.SetActive(true);
        staticText.text = $"{name}: {description}"; 
    }

    public static void HidePopup(){
        staticPopup.SetActive(false);
    }
}
