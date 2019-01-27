using System.Collections.Generic;

class AddressBookCard : TradeCard {

    protected override void Start(){
        base.Start();
        this.cardCost = AnyTwo;
        this.cardReward = AnyTwo;
    }

    public override bool IsLegalTrade(int fuel, List<Card> give, List<Card> get){
        if(!base.IsLegalTrade(fuel, give, get)){
            return false;
        }
        // double check for no duplicate suits
        HashSet<Suit> usedSuits = new HashSet<Suit>();
        foreach(Card c in give){
            if(usedSuits.Contains(c.cardSuit)){
                return false;
            }
            usedSuits.Add(c.cardSuit);
        }
        foreach(Card c in get){
            if(usedSuits.Contains(c.cardSuit)){
                return false;
            }
            usedSuits.Add(c.cardSuit);
        }
        return true;
    }

}