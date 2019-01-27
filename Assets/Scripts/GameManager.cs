using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    }
    IEnumerator GameSceneInit()
    {
        while(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            yield return null;
        }
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    public void GoToGameScene()
    {
        SceneManager.LoadScene("SampleScene");
        AudioManager.instance.SetMusic();
        AudioManager.instance.SetMenuTime(Time.time);
        StartCoroutine(GameSceneInit());
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
        if(AudioManager.instance != null)
        {
            AudioManager.instance.SetMusic(p.theme);
        }
        PlayerMovement.instance.currentPlanet = p;
        CameraController.instance.point = p.transform.position;
        //p.HideGlow();
    }

    public void setPlanets(List<GameObject> planets)
    {
        planetList = planets;
        InputMove(planets[0].GetComponent<Planet>());
    }
}
