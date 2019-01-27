public class TradeCard : Card {

    public int fuelReward;
    public CardPred[] cardReward;

    new void Start(){
        base.Start();
        this.cardType = Type.TRADE;
    }

    public new bool Usable(int fuel, List<Card> hand){
        int maxHandSize = 6; //TODO: WHAT THE FUCK IS THIS!!!!!!!!!!!
        if(!base.Usable(fuel, hand)){
            return false;
        }
        //Check if we go over max hand size with transaction.
        if (tradeCard.cardReward + hand.Count - tradeCard.cardCost > maxHandSize)
        {
            Debug.Log("Transaction would go over max hand size. Trade card not used.");
            return false;
        }
        return true;
    }

}