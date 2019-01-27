
public class GearsCard : MovementCard{

    protected override void Start(){
        base.Start();
        this.fuelCost = 3;
        this.hops = 4;
    }

    protected override bool canMoveTo(Planet p){
        return p.Suit == Suit.NONE;
    }
    
    protected override bool canMoveThrough(Planet p){
        return p.Suit == Suit.NONE;
    }

}