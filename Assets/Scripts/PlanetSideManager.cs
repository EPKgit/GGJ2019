using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetSideManager : MonoBehaviour
{
    public static PlanetSideManager instance;

    private Image planetScreen;
    private Text planetText;
    
    void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
        gameObject.SetActive(false);

        planetScreen = transform.GetChild(0).GetComponent<Image>();
        planetText = transform.GetChild(1).GetComponent<Text>();
    }

    void Update()
    {
        
    }

    public void comeFromMapScreen(Planet p)
    {
        Debug.Log("comefrom");
        planetScreen.sprite = p.background;
        planetText.text = p.flavorText;
        gameObject.SetActive(true);
    }

    public void returnToMapScreen()
    {
        Debug.Log("return");
        gameObject.SetActive(false);
        if(AudioManager.instance != null)
        {
            AudioManager.instance.SetMusic();
        }
        GameManager.instance.SetPlanetOption(null);
        GameManager.instance.UpdateHand();
        GameManager.instance.StartMovePhase();
    }
    
    
}
