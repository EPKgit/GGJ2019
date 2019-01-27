using UnityEngine;

public class TeddyBearCard : TradeCard {

    protected bool prime = true;

    protected override void Start(){
        base.Start();
        this.cardCost = AnyTwo;
        this.cardReward = AnyTwo;
        if(prime){
            CouldRefuleTeddyBearCard crbc = gameObject.AddComponent(typeof(CouldRefuleTeddyBearCard)) as CouldRefuleTeddyBearCard;
            this.CouldRefuelVersion = crbc;
        }
    }

}

public class CouldRefuleTeddyBearCard : TeddyBearCard{

    protected override void Start(){
        this.prime = false;
        base.Start();
        this.cardReward = AnyOne;
    }

}