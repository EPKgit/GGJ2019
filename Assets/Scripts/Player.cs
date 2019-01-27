using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public List<GameObject> playerCards = new List<GameObject>();
    public List<Card> playerHand = new List<Card>();
    public Planet currentPlanet;    //This is useless but I have no idea if someone else is using it.
    public int maxHandSize;
    public int fuel;
    public int fuelCap;

    private PlayerMovement pm;

    void Start()
    {
        pm = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        currentPlanet = pm.currentPlanet;
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
