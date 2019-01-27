using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public LayerMask planetLayer;

    private GameObject player;
    private PlayerMovement playerMovement;
    private List<GameObject> planetList;

    void Start()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        player = GameObject.FindGameObjectWithTag("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    void Update()
    {
    }

    public void ClickInput(Planet p)
    {
        InputMove(p);
    }

    public void InputMove(Planet p)
    {
        if(PlanetSideManager.instance != null)
        {
            PlanetSideManager.instance.comeFromMapScreen(p);
        }
        playerMovement.currentPlanet = p;
        CameraController.instance.point = p.transform.position;
        //p.HideGlow();
    }

    public void setPlanets(List<GameObject> planets)
    {
        planetList = planets;
        InputMove(planets[0].GetComponent<Planet>());
    }
}
