using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewCommerceManager : MonoBehaviour
{
    public Player player;
    public Planet planet;

    //For ability choosing phase.
    public GameObject AbilityChoiceMenu;
    [HideInInspector] public bool abilityChosen;
    [HideInInspector] public Card chosenTradeCard;

    //For ability chosen set-up phase.
    [HideInInspector] public int cardsLosing;
    [HideInInspector] public int cardsGaining;
    private bool abilityChoosingComplete;
    private bool readyForTradeSetup;

    //For trading menu set-up phase.
    private bool tradeMenuReady;
    public GameObject TradingMenu;
    public Image landscape;
    public GameObject playerTradeHand;
    public GameObject planetTradeHand;

    void Start()
    {
        abilityChosen = false;
        abilityChoosingComplete = false;
        readyForTradeSetup = false;
        tradeMenuReady = false;
        if (AbilityChoiceMenu.activeSelf == true)
        {
            AbilityChoiceMenu.SetActive(false);
        }
        if (TradingMenu.activeSelf == true)
        {
            TradingMenu.SetActive(false);
        }
        AbilityChoicePhase();
    }

    private void Update()
    {
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

    }

    public void SetUpTradeMenu()
    {
        Debug.Log("10: Setting up landscape.");
        landscape.sprite = planet.background;
        TradingMenu.SetActive(true);


        //Preparing player trade hand.
        Debug.Log("11: Player trade hand setup.");
        for (int i = 0; i < player.playerHand.Count; ++i)
        {
            GameObject playerCard = Instantiate(Resources.Load("TradingCard")) as GameObject;
            playerCard.transform.SetParent(playerTradeHand.transform, false);
            playerCard.GetComponent<TradingPhaseCard>().owner = "Player";
            playerCard.GetComponent<TradingPhaseCard>().card = player.playerHand[i];
        }

        //Preparing planet trade hand.
        Debug.Log("12: Planet trade hand setup.");
        for (int i = 0; i < planet.trades.Count; ++i)
        {
            GameObject planetCard = Instantiate(Resources.Load("TradingCard")) as GameObject;
            planetCard.transform.SetParent(planetTradeHand.transform, false);
            planetCard.GetComponent<TradingPhaseCard>().owner = "Planet";
            planetCard.GetComponent<TradingPhaseCard>().card = planet.trades[i];
        }


        Debug.Log("13: Trade Menu setup is complete.");
        tradeMenuReady = true;
        
    }

    public void SetChosenTradeCard(Card card)
    {
        chosenTradeCard = card;
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
            if (player.playerHand[i].cardType == "Trade")
            {
                //Check if trade card is usable for player.
                if (player.playerHand[i].Usable(player.fuel, player.playerHand))
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
        AbilityChoiceMenu.gameObject.SetActive(true);


        Debug.Log("4");
        //Spawn choosable ability cards for trade.
        for (int i = 0; i < UsableAbilityCards.Count; ++i)
        {
            GameObject blankTradeAbilityCard = Instantiate(Resources.Load("TradeAbilityCard")) as GameObject;
            blankTradeAbilityCard.transform.SetParent(AbilityChoiceMenu.transform, false);
            blankTradeAbilityCard.GetComponent<AbilityChoiceCardButton>().card = UsableAbilityCards[i];
            blankTradeAbilityCard.GetComponent<AbilityChoiceCardButton>().commerceManager = this;
        }
    }

    public void TradeAbilityChosenPhase()
    {
        Debug.Log("7: Starting trade setup.");
        cardsLosing = chosenTradeCard.cardCost;
        cardsGaining = chosenTradeCard.cardReward;

        //Close ability choice menu.
        Debug.Log("8: Closing Ability Choice Menu.");
        AbilityChoiceMenu.SetActive(false);
        abilityChoosingComplete = true;
        readyForTradeSetup = true;
    }
}
