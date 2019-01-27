using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityChoiceCardButton : MonoBehaviour
{
    public Card card;
    //name picture trade ability
    public Text cardName;
    public Image cardImage;
    public Text cardAbilityText;
    [HideInInspector] public NewCommerceManager commerceManager;

    void Start()
    {
        cardName.text = card.cardName;
        cardImage.sprite = card.cardImage;
        cardAbilityText.text = card.cardAbilityText;
    }
    
    

    public void TradeCardActivated()
    {
        Debug.Log("5: Trade Ability Card chosen.");
        commerceManager.SetAbilityChosen(true);
        commerceManager.SetChosenTradeCard(card);
    }
}
