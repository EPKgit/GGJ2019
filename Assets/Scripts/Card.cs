using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{

    public enum Suit { NONE, MACHINE, DIP, VALUABLE, FOOD, TOY}
    public enum Type { NONE, MOVEMENT, TRADE, PASSIVE }

    public static CardPred[] AnyOne = new CardPred[]{new CardPred()};
    public static CardPred[] AnyTwo = new CardPred[]{new CardPred(),new CardPred()};
    public static CardPred[] AnyThree = new CardPred[]{new CardPred(),new CardPred(),new CardPred()};
    public static CardPred[] OneValuable = new CardPred[]{new SuitCardPred{Suit=Suit.VALUABLE}};

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
    public int fuelReward;
    public int hopsReward; // EXTRA HOPS GIVEN AS A REWARD AFTERS PLAYING THIS CARD
    public int extraRefuelReward; 
    protected PlanetScouterAbility scouter;
    public Card CouldRefuelVersion; //USE THIS CARD INSTEAD IFF COULD REFULE AT CURRENT WORLD
    // DOES THIS CARD IMPOSE RESTRICTIONS ON YOUR NEXT PLANET ACTION
    public bool mayRefuelAfter = true;
    public bool mayTradeAfter = true; 

    public GameObject halfCard;

    public bool starterCard = false;

    protected virtual void Start()
    {
        CouldRefuelVersion = this;    
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


    // TO BE CALLED: AFTER RESOLVING EFFECT SUCCESSFULLY
    public void ProcScoutAbility(Planet here){
        scouter?.ScoutFrom(here);
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

    public class NotSuitCardPred : SuitCardPred{
        public override bool CanPayWith(Card c){
            return !base.CanPayWith(c);
        }
    }

    public class SpecificCardPred : CardPred{
        public Card Card;
        public override bool CanPayWith(Card c){
            return c == Card;
        }
    }

}
