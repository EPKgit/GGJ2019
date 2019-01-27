using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipButtonControl : MonoBehaviour
{
    //I'm going to pretend that phase changes and turn changes occur by enabling and disabling components/game objects within the scene.
    //NewCommerceManager needs a "trade complete" boolean that tells me that a trade was successfully transacted.

    public GameObject movementManager;
    public GameObject invalidWindow;
    [HideInInspector] public GameObject commerceManager;
    [HideInInspector] Planet planet;

    bool tradingIsComplete; //Dummy/substitute variable.

    public void MoveToNextTurn()
    {
        commerceManager = this.transform.gameObject;
        planet = PlayerMovement.instance.currentPlanet;

        if (tradingIsComplete)
        {
            Debug.Log("Trading complete. Going to next turn.");
            commerceManager.SetActive(false);
        }
        else if (!tradingIsComplete && planet.canRefuel)
        {
            Debug.Log("SkipButtonControl: No trade done. Refueling instead.");
            //I have no idea how much refueling you get at a planet.
            //Needs to call a public function to call for refueling here.
            commerceManager.SetActive(false);
        }
        else
        {
            Debug.Log("SkipButtonControl: Cannot refuel. Finish your trade or settle.");
        }
    }

    public void OkayButton()
    {
        invalidWindow.SetActive(false);
    }
}
