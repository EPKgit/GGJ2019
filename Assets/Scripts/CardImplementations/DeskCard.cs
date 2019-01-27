public class DeskCard : MovementCard{

    protected override void Start(){
        base.Start();
        this.cardCost = AnyOne;
        this.hops = 2;
    }

}