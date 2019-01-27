using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradingButtonControl : MonoBehaviour
{
    [HideInInspector] public Planet planet;
    public Player player;

    public NewCommerceManager comMan;

    public void TradeButtonPressed()
    {

        if (CheckValidTrade(comMan.chosenTradeCard, comMan.toPlayer,
                comMan.toPlanet, player.fuel))
        {
            TradeCardsAndFuel(comMan.toPlayer, comMan.toPlanet, -comMan.chosenTradeCard.fuelCost);
            comMan.EndCommercePhase();
        }
        else
        {
            Debug.Log("Illegal Trade");
        }
    }

    public bool CheckValidTrade(TradeCard trade, List<Card> toPlayer, List<Card> toPlanet, int fuel){
        return trade.IsLegalTrade(fuel, toPlanet, toPlayer);
    }


    //Only handles the regular transactions.
    public void TradeCardsAndFuel(List<Card> toPlayer, List<Card> toPlanet, int netFuel)
    {
        planet = PlayerMovement.instance.currentPlanet;
        //Also get the Player GameObject to player variable.

        player.fuel += netFuel;

        //Remove respective cards from player and planet.
        //Removing from player first.
        foreach (Card c in toPlanet)
        {
            int index = player.playerHand.FindIndex(x => x == c);

            if (index == -1)
            {
                Debug.Log("TradingButtonControl Error: Card could not be found in playerHand for removal.");
            }

            player.playerHand.RemoveAt(index);
        }

        //Removing from planet
        foreach (Card c in toPlayer)
        {
            int index = planet.trades.FindIndex(x => x.card == c);

            if (index == -1)
            {
                Debug.Log("TradingButtonControl Error: Card could not be found in planet's hand for removal.");
            }

            planet.trades.RemoveAt(index);
        }

        //Adding cards to player and planet now, respectively.
        foreach (Card c in toPlayer)
        {
            player.playerHand.Add(c);
        }


        //Eric please confirm this is how a planet's trades work.
        foreach (Card c in toPlanet)
        {
            Planet.PlanetTradeCard ptc = new Planet.PlanetTradeCard();
            ptc.revealed = true;
            ptc.card = c;
            planet.trades.Add(ptc);
        }

        // TODO: RESOLVE CARD SPECIAL EFFECTS MAYBE NOPE
    }


    }

