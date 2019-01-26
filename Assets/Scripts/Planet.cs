using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class Planet : MonoBehaviour
{
    public Sprite icon;
    public Sprite background;
    [HideInInspector]public List<Card> trades = new List<Card>();
    [HideInInspector]public List<Planet> connections = new List<Planet>();
    public bool canRefuel;
    public Text flavorText;

    void Start()
    {
        // trades = new List<Card>();
        // connections = new List<Planet>();
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
