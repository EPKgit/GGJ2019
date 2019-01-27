public class ChessCard : TradeCard {

    protected override void Start(){
        base.Start();
        this.cardCost = OneValuable;
        this.cardReward = AnyTwo;
    }

}