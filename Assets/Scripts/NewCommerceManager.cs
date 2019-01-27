using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewCommerceManager : MonoBehaviour
{
    public Player player;
    public PlanetSideManager planetFix;
    [HideInInspector] public Planet planet;

    //For ability choosing phase.
    //public GameObject AbilityChoiceMenu;
    [HideInInspector] public bool abilityChosen;
    [HideInInspector] public TradeCard chosenTradeCard;

    //For ability chosen set-up phase.
    [HideInInspector] public int cardsLosing;
    [HideInInspector] public int cardsGaining;
    private bool abilityChoosingComplete;
    private bool readyForTradeSetup;

    //For trading menu set-up phase.
    private bool tradeMenuReady;
    //public GameObject TradingMenu;
    //public Image landscape;
    public GameObject playerTradeHand;
    public GameObject planetTradeHand;

    public List<Card> toPlayer;
    public List<Card> toPlanet;

    public int refuelValue = 4;

    IEnumerator Start()
    {
        abilityChosen = false;
        abilityChoosingComplete = false;
        readyForTradeSetup = false;
        tradeMenuReady = false;

        /*
        if (AbilityChoiceMenu.activeSelf == true)
        {
            AbilityChoiceMenu.SetActive(false);
        }
        */

        /*
        if (TradingMenu.activeSelf == true)
        {
            TradingMenu.SetActive(false);
        }
        */

        yield return new WaitUntil(() => PlayerMovement.instance != null);
        yield return new WaitUntil(() => PlayerMovement.instance.currentPlanet != null);
        planet = PlayerMovement.instance.currentPlanet;

        AbilityChoicePhase();
        SetUpTradeMenu();
    }

    private void Update()
    {

    /*
        //Player chooses ability.
        if (abilityChosen == true && abilityChoosingComplete == false && readyForTradeSetup == false)
        {
            Debug.Log("6: Going into trade set-up phase.");
            TradeAbilityChosenPhase();
        }
        else if (abilityChosen == false && abilityChoosingComplete == true  && readyForTradeSetup == false)
        {
            Debug.Log("Special Case: No trade cards found. Going to trade menu.");
            cardsLosing = 0;
            cardsGaining = 0;
            readyForTradeSetup = true;
        }
        
        //Set up trade menu once ability choosing is complete.
        if (readyForTradeSetup == true && tradeMenuReady == false)
        {
            Debug.Log("9: Setting up trade menu.");
            SetUpTradeMenu();
        }
    */

    }


    public void SetUpTradeMenu()
    {
        //Debug.Log("10: Setting up landscape.");
        //landscape.sprite = planet.background;
        //TradingMenu.SetActive(true);


        /*
        //Preparing player trade hand.
        Debug.Log("11: Player trade hand setup.");
        for (int i = 0; i < player.playerHand.Count; ++i)
        {
            GameObject playerCard = Instantiate(Resources.Load("TradingCard")) as GameObject;
            playerCard.transform.SetParent(playerTradeHand.transform, false);
            playerCard.GetComponent<TradingPhaseCard>().owner = "Player";
            playerCard.GetComponent<TradingPhaseCard>().card = player.playerHand[i];
        }
        */

        //Preparing planet trade hand.
        Debug.Log("12: Planet trade hand setup.");
        planet.RevealCards(6); //WE'VE ARRIVED AT THE PLANET, IT CAN REVEAL IT'S CARDS NOW
        for (int i = 0; i < planet.trades.Count; ++i)
        {
            GameObject planetCard = Instantiate(Resources.Load("TradingCard")) as GameObject;
            planetCard.transform.SetParent(planetTradeHand.transform, false);
            planetCard.GetComponent<TradingPhaseCard>().owner = "Planet";
            planetCard.GetComponent<TradingPhaseCard>().card = planet.trades[i].card;
        }


        Debug.Log("13: Trade Menu setup is complete.");
        tradeMenuReady = true;
        
    }

    public void SetAbilityChosen(bool active)
    {
        abilityChosen = active;
    }

    public void AbilityChoicePhase()
    {
        Debug.Log("1");
        player.CleanUpHand();


        Debug.Log("2");
        //Iterate list for choosable cards.
        List<Card> UsableAbilityCards = new List<Card>();
        for (int i = 0; i < player.playerHand.Count; ++i)
        {
            if (player.playerHand[i].cardType == Card.Type.TRADE)
            {
                //Check if trade card is usable for player.
                if (player.mayTrade && player.playerHand[i].Usable(player.fuel, player.playerHand) && player.playerHand[i].cardType == Card.Type.TRADE)
                {
                    UsableAbilityCards.Add(player.playerHand[i]);
                }
            }
        }

        if (UsableAbilityCards.Count == 0) 
        {
            Debug.Log("3*: No cards available. Skipping steps 3-8.");
            abilityChoosingComplete = true;
            return;
        }

        Debug.Log("3: Number of usable trade cards is " + UsableAbilityCards.Count + ".");
        //Open menu for choosable abilities.
        //AbilityChoiceMenu.gameObject.SetActive(true);


        /*
        Debug.Log("4");
        //Spawn choosable ability cards for trade.
        for (int i = 0; i < UsableAbilityCards.Count; ++i)
        {
            GameObject blankTradeAbilityCard = Instantiate(Resources.Load("TradeAbilityCard")) as GameObject;
            blankTradeAbilityCard.transform.SetParent(AbilityChoiceMenu.transform, false);
            blankTradeAbilityCard.GetComponent<AbilityChoiceCardButton>().card = UsableAbilityCards[i];
            blankTradeAbilityCard.GetComponent<AbilityChoiceCardButton>().commerceManager = this;
        }
        */
    }

    public void SetChosenTradeCard(Card card)
    {
        chosenTradeCard = card as TradeCard;
        toPlanet = new List<Card>();
        toPlayer = new List<Card>();
    }

    //Looks at GameObject that holds all the UI cards.
    public void ToggleTrading(Card card, GameObject owner)
    {
        //If gameobject is player's trade hand.
        //Literally toggles the cards in toPlanet and toPlayer
        if (owner == playerTradeHand)
        {
            if (toPlanet.Contains(card))
            {
                toPlanet.Remove(card);
            }
            else
            {
                toPlanet.Add(card);
            }
        }
        else
        {
            if (toPlayer.Contains(card))
            {
                toPlayer.Remove(card);
            }
            else
            {
                toPlanet.Add(card);
            }
        }
    }

    // TO BE CALLED BY THE CARD BUTTONS
    // TAKE THE ASSOCIATED DATA TYPE either PLAYER TRADE HAND OR PLANET TRADE HAND
    public void CardClickedOn(Card card, GameObject owner, List<Card> UsableAbilityCards){
        if(!abilityChoosingComplete && UsableAbilityCards.Contains(card)){
            SetChosenTradeCard(card);
            TradeAbilityChosenPhase();
        }else if(abilityChoosingComplete){
             ToggleTrading(card, owner);
        }

    }

    // TO BE CALLED BY THE REFUEL BUTTON
    public void Refule(){
        if(planet.canRefuel && player.mayRefule){
            player.fuel += refuelValue + player.extaRefuel;
            player.extaRefuel = 0;
            planet.canRefuel = false;
            Debug.Log("Refueling.");
            EndCommercePhase();
        }
        else
        {
            Debug.Log("Not refueling.");
        }
    }

    public void TradeAbilityChosenPhase()
    {
        Debug.Log("7: Starting trade setup.");
        cardsLosing = chosenTradeCard.cardCost.Length;
        cardsGaining = chosenTradeCard.cardReward.Length;

        //Close ability choice menu.
        Debug.Log("8: Closing Ability Choice Menu.");
        //AbilityChoiceMenu.SetActive(false);
        abilityChoosingComplete = true;
        readyForTradeSetup = true;
        if(!tradeMenuReady){
            SetUpTradeMenu();
        }
    }

    // TO BE CALLED BY CANCEL TRADE
    public void LeaveTradeAbilityChosenPhase(){
        Debug.Log("Leaving Trade Ability Chosen Phase");
        //AbilityChoiceMenu.SetActive(false);
        abilityChoosingComplete = false;
        readyForTradeSetup = false;
    }

    // TO BE CALLED BY:
    //      SUCCESSFUL TRADE
    //      SUCCESSFUL REFULE
    public void EndCommercePhase(){
        player.mayRefule = true;
        player.mayTrade = true;
        planetFix.returnToMapScreen();
    }

    
}
