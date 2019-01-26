using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommerceManager : MonoBehaviour
{
    public Player player;
    public Planet currentPlanet;
    public Canvas tradingInterface;

    //Card Generation Formatting
    //public List<DraggableCard> playerCards;
    //public List<DraggableCard> planetCards;
    public float marginX;
    public float marginY;


    public bool hasTakenAction;
    public bool dragging;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("CommerceManager is enabled.");
        currentPlanet = player.currentPlanet;
        hasTakenAction = false;
        GenerateLayout();
    }

    void GenerateLayout()
    {
        GenerateCardLayout();
        GenerateLandscape();
    }

    void GenerateCardLayout()
    {
        float canvasLeftBorder = tradingInterface.pixelRect.xMin + marginX;
        float canvasRightBorder = tradingInterface.pixelRect.xMax - marginX;
        float canvasBottomBorder = tradingInterface.pixelRect.yMin + marginY;
        float canvasTopBorder = tradingInterface.pixelRect.yMax - marginY;
        float handSpacingPlayer = (canvasRightBorder - canvasLeftBorder) / (player.playerHand.Count + 1);
        float handSpacingPlanet = (canvasRightBorder - canvasLeftBorder) / (currentPlanet.trades.Count + 1);

        //Generate interactble player hand.
        for (int i = 0; i < player.playerHand.Count; ++i)
        {
            GameObject blankCard = Instantiate(Resources.Load("TestDraggableCard")) as GameObject;
            blankCard.transform.SetParent(this.gameObject.transform, false);
            blankCard.transform.position = new Vector2(canvasLeftBorder + handSpacingPlayer * (i + 1), canvasBottomBorder);
            blankCard.GetComponent<DraggableCard>().card = player.playerHand[i];
            blankCard.GetComponent<DraggableCard>().LoadCard();
            //playerCards.Add(blankCard.GetComponent<DraggableCard>());
        }

        //iterate through planet's cardlist
        //generate cards, leave space for refueling but you can ignore that for now i think
        for (int i = 0; i < currentPlanet.trades.Count; ++i)
        {
            GameObject blankCard = Instantiate(Resources.Load("TestDraggableCard")) as GameObject;
            blankCard.transform.SetParent(this.gameObject.transform, false);
            blankCard.transform.position = new Vector2(canvasLeftBorder + handSpacingPlanet * (i + 1), canvasTopBorder);
            blankCard.GetComponent<DraggableCard>().card = player.playerHand[i];
            blankCard.GetComponent<DraggableCard>().LoadCard();
            //planetCards.Add(blankCard.GetComponent<DraggableCard>());
        }
    }

    void GenerateLandscape()
    {
        this.transform.GetChild(0).GetComponent<Image>().sprite = currentPlanet.background;
    }

    public void ActionTaken()
    {
        hasTakenAction = true;
    }

    //Trading/Refueling
    //For refueling, once button is pressed, actionTaken is set to true and turn is basically done
    //For trading, once dragged object hovers another object, the card positions are swapped (literally, this should work)
    //actionTaken is set to true
    //card data is swapped in the respective lists

}
