public class KettleCard : TradeCard{

    protected override void Start(){
        base.Start();
        this.cardCost = AnyOne;
        this.cardReward = AnyTwo;
    }

    public override bool IsLegalTrade(int fuel, System.Collections.Generic.List<Card> give, System.Collections.Generic.List<Card> get){
        if(!base.IsLegalTrade(fuel, give, get)){
            return false;
        }
        Suit suit = give[0].cardSuit;
        foreach(Card c in get){
            if(c.cardSuit != suit){
                return false;
            }
        }
        return true;
    }

}