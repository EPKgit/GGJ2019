using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradingButtonControl : MonoBehaviour
{
    public Planet planet;
    public Player player;

    public void TradeButtonPressed()
    {
        if(CurrentPlanetCheck())
        {
            //Check if transaction is valid.
                //If valid, do transaction.
                //Else, send warning.
        }
    }

    public void TradeCardsAndFuel(List<Card> toPlayer, List<Card> toPlanet, int netFuel)
    {
        planet = PlayerMovement.instance.currentPlanet;
        //Also get the Player GameObject to player variable.

        player.fuel += netFuel;

        //Remove respective cards from player and planet.
        //Removing from player first.
        foreach (Card c in toPlanet)
        {
            int index = player.playerHand.FindIndex(x => x.cardName == c.cardName);

            if (index == -1)
            {
                Debug.Log("TradingButtonControl Error: Card could not be found in playerHand for removal.");
            }

            player.playerHand.RemoveAt(index);
        }

        //Removing from planet
        foreach (Card c in toPlayer)
        {
            int index = planet.trades.FindIndex(x => x.cardName == c.cardName);

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

        foreach (Card c in toPlanet)
        {
            planet.trades.Add(c);
        }
        
        
    }

    public void TradeValidityCheck()
    {

    }

    private bool CurrentPlanetCheck()
    {
        //Uses instance of PlayerMovement script.
        if (PlayerMovement.instance != null)
        {
            Debug.Log("TradeButtonControl: PlayerMovement Instance found.");

            if (PlayerMovement.instance.currentPlanet != null)
            {
                Debug.Log("TradeButtonControl: Planet has been found.");
                return true;
            }
            else
            {
                Debug.Log("TradeButtonControl: Planet has not been found.");
                return false;
            }
        }
        else
        {
            Debug.Log("TradeButtonControl: PlayerMovement instance could not be found.");
            return false;
        }
    }

}
