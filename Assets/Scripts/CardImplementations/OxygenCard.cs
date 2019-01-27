/// DOES NOT WORK!!!!!!!!
public class OxygenCard : GearsCard {

    protected override void Start(){
        base.Start();
        this.cardCost = AnyOne;
        this.fuelCost = 2;
        this.hops = 5;
    }

}