using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{

    public enum Suit { MACHINE, DIP, VALUABLE, FOOD, TOY}

    public string cardName;
    public string cardType;
    public Suit cardSuit;
    public Sprite cardImage;
    public string cardFlavorText;
    public string cardAbilityText;

    //For trading costs and transactions.
    public int fuelCost;
    public CostCost[] cardCost;
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

    // card cost objects
    public class CardCost{
        public virtual bool CanPayWith(Card c){
            return true;
        }
    }

    public class SuitCardCost : CardCost{
        public Suit Suit;
        public override bool CanPayWith(Card c){
            return c.cardSuit == Suit;
        }
    }

    public class SpecificCardCost : CardCost{
        public Card Card;
        public override bool CanPayWith(Card c){
            return c == Card;
        }
    }

}
