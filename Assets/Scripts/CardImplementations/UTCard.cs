public class UTCard : TradeCard{

    public int PlanetsScouted = 1;
    public int CardsRevealed = 6;

    protected override void Start(){
        base.Start();
        this.cardCost = OneValuable;
        this.cardReward = AnyOne;
        this.scouter = new PlanetScouterAbility(PlanetsScouted, CardsRevealed);
    }

}