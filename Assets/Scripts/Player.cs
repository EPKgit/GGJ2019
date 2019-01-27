using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Card[] startingHand;
    public List<Card> playerHand;
    public Planet currentPlanet;
    public int maxHandSize;
    public int fuel;
    public int fuelCap;

    void Start()
    {
        playerHand = new List<Card>(startingHand);
    }

    public void CleanUpHand()
    {
        for (int i = playerHand.Count - 1; i > -1; --i)
        {
            if (playerHand[i] == null)
                playerHand.RemoveAt(i);
        }

        playerHand.TrimExcess();
    }
}
