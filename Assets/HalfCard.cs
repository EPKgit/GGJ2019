using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HalfCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Card card;

    private float offset = 5f;

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        BigCard.instance.setNewBigCard(card);
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        //Debug.Log("exit");
        //fullCard.transform.position = Vector3.one * 99999;
    }
}
