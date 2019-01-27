public class DollCard : TradeCard {

    protected override void Start(){
        base.Start();
        this.cardCost = OneValuable;
        this.cardReward = AnyOne;
        this.hopsReward = 1;
    }

}