using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public List<GameObject> playerCards = new List<GameObject>();
    public List<Card> playerHand = new List<Card>();
    public Planet currentPlanet;    //This is useless but I have no idea if someone else is using it.
    public int maxHandSize;
    public int fuel = 6;
    public int fuelCap;
    public int extraHops = 0;
    public int extaRefuel = 0;
    public bool mayRefule = true;
    public bool mayTrade = true;

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

    public void ResolveSpecialAbilities(Card c){
        fuel += c.fuelReward;
        extraHops += c.hopsReward;
        extaRefuel += c.extraRefuelReward;
        c.ProcScoutAbility(pm.currentPlanet);
        mayRefule = c.mayRefuelAfter;
        mayTrade = c.mayTradeAfter;
    }
}
