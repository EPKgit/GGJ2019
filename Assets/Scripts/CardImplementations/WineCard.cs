
public class WineCard : MovementCard {

    protected override void Start(){
        base.Start();
        this.fuelCost = 3;
        this.DoesHop = false;
        this.DoesTp = true;
    }

    public override bool CanTpTo(Planet from, Planet to){
        return to.Suit == Suit.VALUABLE;
    }
    
}
        