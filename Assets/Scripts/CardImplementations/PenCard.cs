public class PenCard : TradeCard{
    protected override void Start(){
        base.Start();
        this.cardCost = AnyOne;
        this.fuelCost = 1; // will fight unity
        this.cardReward = AnyOne;
    }
}