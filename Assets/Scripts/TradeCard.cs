using System.Collections.Generic;
using UnityEngine;

public class TradeCard : Card {

    public int fuelReward;
    public CardPred[] cardReward;

    public override Type cardType{
        get{ return Type.TRADE; }
    }

    new protected void Start(){
        base.Start();
    }

    public new virtual bool Usable(int fuel, List<Card> hand){
        int maxHandSize = 6; //TODO: WHAT THE FUCK IS THIS!!!!!!!!!!!
        if(!base.Usable(fuel, hand)){
            return false;
        }
        //Check if we go over max hand size with transaction.
        if (this.cardReward.Length + hand.Count - this.cardCost.Length > maxHandSize)
        {
            Debug.Log("Transaction would go over max hand size. Trade card not used.");
            return false;
        }
        return true;
    }


    // returns true in the total stated list of cards
    public virtual bool IsLegalTrade(int fuel, List<Card> give, List<Card> get){
        if(!Usable(fuel, give)){
            return false;
        }
        HashSet<CardPred> usedSlots = new HashSet<CardPred>();
        foreach(Card c in get){
            bool slotFound = false;
            foreach(CardPred p in cardReward){
                if(!usedSlots.Contains(p) && p.CanPayWith(c)){
                    usedSlots.Add(p);
                    slotFound = true;
                    break;
                }
            }
            if(!slotFound){
                return false;
            }
        }
        return true;
    }

}