using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableCard : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler, IDropHandler
{
    [HideInInspector] public string cardOwner;
    [HideInInspector] public Card card;
    [HideInInspector] public Vector2 originalPosition;
    [HideInInspector] public CommerceManager commerceManager;
    [HideInInspector] public GameObject popUpWindow;
    [HideInInspector] public bool beingDragged;


    //Card Display Data
    public Text cardName;
    public Image cardImage;
    public Text cardAbilityText;
    public Text cardFlavorText;

    void Start()
    {
        originalPosition = this.transform.position;
        beingDragged = false;
    }

    public void LoadCard()
    {
        cardName.text = card.cardName;
        cardImage.sprite = card.cardImage;
        cardAbilityText.text = card.cardAbilityText;
        cardFlavorText.text = card.cardFlavorText;
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        beingDragged = true;

        if (popUpWindow.activeSelf)
            popUpWindow.SetActive(false);

        this.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    //Handles dragging of Game Object in the UI with the mouse cursor.
    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = eventData.position;
    }

    //Game Object returns to original position once dragging is complete.
    public void OnEndDrag(PointerEventData eventData)
    {
        /* if card is over another card, they swap places
         * else, card returns to its original position
        */
        beingDragged = false;

        this.GetComponent<CanvasGroup>().blocksRaycasts = true;
        this.transform.position = originalPosition;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("OnPointerOver");
        if (!eventData.dragging)
        {
            popUpWindow.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        popUpWindow.SetActive(false);
    }

    public void OnDrop(PointerEventData eventData)
    {
        //Swapping
        if (!beingDragged)
        {
            Debug.Log("Swapping " + eventData.pointerDrag.GetComponent<DraggableCard>().cardName.text + " with " + cardName.text);

            Vector2 temp = this.originalPosition;

            this.originalPosition = eventData.pointerDrag.GetComponent<DraggableCard>().originalPosition;
            eventData.pointerDrag.GetComponent<DraggableCard>().originalPosition = temp;
            this.transform.position = this.originalPosition;

            //Checks if you're just swapping around in the hand or not
            if (this.cardOwner != eventData.pointerDrag.GetComponent<DraggableCard>().cardOwner)
            {
                Player player = commerceManager.player;
                Planet planet = commerceManager.currentPlanet;

                //int playerCardIndex = player.playerHand.FindIndex()

                //Action has now been taken this turn
                Debug.Log("Action has been taken.");
                //commerceManager.ActionTaken();
            }
            else
            {
                Debug.Log("Same ownership. Action has not been taken.");
            }
            
        }
    }
}
