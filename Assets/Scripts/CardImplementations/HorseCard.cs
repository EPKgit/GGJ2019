
public class HorseCard : MovementCard{

    protected override void Start(){
        base.Start();
        this.fuelCost = 2;
        this.DoesTp = true;
    }

    public override bool CanTpTo(Planet from, Planet to){
        return to.isStart;
    }

}