using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Draggable : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Vector2 originalPosition;
    public GameObject popUpWindow;
    public bool dragging;

    void Start()
    {
        originalPosition = this.transform.position;
        dragging = false;
    }
    
    //Handles dragging of Game Object in the UI with the mouse cursor.
    public void OnDrag(PointerEventData eventData)
    {
        dragging = true;
        this.transform.position = eventData.position;

        if (popUpWindow.activeSelf)
            popUpWindow.SetActive(false);
    }

    //Game Object returns to original position once dragging is complete.
    public void OnEndDrag(PointerEventData eventData)
    {
        /* if card is over another card, they swap places
         * else, card returns to its original position
        */
        dragging = false;
        this.transform.position = originalPosition;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("OnPointerOver");
        if (!dragging)
        {
            popUpWindow.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        popUpWindow.SetActive(false);
    }
}
