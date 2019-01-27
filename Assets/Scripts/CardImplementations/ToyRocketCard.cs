
public class ToyRocketCard : MovementCard{

    protected override void Start(){
        base.Start();
        this.fuelCost = 1;
        this.hops = 3;
        this.mayRefuelAfter = false;
    }

}