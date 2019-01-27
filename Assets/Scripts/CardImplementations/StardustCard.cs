public class StardustCard : MovementCard{

    protected override void Start(){
        base.Start();
        this.fuelCost = 0;
        this.hops = 100;
        this.mayTradeAfter = false;
    }

    protected override bool canMoveThrough(Planet p){
        return !p.canRefuel;
    }

}