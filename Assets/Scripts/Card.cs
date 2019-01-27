using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{

    public enum Suit { MACHINE, DIP, VALUABLE, FOOD, TOY}
    public enum Type { NONE, MOVEMENT, TRADE, PASSIVE }

    public string cardName;
    public virtual Type cardType{
        get { return Type.NONE; }
    }
    public Suit cardSuit;
    public Sprite cardImage;
    public string cardFlavorText;
    public string cardAbilityText;

    //For trading costs and transactions.
    public int fuelCost;
    public CardPred[] cardCost;

    public GameObject halfSizeCard;
    public GameObject fullSizeCard;

    void Start()
    {
        
    }
    public bool Usable(int fuel, List<Card> hand)
    {
        //Check if we go over fuel cost.
        if (this.fuelCost > fuel)
        {
            Debug.Log("Not enough fuel. Trade card not used.");
            return false;
        }
        //Check if we go over card cost.
        if (this.cardCost.Length > hand.Count)
        {
            Debug.Log("Not enough cards. Trade card not used.");
            return false;
        }else{
            HashSet<Card> usedCards = new HashSet<Card>();
            foreach(var cost in this.cardCost){
                bool found = false;
                foreach(Card c in hand){
                    if(!usedCards.Contains(c) && cost.CanPayWith(c)){
                        usedCards.Add(c);
                        found = true;
                        break;
                    }
                }
                if(!found){
                    return false;
                }
            }
        }
        return true;
    }

    //public bool ownedByPlayer;
    //Ability cardAbility;
    //each UI card should have a Card datatype of the card itself

    // card cost objects
    public class CardPred{
        public virtual bool CanPayWith(Card c){
            return true;
        }
    }

    public class TypeCardPred : CardPred{
        public Type Type;
        public override bool CanPayWith(Card c){
            return c.cardType == Type;
        }
    }

    public class SuitCardPred : CardPred{
        public Suit Suit;
        public override bool CanPayWith(Card c){
            return c.cardSuit == Suit;
        }
    }

    public class SpecificCardPred : CardPred{
        public Card Card;
        public override bool CanPayWith(Card c){
            return c == Card;
        }
    }

}
