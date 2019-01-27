using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetSideManager : MonoBehaviour
{
    public static PlanetSideManager instance;

    private Image planetScreen;
    
    void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
        gameObject.SetActive(false);

        planetScreen = transform.GetChild(0).GetComponent<Image>();
    }

    void Update()
    {
        
    }

    public void comeFromMapScreen(Planet p)
    {
        planetScreen.sprite = p.background;
        gameObject.SetActive(true);
    }

    public void returnToMapScreen()
    {
        gameObject.SetActive(false);
        if(AudioManager.instance != null)
        {
            AudioManager.instance.SetMusic();
        }
    }
    
    
}
