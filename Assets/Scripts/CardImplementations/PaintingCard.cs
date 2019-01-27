public class FanctyPainting : TradeCard{

    protected override void Start(){
        this.cardCost = AnyOne;
        this.fuelCost = 1;
        this.cardReward = AnyTwo;
    }

    public override bool IsLegalTrade(int fuel, System.Collections.Generic.List<Card> give, System.Collections.Generic.List<Card> get){
        if(!base.IsLegalTrade(fuel, give, get)){
            return false;
        }
        return get[0].cardSuit == get[1].cardSuit;
    }

}