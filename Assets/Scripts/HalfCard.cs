using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HalfCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject prefabCard;
    public Card card;

    private float offset = 5f;

    void Start()
    {
        //card = prefabCard.GetComponent<Card>();
        transform.GetChild(1).GetComponent<Image>().sprite = prefabCard.GetComponent<Card>().cardImage;
    }

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
