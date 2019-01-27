using UnityEngine;

public class BeerCard : TradeCard {

    protected bool prime = true;

    protected override void Start(){
        base.Start();
        this.cardCost = AnyTwo;
        this.cardReward = AnyOne;
        if(prime){
            CouldRefuleBeerCard crbc = gameObject.AddComponent(typeof(CouldRefuleBeerCard)) as CouldRefuleBeerCard;
            this.CouldRefuelVersion = crbc;
        }
        
    }

}

public class CouldRefuleBeerCard : BeerCard{

    protected override void Start(){
        this.prime = false;
        base.Start();
        this.fuelReward  = 6;
    }

}