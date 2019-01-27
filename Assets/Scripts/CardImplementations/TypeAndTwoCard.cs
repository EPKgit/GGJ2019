public class TypeAndTwoCard : TradeCard {

    public Type type;

    protected override void Start(){
        base.Start();
        this.cardCost = AnyOne;
        this.cardReward = new CardPred[]{new TypeCardPred{Type = type}};
        this.fuelReward = 2;
    }

}