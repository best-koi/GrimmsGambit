using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
