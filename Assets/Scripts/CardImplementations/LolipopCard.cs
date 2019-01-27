public class LolipopCard : MovementCard{

    protected override void Start(){
        base.Start();
        this.fuelCost = 1;
        this.extraRefuelReward = 2;
    }
}