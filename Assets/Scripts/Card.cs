using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public string cardName;
    public string cardType;
    public string cardSuit;
    public Sprite cardImage;
    public string cardFlavorText;
    public string cardAbilityText;

    //For trading costs and transactions.
    public int fuelCost;
    public int cardCost;
    public int fuelReward;
    public int cardReward;

    public GameObject halfSizeCard;
    public GameObject fullSizeCard;

    void Start()
    {
        
    }
    //public bool ownedByPlayer;
    //Ability cardAbility;
    //each UI card should have a Card datatype of the card itself
}
