public class MapCard : MovementCard{
    
    protected override void Start(){
        base.Start();
        this.cardCost = AnyOne;
        this.fuelCost = 1;
        this.hops = 3;
    }

    protected override bool canMoveThrough(Planet p){
        foreach(var c in p.trades){
            if(c.revealed){
                return true;
            }
        }
        return false;
    }

}