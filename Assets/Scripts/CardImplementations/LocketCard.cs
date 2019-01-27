
public class LocketCard : TradeCard{
    protected override void Start(){
        base.Start();
        this.cardCost = new CardPred[]{new SpecificCardPred{Card = this}};
        this.cardReward = AnyTwo;
    }
}