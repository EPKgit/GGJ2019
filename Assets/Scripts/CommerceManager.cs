using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommerceManager : MonoBehaviour
{
    public Player player;
    public Planet currentPlanet;
    public Canvas tradingInterface;

    //Card 

    //Card Generation Formatting
    public List<DraggableCard> playerCards;
    public List<DraggableCard> planetCards;
    public float marginX;
    public float bottomMarginY;
    public float topMarginY;

    public bool actionTaken;
    public bool dragging;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("CommerceManager is enabled.");
        currentPlanet = player.currentPlanet;
        actionTaken = false;
        GenerateLayout();
    }

    void GenerateLayout()
    {
        float canvasLeftBorder = tradingInterface.pixelRect.xMin + marginX;
        float canvasRightBorder = tradingInterface.pixelRect.xMax - marginX;
        float canvasSpacing = (canvasRightBorder - canvasLeftBorder) / (player.playerHand.Count + 1);
        Debug.Log(player.playerHand.Count);

        //Generate interactble player hand.
        for (int i = 0; i < player.playerHand.Count; ++i)
        {
            GameObject blankCard = Instantiate(Resources.Load("TestDraggableCard")) as GameObject;
            blankCard.transform.SetParent(this.gameObject.transform, false);
            blankCard.transform.position = new Vector2(canvasLeftBorder + canvasSpacing * (i + 1), tradingInterface.transform.position.y);
            blankCard.GetComponent<DraggableCard>().card = player.playerHand[i];
            blankCard.GetComponent<DraggableCard>().LoadCard();
        }

        //iterate through planet's cardlist
        //generate cards, leave space for refueling but you can ignore that for now i think
    }

    //Trading/Refueling
    //For refueling, once button is pressed, actionTaken is set to true and turn is basically done
    //For trading, once dragged object hovers another object, the card positions are swapped (literally, this should work)
    //actionTaken is set to true
    //card data is swapped in the respective lists

}
