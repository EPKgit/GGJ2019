using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableCard : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Card card;
    public Vector2 originalPosition;
    public GameObject popUpWindow;
    public bool dragging;


    //Card Display Data
    public Text cardName;
    public Image cardImage;
    public Text cardAbilityText;
    public Text cardFlavorText;

    void Start()
    {
        originalPosition = this.transform.position;
        dragging = false;
    }

    public void LoadCard()
    {
        cardName.text = card.cardName;
        cardImage.sprite = card.cardImage;
        cardAbilityText.text = card.cardAbilityText;
        cardFlavorText.text = card.cardFlavorText;
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
