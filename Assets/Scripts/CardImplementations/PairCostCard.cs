using System.Collections.Generic;

// mirror cube, pocket watch
public class PairCostCard : TradeCard{

    public int cardRewardCount;

    protected override void Start(){
        base.Start();
        this.cardCost = AnyTwo;
        this.cardReward = new CardPred[cardRewardCount];
        for(int i = 0; i < cardRewardCount; i++){
            this.cardReward[i] = new CardPred();
        }
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