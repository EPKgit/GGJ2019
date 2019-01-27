
public class FingureTrapCard : MovementCard{

    protected override void Start(){
        base.Start();
        this.fuelCost = 6;
        this.DoesHop = false;
        this.DoesTp = true;
    }

    public override bool CanTpTo(Planet from, Planet to){
        return true;
    }

}