using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewCommerceManager : MonoBehaviour
{
    public Player player;
    public Planet planet;
    public GameObject AbilityChoiceMenu;
    [HideInInspector] public bool abilityChosen;

    void Start()
    {
        abilityChosen = false;
        AbilityChoicePhase();
    }

    private void Update()
    {
        //Player chooses ability.
        if (abilityChosen == true)
        {
        }

    }

    public bool TradeAbilityCardUsable(Card tradeCard)
    {
        //Check if we go over fuel cost.
        if (tradeCard.fuelCost > player.fuel)
        {
            Debug.Log("Not enough fuel. Trade card not used.");
            return false;
        }
        //Check if we go over card cost.
        if (tradeCard.cardCost > player.playerHand.Count)
        {
            Debug.Log("Not enough cards. Trade card not used.");
            return false;
        }
        //Check if planet has enough cards.

        return true;
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
                if (TradeAbilityCardUsable(player.playerHand[i]) == true)
                {
                    UsableAbilityCards.Add(player.playerHand[i]);
                }
            }
        }

        Debug.Log("3");
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
}
