public class LobsterCard : TradeCard{

    protected override void Start(){
        base.Start();
        this.cardCost = OneValuable;
        this.cardReward = new CardPred[]{new NotSuitCardPred{Suit = Suit.VALUABLE}};
        this.fuelReward = 4;
    }

    

}