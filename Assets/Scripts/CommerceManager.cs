using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommerceManager : MonoBehaviour
{
    public Player player;
    public Planet currentPlanet;


    // Start is called before the first frame update
    void Start()
    {
        currentPlanet = player.currentPlanet;
    }

}
