using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//This Script was meant for the shop, but has since been phased out for constant descriptions
public class HoverShopItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private GameObject descriptionMenu;
    public void OnPointerEnter(PointerEventData eventData){
        Debug.Log("Pointer on Icon");
        descriptionMenu.SetActive(true);

    }
    public void OnPointerExit(PointerEventData eventData){
        descriptionMenu.SetActive(false);
        Debug.Log("Pointer off Icon");

    }
}
