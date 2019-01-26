using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommerceManager : MonoBehaviour
{
    public Player player;
    public Planet currentPlanet;
    public Canvas tradingInterface;

    //Card Generation Formatting
    public float marginX;
    public float bottomMarginY;
    public float topMarginY;

    public bool actionTaken;


    // Start is called before the first frame update
    void Start()
    {
        currentPlanet = player.currentPlanet;
        actionTaken = false;
        GenerateLayout();
    }

    void GenerateLayout()
    {
        //iterate through player's card list
        //generate cards based on dividing algorithm using marginX, bottomMarginY for card Y position

        //iterate through planet's cardlist
        //generate cards, leave space for refueling but you can ignore that for now i think
    }

    //Trading/Refueling
    //For refueling, once button is pressed, actionTaken is set to true and turn is basically done
    //For trading, once dragged object hovers another object, the card positions are swapped (literally, this should work)
    //actionTaken is set to true
    //card data is swapped in the respective lists

}
