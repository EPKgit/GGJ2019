public class TypeAndTwoCard : TradeCard {

    public Type Type;

    protected override void Start(){
        base.Start();
        this.cardCost = AnyOne;
        this.cardReward = new CardPred[]{new TypeCardPred{Type = Type}};
        this.fuelReward = 2;
    }

}