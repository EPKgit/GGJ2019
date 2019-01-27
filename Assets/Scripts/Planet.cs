﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.UI;

public class Planet : MonoBehaviour
{
    public Sprite icon;
    public Sprite background;
    public AudioClip theme;
    [HideInInspector]public List<PlanetTradeCard> trades = new List<PlanetTradeCard>();
    [HideInInspector]public List<Planet> connections = new List<Planet>();
    public bool canRefuel;
    public Card.Suit Suit;
    public string flavorText;
    public float spinSpeed = 0f;

    private bool shouldDisplay;
    private Transform spriteTransform;
    private Canvas popupUI;
    private GameObject hoverSprite;

    void Start()
    {
        popupUI = transform.GetChild(2).gameObject.GetComponent<Canvas>();
        popupUI.gameObject.SetActive(false);
        spriteTransform = transform.GetChild(0);
        spriteTransform.gameObject.GetComponent<SpriteRenderer>().sprite = icon;
        hoverSprite = transform.GetChild(1).gameObject;
        hoverSprite.SetActive(false);
        // trades = new List<Card>();
        // connections = new List<Planet>();
    }
    
    void Update()
    {
        spriteTransform.Rotate(0f, 0f, spinSpeed * Time.deltaTime);
    }

    void OnMouseEnter()
    {
        //Debug.Log("enter");
        shouldDisplay = true;
        popupUI.gameObject.SetActive(true);
        if(PlayerMovement.instance.currentPlanet.connections.Contains(this))
        {
            hoverSprite.SetActive(true);
        }
    }

    void OnMouseExit()
    {
        //Debug.Log("exit");
        shouldDisplay = false;
        popupUI.gameObject.SetActive(false);
        hoverSprite.SetActive(false);
    }

    public void HideGlow()
    {
        hoverSprite.SetActive(false);
    }
    
    void OnMouseDown()
    {
        GameManager.instance.ClickInput(this);
    }

    // reveals up to i cards (pass 6 to reveal all)
    // return the number of cards revealed
    public int RevealCards(int i){
        int revealed = 0;
        foreach(PlanetTradeCard c in trades){
            if(!c.revealed){
                c.revealed = true;
                revealed++;
                if(revealed >= i){
                    break;
                }
            }
        }
        return revealed;
    }

    public class PlanetTradeCard{
        public bool revealed;
        public Card card;
    }

}

[CustomEditor(typeof(Planet))]
public class PlanetEditor : Editor 
{
    public override void OnInspectorGUI()
	{
		Planet t = target as Planet;
		DrawDefaultInspector();
        EditorGUILayout.LabelField("Connections");
        EditorGUI.indentLevel++;
        foreach(Planet p in t.connections)
        {
            EditorGUILayout.LabelField(p.name);
        }
        EditorGUI.indentLevel--;
        EditorGUILayout.LabelField("-----------");
	}
}
