public class CosmiflowerCard : TradeCard {

    protected override void Start(){
        base.Start();
        this.cardCost = AnyOne;
        this.fuelCost = 2;
        this.cardReward = AnyTwo;
    }

}