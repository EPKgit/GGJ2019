using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Planet : MonoBehaviour
{
    public Sprite icon;
    public Image background;
    public List<Card> trades;
    public bool canRefuel;
    public Text flavorText;
    public List<Planet> connections;

    void Start()
    {
        connections = new List<Planet>();
    }
}
