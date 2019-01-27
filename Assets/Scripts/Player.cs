using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public List<Card> playerHand;
    public Planet currentPlanet;
    public int maxHandSize;
    public int fuel;
    public int fuelCap;

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
