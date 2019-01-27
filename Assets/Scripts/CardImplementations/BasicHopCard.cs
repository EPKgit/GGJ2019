public class BasicHopCard : MovementCard{

    public int FuelCost;
    public int Hops;

    protected override void Start(){
        base.Start();
        this.fuelCost = FuelCost;
        this.hops = Hops;
    }
}