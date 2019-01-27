public class JewelryBoxCard : TradeCard{

    protected override void Start(){
        base.Start();
        this.cardCost = AnyOne;
        this.cardReward = AnyOne;
        this.scouter = new PlanetScouterAbility(2,1);
        // this is not exactly how the ability was worded, but eh
    }

}