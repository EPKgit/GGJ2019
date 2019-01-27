using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TradingPhaseCard : MonoBehaviour
{
    [HideInInspector] public string owner;
    public Card card;
    public Text cardName;
    public Image cardImage;
    public Text cardAbilityText;



    void Start()
    {
        cardName.text = card.cardName;
        cardImage.sprite = card.cardImage;
        cardAbilityText.text = card.cardAbilityText;
    }
}
