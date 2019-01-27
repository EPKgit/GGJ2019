public class GeoBookCard : MovementCard{

    protected virtual void Start(){
        base.Start();
        this.fuelCost = 1;
        this.hops = 1;
        this.scouter = new PlanetScouterAbility(10, 1);
    }

}