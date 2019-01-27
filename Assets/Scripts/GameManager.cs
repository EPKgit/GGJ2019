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
    private float currentHandSize;
    private Card chosenCard;
    private Planet chosenPlanet;

    void Start()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
        DontDestroyOnLoad(gameObject);  
    }

    void Update()
    {
        if(player != null && player.playerHand != null && player.playerHand.Count != currentHandSize)
        {
            UpdateHand();
        }
    }

    public void GoToGameScene()
    {
        SceneManager.LoadScene("SampleScene");
        AudioManager.instance.SetMusic();
        AudioManager.instance.SetMenuTime(Time.time);
    }

    public void ClickInput(Planet p)
    {
        InputMove(p);
    }

    public void InputMove(Planet p)
    {
        chosenPlanet = null;
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
        //Debug.Log("set planetls");
        planetList = planets;
        GameObject temp = GameObject.FindWithTag("Player");
        playerMovement = temp.GetComponent<PlayerMovement>();
        player = temp.GetComponent<Player>();
        if(playerMovement.currentPlanet == null) {
            playerMovement.currentPlanet = planets[0].GetComponent<Planet>();
        }
        StartCoroutine(WaitForMoveInput());
    }

    private bool pickedMoveOption;

    public void setMoveOption(Card c)
    {
        pickedMoveOption = true;
        chosenCard = c;
    }
    public void SetPlanetOption(Planet p)
    {
        chosenPlanet = p;
    }

    void CheckLegalMoves()
    {
        int index = 0;
        //Debug.Log(player.playerHand.Count);
        foreach(Card c in player.playerHand)
        {
            //Debug.Log(c.cardName);
            if(c.Usable(player.fuel, player.playerHand) && c.cardType == Card.Type.MOVEMENT)
            {
                hand[index].GetComponent<Image>().enabled = true;
                c.halfCard.GetComponent<Button>().onClick.AddListener( () => setMoveOption(c));
            }
            else
            {
                hand[index].GetComponent<Image>().enabled = false;
            }
            index++;
        }
        for(int x = index; x < hand.Length; ++x)
        {
            hand[x].GetComponent<Image>().enabled = false;
        }
    }

    void UpdateHand()
    {
        //Debug.Log("Update");
        int index = 0;
        foreach(GameObject slot in hand)
        {
            foreach(Transform child in slot.transform)
            {
                child.transform.parent = null;
                child.position = Vector3.one * 9999;
            }
            if(player.playerHand.Count > index)
            {
                //Debug.Log(index);
                player.playerHand[index].halfCard.transform.SetParent(slot.transform);
                RectTransform rect = player.playerHand[index].halfCard.GetComponent<RectTransform>();
                rect.anchoredPosition = Vector2.zero;
                rect.anchoredPosition3D = Vector3.zero;
                rect.offsetMax = Vector2.zero;
                rect.offsetMin = Vector2.zero;
            }
            index++;
        }
        currentHandSize = player.playerHand.Count;
    }

    void SetAllSlotsOff()
    {
        for(int x = 0; x < hand.Length; ++x)
        {
            hand[x].GetComponent<Image>().enabled = false;
        }
    }

    void ResetListeners()
    {
        foreach(Card c in player.playerHand)
        {
            c.halfCard.GetComponent<Button>().onClick.RemoveAllListeners();
        }
    }

    IEnumerator WaitForMoveInput()
    {
        yield return new WaitUntil( () => player.playerHand !=  null);
        yield return new WaitUntil( () => player.playerHand.Count != 0);
        CheckLegalMoves();
        yield return new WaitUntil( () => pickedMoveOption);
        pickedMoveOption = false;
        ResetListeners();
        SetAllSlotsOff();
        Debug.Log(chosenCard);
        HashSet<Planet> planetsAvail = (chosenCard as MovementCard).GetHopTargets(playerMovement.currentPlanet, 0);
        foreach(Planet p in planetsAvail)
        {
            p.StartGlow();
        }
        while(!planetsAvail.Contains(chosenPlanet))
        {
            chosenPlanet = null;
            while(chosenPlanet == null)
            {
                Debug.Log("busy wait");
                yield return null;
            }
        }
        InputMove(chosenPlanet);
    }
}
