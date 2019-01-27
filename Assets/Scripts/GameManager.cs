using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public LayerMask planetLayer;
    public GameObject[] hand;

    public PlayerMovement playerMovement;
    public Player player;
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
        GameObject temp = null;
        while(temp == null)
        {
            temp = GameObject.FindWithTag("Player");
            yield return null;
        }
        playerMovement = temp.GetComponent<PlayerMovement>();
        player = temp.GetComponent<Player>();
        StartCoroutine(WaitForMoveInput()); 
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

    private bool pickedMoveOption;
    public void setMoveOption()
    {
        pickedMoveOption = true;
    }
    void CheckLegalMoves()
    {
        int index = 0;
        foreach(Card c in player.playerHand)
        {
            if(c.Usable(player.fuel, player.playerHand))
            {
                hand[index].GetComponent<Image>().enabled = true;
            }
            else
            {
                hand[index].GetComponent<Image>().enabled = false;
            }
        }
    }
    IEnumerator WaitForMoveInput()
    {
        yield return new WaitUntil( () => player.playerHand.Count != 0);
        CheckLegalMoves();
        yield return new WaitUntil( () => pickedMoveOption);

    }
}
