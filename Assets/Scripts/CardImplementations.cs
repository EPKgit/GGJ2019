using System.Collections.Generic;

public class PenCard : TradeCard{
    new protected void Start(){
        base.Start();
        this.cardCost = new CardPred[]{new CardPred()};
        this.fuelCost = 1; // will fight unity
        this.cardReward = new CardPred[]{new CardPred()};
    }
}

public class LocketCard : TradeCard{
    new protected void Start(){
        base.Start();
        this.cardCost = new CardPred[]{new SpecificCardPred{Card = this}};
        this.cardReward = new CardPred[]{new CardPred(), new CardPred()};
    }
}

// mirror cube, pocket watch
public class PairCostCard : TradeCard{
    new protected void Start(){
        base.Start();
        this.cardCost = new CardPred[]{new CardPred(), new CardPred()};
    }

    public override bool Usable(int fuel, System.Collections.Generic.List<Card> hand){
        HashSet<Card.Suit> seenSuits = new HashSet<Suit>();
        foreach(Card c in hand){
            if(seenSuits.Contains(c.cardSuit)){
                return base.Usable(fuel, hand); 
            }
            seenSuits.Add(c.cardSuit);
        }
        return false; // never saw a suit twice
    }
}

public class MirrorCudeCard : PairCostCard{

    new protected void Start(){
        base.Start();
        this.cardReward = new CardPred[]{new CardPred(), new CardPred(), new CardPred()};
    }

}

public class PocketWatchCard : PairCostCard{

    new protected void Start(){
        base.Start();
        this.cardReward = new CardPred[]{new CardPred(), new CardPred()};
    }
}