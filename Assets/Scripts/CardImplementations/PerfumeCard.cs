public class PerfumeCard : TradeCard{

    protected override void Start(){
        base.Start();
        this.cardCost = new CardPred[]{new SpecificCardPred{Card = this}};
        this.cardReward = AnyOne;
        this.fuelReward = 3;
    }

}