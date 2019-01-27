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
        switch(prefabCard.GetComponent<Card>().cardSuit)
        {
            case Card.Suit.DIP: 
                transform.GetChild(2).GetComponent<Image>().color = Color.blue;
                break;
            case Card.Suit.FOOD: 
                transform.GetChild(2).GetComponent<Image>().color = Color.green;
                break;
            case Card.Suit.MACHINE: 
                transform.GetChild(2).GetComponent<Image>().color = Color.red;
                break;
            case Card.Suit.TOY: 
                transform.GetChild(2).GetComponent<Image>().color = Color.magenta;//new Color(164f, 122f, 211f);
                break;
            case Card.Suit.VALUABLE: 
                transform.GetChild(2).GetComponent<Image>().color = Color.yellow;
                break;
        }
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
